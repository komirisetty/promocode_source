using Alphasource.Libs.Promocodes.Controllers;
using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using Alphasource.Libs.Promocodes.Utilities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Alphasource.Lib.Promocodes.Controllers.Tests
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

            var expected = "F1";
            var actual = actualCustomers.First().FranchiseName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetFranchisePromocodeBySearchStringReturnNotValidResults()
        {
            var searchString = "F1";
           
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockFranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();

            var franchisePromocodeController = new FranchisePromocodeController(mockFranchisePromocodeService.Object);
            var result = await franchisePromocodeController.GetAllocatedFranchise(searchString);

            Assert.NotNull(result);

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_DATA_NOT_FOUND;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }

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
            var returnValue = Assert.IsType<FranchisePromocode>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = "F2";
            var actual = returnValue.FranchiseName;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void InsertFranchiesPromocodeReturnValidResults_AvailablePromocode()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 2, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F3", AllocatedPromoCode = 1, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };

            var response = new Response { ErrorMessage = "", IsSuccess = true, data = code };

            mockfranchisePromocodeService
                .Setup(service => service.Create(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode("C1"))
               .ReturnsAsync(promocode);

            mockfranchisePromocodeService
                    .Setup(service => service.GetAllocatedFranchise("C1"))
                    .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Create(code);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;
            var returnValue = Assert.IsType<FranchisePromocode>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expectedPromocode = 32;
            var actualPromocode = returnValue.AvailablePromoCode;

            Assert.Equal(expectedPromocode, actualPromocode);
        }

        [Fact]
        public async void InsertFranchiesPromocodeReturnValidResults_NoFranchise()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 2, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
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
            var returnValue = Assert.IsType<FranchisePromocode>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expectedPromocode = 48;
            var actualPromocode = returnValue.AvailablePromoCode;

            Assert.Equal(expectedPromocode, actualPromocode);
        }

        [Fact]
        public async void InsertFranchiesPromocodeReturnValidResults_UtilizedPromocode()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C2", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C3", FranchiseName = "F3", AllocatedPromoCode = 600, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };

            var response = new Response { ErrorMessage = "", IsSuccess = true, data = code };

            mockfranchisePromocodeService
                .Setup(service => service.Create(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode("C1"))
               .ReturnsAsync(promocode);

            mockfranchisePromocodeService
                    .Setup(service => service.GetAllocatedFranchise("C1"))
                    .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Create(code);

            Assert.NotNull(result);

            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_INVALID_NOPROMOCODE;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void InsertFranchiesPromocodeReturnNotValidResults_InvalidCampaignName()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C2", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C3", FranchiseName = "F3", AllocatedPromoCode = 600, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };

            var response = new Response { ErrorMessage = "", IsSuccess = true, data = code };

            mockfranchisePromocodeService
                .Setup(service => service.Create(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
                    .Setup(service => service.GetAllocatedFranchise("C1"))
                    .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Create(code);

            Assert.NotNull(result);

            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_INVALID_CAMPAIGNNAME;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void InsertFranchiesPromocodeReturnNotValidResults()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
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

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void InsertFranchiesPromocodeReturnInvalidCampaignName()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };

            var response = new Response { ErrorMessage = "", IsSuccess = true, data = code };

            mockfranchisePromocodeService
                .Setup(service => service.Create(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode(""))
               .ReturnsAsync(promocode);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Create(code);

            Assert.NotNull(result);

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_EMPTY_CAMPAIGNNAME;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void InsertFranchiesPromocodeReturnInvalidAllocatedPromocode()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 0, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
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

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_EMPTY_ALLOCATEDPROMOCODE;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void InsertFranchiesPromocodeReturnExceedAllocatedPromocode()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 100, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
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

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_INVALID_ALLOCATEDPROMOCODEXCEED;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void InsertFranchiesPromocodeReturnExpiredPromocode()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F2", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2020, 1, 25, 10, 30, 45) };

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

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_INVALID_ALLOCATEDPROMOCODEEXPIRED;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void UpdateFranchiesPromocodeReturnValidResults()
        {
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };

            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();
            

            mockfranchisePromocodeService
                .Setup(service => service.Update(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode("C1"))
               .ReturnsAsync(promocode);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Update(code);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;
            var returnValue = Assert.IsType<FranchisePromocode>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = "F1";
            var actual = returnValue.FranchiseName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UpdateFranchiesPromocodeReturnNotValidResults()
        {
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 10, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };

            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();


            mockfranchisePromocodeService
                .Setup(service => service.Update(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode("C1"))
               .ReturnsAsync(promocode);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Update(code);

            Assert.NotNull(result);

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = AppMessages.ERR_INVALID_NOPROMOCODE;
            var actual = returnValue.ErrorMessage;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UpdateFranchiesPromocodeReturnNullData()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Update(null);

            Assert.NotNull(result);

            Assert.IsType<NotFoundObjectResult>(result);
            var convertedResult = result as NotFoundObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expected = false;
            var actual = returnValue.IsSuccess;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async void UpdateFranchiesPromocodeReturnValidResultsCancelPromocode()
        {
            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", AllocatedDate = new DateTime(2020, 12, 10), CancellationReason = "cancelled", CancelledDate = new DateTime(2020, 12, 10), CancelledUser = "admin", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };

            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();


            mockfranchisePromocodeService
                .Setup(service => service.Update(code))
                .ReturnsAsync(code);

            mockfranchisePromocodeService
               .Setup(service => service.GetPromocode("C1"))
               .ReturnsAsync(promocode);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Update(code);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;
            var returnValue = Assert.IsType<FranchisePromocode>(convertedResult.Value);

            Assert.NotNull(returnValue);

            var expectedAllocatedUser = "User1";
            var expectedAllocatedDate = new DateTime(2020, 12, 10);
            var expectedCancelledDate = new DateTime(2020, 12, 10);
            var expected = "F1";
            var expectedCancellationReason = "cancelled";
            var expectedCancelledUser = "admin";

            Assert.Equal(expected, returnValue.FranchiseName);
            Assert.Equal(expectedCancellationReason, returnValue.CancellationReason);
            Assert.Equal(expectedCancelledUser, returnValue.CancelledUser);
            Assert.Equal(expectedAllocatedUser, returnValue.AllocatedUser);
            Assert.Equal(expectedAllocatedDate, returnValue.AllocatedDate);
            Assert.Equal(expectedCancelledDate, returnValue.CancelledDate);
        }

        [Fact]
        public async void DeleteFranchiesPromocodeReturnValidResults()
        {
            var mockFranchise = new List<FranchisePromocode>
            {
                new FranchisePromocode { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true},
                new FranchisePromocode { CampaignName = "C2", FranchiseName = "F2", AllocatedPromoCode = 5, TotalPromoCode = 50, Location = "L2", AllocatedUser = "User2", IsActive = true},
                new FranchisePromocode { CampaignName = "C3", FranchiseName = "F3", AllocatedPromoCode = 600, TotalPromoCode = 200, Location = "L3", AllocatedUser = "User1", IsActive = true}
            };

            FranchisePromocode code = new FranchisePromocode() { CampaignName = "C1", FranchiseName = "F1", AllocatedPromoCode = 10, TotalPromoCode = 20, Location = "L1", AllocatedUser = "User1", IsActive = true };
            PromoCodeModel promocode = new PromoCodeModel() { CampaignName = "C1", NoOfPromoCodes = 50, EndDate = new DateTime(2021, 1, 25, 10, 30, 45) };
            string id = "1";
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();


            mockfranchisePromocodeService
                .Setup(service => service.GetAllocatedFranchise(id))
                .ReturnsAsync(mockFranchise);

            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Delete(id);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;
            var returnValue = Assert.IsType<Response>(convertedResult.Value);
        }
        [Fact]
        public async void DeleteFranchiesPromocodeReturnNotValidResults()
        {
            string id = It.IsAny<string>();
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockfranchisePromocodeService = mockRepository.Create<IFranchisePromocodeService>();


            var franchisePromocodeController = new FranchisePromocodeController(mockfranchisePromocodeService.Object);
            var result = await franchisePromocodeController.Delete(id);

            Assert.NotNull(result);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
