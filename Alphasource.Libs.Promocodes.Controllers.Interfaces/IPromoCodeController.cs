using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Models;

namespace Alphasource.Libs.Promocodes.Controllers.Interfaces
{
    public interface IPromoCodeController
    {
        Task<IActionResult> GetPromoCodeByCampaign(string searchString);
        Task<IActionResult> SearchCampaign(string campaignName);
        IActionResult CreateNewCampaign(PromoCodeModel newPromoCodeModel);
        IActionResult UpdateCampaign(PromoCodeModel updatePromoCodeModel);
        IActionResult ViewAllCampaigns();

    }
}
