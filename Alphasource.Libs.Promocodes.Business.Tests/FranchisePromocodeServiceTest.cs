using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Alphasource.Libs.Promocodes.Business;
using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Service;
using Alphasource.Libs.Promocodes.Repositories.Interface;

namespace Alphasource.Libs.Promocodes.Business.Tests
{
    public class FranchisePromocodeServiceTest
    {
        [Fact]
        public async void CreateFranchiesPromocodeReturnNotValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeRepository>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };

            mockfranchisePromocodeService
              .Setup(service => service.Create(code))
              .ReturnsAsync(code);

            var franchisePromocodeController = new FranchisePromocodeService(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Create(code);

           Assert.NotNull(result);

           Assert.IsType<FranchisePromocode>(result);
        }
    }
}
