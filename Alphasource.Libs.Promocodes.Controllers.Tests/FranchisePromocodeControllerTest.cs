using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocode.Controllers;
using Xunit;
using Moq;
using Alphasource.Libs.Promocodes.Service;
using Alphasource.Libs.Promocode.Utilities;

namespace Alphasource.Lib.Promocode.Controllers.Tests
{
    public class FranchisePromocodeControllerTest
    {
        [Fact]
        public async void GetFranchisePromocodeBySearchStringReturnValidResults()
        {
            var searchString = "F1";
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C2", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C3", FranchiseName = "F3", AllocatedPromoCode = 600, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockFranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();

            mockFranchisePromocodeService
                .Setup(service => service.GetAllocatedFranchise(searchString))
                .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeController(mockFranchisePromocodeService.Object);
            var result = await franchisePromocodeController.GetAllocatedFranchise(searchString);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;

            Assert.NotNull(convertedResult);

            var actualCustomers = convertedResult.Value as IEnumerable<FranchisePromocode>;

            Assert.NotNull(actualCustomers);

            var expectedNoOfCustomers = 3;
            var actualNoOfCustomers = actualCustomers.Count();

            Assert.Equal(expectedNoOfCustomers, actualNoOfCustomers);

            var expectedCustomerName = "F1";
            var actualCustomerName = actualCustomers.First().FranchiseName;

            Assert.Equal(expectedCustomerName, actualCustomerName);
        }
        /*
        [Fact]
        public async void InsertFranchiesPromocodeReturnValidResults()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };

            var response = new Response { ErrorMessage = "", IsSuccess = true, data = code };

            mockfranchisePromocodeService
                .Setup(service => service.Create(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode("C1"))
               .ReturnsAsync(promocode);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Create(code);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;
            var convertedResult1 = result;

            Assert.NotNull(convertedResult1);

            var actualCustomers = (FranchisePromocode)result;

            Assert.NotNull(actualCustomers);

            var expectedCustomerName = "F2";
            var actualCustomerName = actualCustomers.FranchiseName;

            Assert.Equal(expectedCustomerName, actualCustomerName);
        }

        [Fact]
        public async void UpdateFranchiesPromocodeReturnValidResults()
        {
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            

            mockfranchisePromocodeService
                .Setup(service => service.Update(code))
                .ReturnsAsync(code);


            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Update(code);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;
            var convertedResult1 = result;

            Assert.NotNull(convertedResult);

            var actualCustomers = convertedResult.Value as FranchisePromocode;

            Assert.NotNull(actualCustomers);

            var expectedCustomerName = "F1";
            var actualCustomerName = actualCustomers.FranchiseName;

            Assert.Equal(expectedCustomerName, actualCustomerName);
        }*/

    }
}
