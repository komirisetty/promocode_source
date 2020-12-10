using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Utilities;

namespace Alphasource.Libs.Promocodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "defaultPolicy")]
    public class FranchisePromocodeController : ControllerBase
    {
        private readonly IFranchisePromocodeService _franchiseService;
        /// <summary>
        /// Main controller to Allocate promocode to franchisee 
        /// </summary>
        /// <param name="FranchiseService">Contractor for the FranchisePromocode contoller</param>
        public FranchisePromocodeController(IFranchisePromocodeService FranchiseService)
        {
            _franchiseService = FranchiseService;
        }
        /// <summary>
        /// Gets the promocodes given by the franchisee.
        /// </summary>
        /// <param name="campaignName">campaignName</param>
        /// <returns></returns>
        [HttpGet("CampaignName")]
        public async Task<ActionResult> GetAllocatedFranchise(string campaignName)
        {
            var promo = await _franchiseService.GetAllocatedFranchise(campaignName);
            if (promo == null)
            {
                return NotFound(new Response { ErrorMessage = AppMessages.ERR_DATA_NOT_FOUND, IsSuccess = false });
            }

            var list = promo.ToList();
            promo = list.OrderByDescending(x => x.AllocatedDate).ToList();

            return Ok(promo);

        }
        /// <summary>
        /// Admin allocates the Promocode to their franchise
        /// </summary>
        /// <param name="saveFranchisePromoCodesResource"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> Create([FromBody] FranchisePromocode saveFranchisePromoCodes)
        {
            try
            {
                ValidatePromocode(saveFranchisePromoCodes);
                saveFranchisePromoCodes.AvailablePromoCode = GetTotalPromocode(saveFranchisePromoCodes.CampaignName) - ( GetAllocatedPromocode(saveFranchisePromoCodes.CampaignName) + saveFranchisePromoCodes.AllocatedPromoCode);
                if (saveFranchisePromoCodes.AvailablePromoCode >= 0)
                {
                    saveFranchisePromoCodes.Id = "";
                    var promoCreated = await _franchiseService.Create(saveFranchisePromoCodes);

                    return Ok(promoCreated);
                }
                else
                {
                    return NotFound(new Response { ErrorMessage = AppMessages.ERR_INVALID_NOPROMOCODE, IsSuccess = false });
                }

            }
            catch (Exception exp)
            {
                return NotFound(new Response { ErrorMessage = exp.Message, IsSuccess = false });
            }

        }

        /// <summary>
        ///Update the promocode details for the franchise.
        /// </summary>
        /// <remarks>It is used to update promocode</remarks>
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] FranchisePromocode saveFranchisePromoCodes)
        {
            try
            {
                ValidatePromocode(saveFranchisePromoCodes);

                saveFranchisePromoCodes.AvailablePromoCode = GetTotalPromocode(saveFranchisePromoCodes.CampaignName) - (GetAllocatedPromocode(saveFranchisePromoCodes.CampaignName) + saveFranchisePromoCodes.AllocatedPromoCode);
                if (saveFranchisePromoCodes.AvailablePromoCode > 0)
                {
                    var promoCreated = await _franchiseService.Update(saveFranchisePromoCodes);

                    return Ok(promoCreated);
                }
                else
                {
                    return NotFound(new Response { ErrorMessage = AppMessages.ERR_INVALID_NOPROMOCODE, IsSuccess = false });
                }
            }
            catch (Exception exp)
            {
                return NotFound(new Response { ErrorMessage = exp.Message, IsSuccess = false });
            }
        }

        /// <summary>
        ///Delete the promocode allocation from the franchise.
        /// </summary>
        /// <remarks>It is used to delete a promocode allocation from Franchise</remarks>
        /// <param name="id">id is used to filter particular franchiese</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await _franchiseService.GetAllocatedFranchise(id);
            if (data == null)
            {
                return NotFound(new Response { ErrorMessage = AppMessages.ERR_DATA_NOT_FOUND, IsSuccess = false });
            }
            _franchiseService.Delete(id);

            return Ok(new Response { ErrorMessage = "", IsSuccess = true });
        }

        /// <summary>
        /// Validate promocode data before saving the allocation to the franchise
        /// </summary>
        /// <param name="saveFranchisePromoCodes"></param>
        private void ValidatePromocode(FranchisePromocode saveFranchisePromoCodes)
        {
            var validateName = !string.IsNullOrEmpty(saveFranchisePromoCodes.CampaignName) &&
                this._franchiseService != default(IFranchisePromocodeService);

            var validatePromocode = saveFranchisePromoCodes.AllocatedPromoCode > 0;

            if (!validateName)
                throw new Exception(AppMessages.ERR_EMPTY_CAMPAIGNNAME);
            if (!validatePromocode)
                throw new Exception(AppMessages.ERR_EMPTY_ALLOCATEDPROMOCODE);

          
            var totalpromocode = GetTotalPromocode(saveFranchisePromoCodes.CampaignName);

            if (saveFranchisePromoCodes.AllocatedPromoCode > totalpromocode)
            {
                throw new Exception(AppMessages.ERR_INVALID_ALLOCATEDPROMOCODEXCEED);
            }

            var promocodeEnddate = GetPromocodeEndDate(saveFranchisePromoCodes.CampaignName);
            if (promocodeEnddate < System.DateTime.Now)
            {
                throw new Exception(AppMessages.ERR_INVALID_ALLOCATEDPROMOCODEEXPIRED);
            }


        }

        private int GetAllocatedPromocode(string campaignName)
        {
            var franchiseResult = _franchiseService.GetAllocatedFranchise(campaignName);
           
            List<FranchisePromocode> franchiseList = franchiseResult.Result;
            int count = 0;
            if (franchiseList != null)
            {

                foreach (var promoCount in franchiseList)
                {
                    count = count + promoCount.AllocatedPromoCode;
                }

            }

            return count;
        }

        private int GetTotalPromocode(string campaignName)
        {
            var promocodeResult = _franchiseService.GetPromocode(campaignName);
            PromoCodeModel promocode = promocodeResult.Result;
            if (promocode == null)
            {
                throw new Exception(AppMessages.ERR_INVALID_CAMPAIGNNAME);
            }

            return promocode.NoOfPromoCodes;
        }

        private DateTime GetPromocodeEndDate(string campaignName)
        {
            var promocodeResult = _franchiseService.GetPromocode(campaignName);
            PromoCodeModel promocode = promocodeResult.Result;
            if (promocode == null)
            {
                throw new Exception(AppMessages.ERR_INVALID_CAMPAIGNNAME);
            }

            return promocode.EndDate;
        }
    }
}
