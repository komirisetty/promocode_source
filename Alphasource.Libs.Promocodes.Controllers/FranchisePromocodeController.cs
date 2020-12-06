using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Alphasource.Libs.Promocode.Utilities;

namespace Alphasource.Libs.Promocode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "defaultPolicy")]
    public class FranchisePromocodeController : ControllerBase
    {
        private readonly IFranchisePromocodeService _franchiseService;
        
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
                return NotFound(new Response { ErrorMessage = "Data Not found", IsSuccess = false });
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

                var promoCreated = await _franchiseService.Create(saveFranchisePromoCodes);

                return Ok(promoCreated);
            }
            catch (Exception exp)
            {
                return NotFound(new Response { ErrorMessage = exp.Message, IsSuccess = false });
            }

        }
        
        /// <summary>
        ///Update the promocode details.
        /// </summary>
        /// <remarks>It is used to update promocode</remarks>
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] FranchisePromocode saveFranchisePromoCodes)
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
        ///Delete the Allocation.
        /// </summary>
        /// <remarks>It is used to delete a promocode allocation from Franchise</remarks>
        /// <param name="id">id is used to filter particular franchiese and delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await _franchiseService.GetAllocatedFranchise(id);
            if (data == null)
            {
                return NotFound(new Response { ErrorMessage = "Data Not found", IsSuccess = false });
            }
            _franchiseService.Delete(id);

            return Ok(new Response { ErrorMessage = "", IsSuccess = true });
        }

        private void ValidatePromocode(FranchisePromocode saveFranchisePromoCodes)
        {
            var validateName = !string.IsNullOrEmpty(saveFranchisePromoCodes.CampaignName) &&
                this._franchiseService != default(IFranchisePromocodeService);

            var validatePromocode = saveFranchisePromoCodes.AllocatedPromoCode >= 0;

            if (!validateName)
                throw new Exception("CampaignName is mandatory");
            if (!validatePromocode)
                throw new Exception("AllocatePromoCode is mandatory");

            var promocodeResult = _franchiseService.GetPromocode(saveFranchisePromoCodes.CampaignName);
            PromoCodeModel promocode = promocodeResult.Result;
            if (promocode == null)
            {
                throw new Exception("Invalid CampaignName.");
            }

            var totalpromocode = promocode.NoOfPromoCodes;
            if (saveFranchisePromoCodes.AllocatedPromoCode > totalpromocode)
            {
                throw new Exception("Allocated promocode cannot exceed total promocode.");
            }

            var promocodeEnddate = promocode.EndDate;
            if (promocodeEnddate < System.DateTime.Now)
            {
                throw new Exception("CampaignName is not valid.");
            }
        }

    }
}
