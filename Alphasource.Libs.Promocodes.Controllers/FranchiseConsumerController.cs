using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using Alphasource.Libs.Promocodes.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "defaultPolicy")]
    public class FranchiseConsumerController : ControllerBase
    {
        private readonly IFranchiseConsumerService _franchiseService;
        /// <summary>
        /// Main controller to Allocate promocode to consumers 
        /// </summary>
        /// <param name="FranchiseService">Contractor for the FranchiseConsumer contoller</param>
        public FranchiseConsumerController(IFranchiseConsumerService FranchiseService)
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
        public async Task<ActionResult> Create([FromBody] FranchiseConsumer saveFranchisePromoCodes)
        {
            try
            {
                ValidatePromocode(saveFranchisePromoCodes);
                var promoCreated = await _franchiseService.Create(saveFranchisePromoCodes);
                return Ok(promoCreated);
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
        public async Task<IActionResult> Update([FromBody] FranchiseConsumer saveFranchisePromoCodes)
        {
            try
            {
                ValidatePromocode(saveFranchisePromoCodes);

                var promoCreated = await _franchiseService.Update(saveFranchisePromoCodes);

                return Ok(promoCreated);

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
        private void ValidatePromocode(FranchiseConsumer saveFranchisePromoCodes)
        {
            var validateName = !string.IsNullOrEmpty(saveFranchisePromoCodes.CampaignName) &&
                this._franchiseService != default(IFranchiseConsumerService);

            var validateFranchiseName = !string.IsNullOrEmpty(saveFranchisePromoCodes.FranchiseName);

            var validatePromocodeId = !string.IsNullOrEmpty(saveFranchisePromoCodes.PromocodeId);

            if (!validateName)
                throw new Exception(AppMessages.ERR_EMPTY_CAMPAIGNNAME);

            if (!validateFranchiseName)
                throw new Exception(AppMessages.ERR_EMPTY_FRANCHISENAME);

            if (!validatePromocodeId)
                throw new Exception(AppMessages.ERR_EMPTY_PROMOCODEID);


            var promocodeEnddate = GetPromocodeEndDate(saveFranchisePromoCodes.CampaignName);
            if (promocodeEnddate < System.DateTime.Now)
            {
                throw new Exception(AppMessages.ERR_INVALID_ALLOCATEDPROMOCODEEXPIRED);
            }


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
