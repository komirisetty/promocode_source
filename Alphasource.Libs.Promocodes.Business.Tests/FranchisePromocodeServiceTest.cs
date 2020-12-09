using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Alphasource.Libs.Promocodes.Business;
using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Services;
using Alphasource.Libs.Promocodes.Repositories.Interfaces;

namespace Alphasource.Libs.Promocodes.Business.Tests
{
    public class FranchisePromocodeServiceTest
    {
        [Fact]
        public async void CreateFranchiesPromocodeReturnValidResults()
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

        [Fact]
        public async void UpdateFranchiesPromocodeReturnValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeRepository>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };

            mockfranchisePromocodeService
              .Setup(service => service.Update(code))
              .ReturnsAsync(code);

            var franchisePromocodeController = new FranchisePromocodeService(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Update(code);

            Assert.NotNull(result);

            Assert.IsType<FranchisePromocode>(result);
        }

        [Fact]
        public async void GetFranchiesPromocodeReturnValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeRepository>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C2", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C3", FranchiseName = "F3", AllocatedPromoCode = 600, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };

            mockfranchisePromocodeService
              .Setup(service => service.GetAllocatedFranchise(code.CampaignName))
              .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeService(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.GetAllocatedFranchise(code.CampaignName);

            Assert.NotNull(result);

            Assert.IsType<List<FranchisePromocode>>(result);
        }

        [Fact]
        public async void GetFranchiesPromocodeByIdReturnValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeRepository>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C2", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C3", FranchiseName = "F3", AllocatedPromoCode = 600, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };

            mockfranchisePromocodeService
              .Setup(service => service.GetAllocatedFranchiseById(id))
              .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeService(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.GetAllocatedFranchiseById(id);

            Assert.NotNull(result);

            Assert.IsType<List<FranchisePromocode>>(result);
        }

        [Fact]
        public async void GePromocodeReturnValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeRepository>();
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };
            mockfranchisePromocodeService
              .Setup(service => service.GetPromocode(promocode.CampaignName))
              .ReturnsAsync(promocode);

            var franchisePromocodeController = new FranchisePromocodeService(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.GetPromocode(promocode.CampaignName);

            Assert.NotNull(result);

            Assert.IsType<PromoCodeModel>(result);
        }

        [Fact]
        public async void DeleteFranchiesPromocodeReturnNotValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeRepository>();

            var franchisePromocodeController = new FranchisePromocodeService(mockfranchisePromocodeService.Object);
            franchisePromocodeController.Delete(id);

            //Assert.NotNull(result);
        }
    }
}
