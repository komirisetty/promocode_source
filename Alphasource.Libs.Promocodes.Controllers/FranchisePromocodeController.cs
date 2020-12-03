using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Service;

namespace Alphasource.Lib.Promocode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "defaultPolicy")]

    public class FranchisePromocodeController : ControllerBase
    {
        private readonly IFranchisePromocodeService _franchiseService;
        private readonly IMapper _mapper;
        
        public FranchisePromocodeController(IFranchisePromocodeService FranchiseService, IMapper mapper)
        {
            _franchiseService = FranchiseService;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the promocodes given by the franchisee.
        /// </summary>
        /// <param name="campaignName">campaignName</param>
        /// <returns></returns>
        [HttpGet("CampaignName")]
        public async Task<ActionResult<List<FranchisePromocode>>> GetAllocatedFranchise(string campaignName)
        {
            var promo = await _franchiseService.GetAllocatedFranchise(campaignName);
            //if (promo == null)
            //{
            //    return NotFound(new Response { ErrorMessage = "Data Not found", IsSuccess = false });
            //}


            var list = promo.ToList();
            promo = list.OrderByDescending(x => x.AllocatedDate).ToList();

            //var promoResources = _mapper.Map<List<FranchisePromocode>, List<FranchisePromocode>>(promo);
            return Ok(promo);

        }
        /// <summary>
        /// Admin allocates the Promocode to their franchise
        /// </summary>
        /// <param name="saveFranchisePromoCodesResource"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<FranchisePromocode>> Create([FromBody] FranchisePromocode saveFranchisePromoCodesResource)
        {
             //var promo = _mapper.Map<SaveFranchisePromoCodesResource, AllocatePromoCodeToFranchise>(saveFranchisePromoCodesResource);


             var promoCreated = await _franchiseService.Create(saveFranchisePromoCodesResource);

            //if (promoCreated == null)
          //   {
          //       return NotFound(new Response { ErrorMessage = "Error while allocating promocode", IsSuccess = false });
        //     }

            //var promoResource = _mapper.Map<AllocatePromoCodeToFranchise, FranchisePromoCodesResource>(promoCreated);

            return Ok(promoCreated);
        }
        /// <summary>
        ///Update the promocode details.
        /// </summary>
        /// <remarks>It is used to update promocode</remarks>
        [HttpPut("")]
        public async Task<ActionResult<FranchisePromocode>> Update([FromBody] FranchisePromocode saveFranchisePromoCodesResource)
        {
           // var promo = _mapper.Map<SaveFranchisePromoCodesResource, AllocatePromoCodeToFranchise>(saveFranchisePromoCodesResource);


            var promoCreated = await _franchiseService.Update(saveFranchisePromoCodesResource);

           // if (promoCreated == null)
            //{
              //  return NotFound(new Response { ErrorMessage = "Error while allocating promocode", IsSuccess = false });
            //}

            //var promoResource = _mapper.Map<AllocatePromoCodeToFranchise, FranchisePromoCodesResource>(promoCreated);

            return Ok(promoCreated);
        }
    }
}
