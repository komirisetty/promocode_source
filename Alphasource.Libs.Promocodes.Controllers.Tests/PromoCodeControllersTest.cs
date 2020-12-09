using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using Alphasource.Libs.Promocodes.Controllers;


namespace Alphasource.Lib.Promocodes.Controllers.Tests
{
    public class PromoCodeControllersTest
    {
        [Fact]
        public async void ShouldSearchCmapaignBySearchStringReturnValidResult()
        {
            var searchString = "Diwali";
            var mockCampaigns = new List<PromoCodeModel>
            {
                new PromoCodeModel{ _id ="001",CampaignName="Diwali",NoOfPromoCodes=10,
                    PromocodeCost=75,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DW",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0},
                                new PromoCodeModel{ _id ="002",CampaignName="New Year",NoOfPromoCodes=15,
                    PromocodeCost=50,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020NY",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0},
                                                new PromoCodeModel{ _id ="003",CampaignName="Dashara",NoOfPromoCodes=16,
                    PromocodeCost=30,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DA",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0}
            };

            var mockRepository = new MockRepository(MockBehavior.Default);

            var mockPromoCodeDomainServices = mockRepository.Create<IPromoCodeDoaminServices>();

            mockPromoCodeDomainServices.Setup(s => s.SearchCampaign(searchString)).ReturnsAsync(mockCampaigns);

            var promocodeController = new PromoCodeController(mockPromoCodeDomainServices.Object);

            var result = await promocodeController.SearchCampaign(searchString);

            Assert.NotNull(result);

            var convertedResults = result as OkObjectResult;

            Assert.NotNull(convertedResults);

            var actualCampaignList = convertedResults.Value as IEnumerable<PromoCodeModel>;

            Assert.NotNull(actualCampaignList);

            var expectedNoOfCampaigns = 3;

            var actualNoOfCampaigns = actualCampaignList.Count();

            Assert.Equal(expectedNoOfCampaigns, actualNoOfCampaigns);

            var expectedCampaignName = "Diwali";

            var actualCustomerName = actualCampaignList.First().CampaignName;

            Assert.Equal(expectedCampaignName, actualCustomerName);
        }


        [Fact]
        public async void ShouldGetPromoCodeByCampaignNameReturnValidResult()
        {
            var campaignName = "New Year";
            var mockCampaigns = new List<PromoCodeModel>
            {
                new PromoCodeModel{ _id ="001",CampaignName="Diwali",NoOfPromoCodes=10,
                    PromocodeCost=75,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DW",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0},
                                new PromoCodeModel{ _id ="002",CampaignName="New Year",NoOfPromoCodes=15,
                    PromocodeCost=50,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020NY",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0},
                                                new PromoCodeModel{ _id ="003",CampaignName="Dashara",NoOfPromoCodes=16,
                    PromocodeCost=30,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DA",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0}
            };

            var mockRepository = new MockRepository(MockBehavior.Default);

            var mockPromoCodeDomainServices = mockRepository.Create<IPromoCodeDoaminServices>();

            mockPromoCodeDomainServices.Setup(s => s.GetPromoCodeByCampaign(campaignName)).ReturnsAsync(mockCampaigns);

            var promocodeController = new PromoCodeController(mockPromoCodeDomainServices.Object);

            var result = await promocodeController.GetPromoCodeByCampaign(campaignName);

            Assert.NotNull(result);

            var convertedResults = result as OkObjectResult;

            Assert.NotNull(convertedResults);

            var actualCampaignList = convertedResults.Value as IEnumerable<PromoCodeModel>;

            Assert.NotNull(actualCampaignList);

            var expectedNoOfCampaigns = 3;

            var actualNoOfCampaigns = actualCampaignList.Count();

            Assert.Equal(expectedNoOfCampaigns, actualNoOfCampaigns);

            var expectedCampaignName = "New Year";

            var actualCustomerName = actualCampaignList.First(p => p.CampaignName == campaignName).CampaignName;

            Assert.Equal(expectedCampaignName, actualCustomerName);
        }



        [Fact]
        public void ShouldGetAllCampaignsList()
        {
            var mockCampaigns = new List<PromoCodeModel>
            {
                new PromoCodeModel{ _id ="001",CampaignName="Diwali",NoOfPromoCodes=10,
                    PromocodeCost=75,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DW",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0},
                                new PromoCodeModel{ _id ="002",CampaignName="New Year",NoOfPromoCodes=15,
                    PromocodeCost=50,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020NY",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0},
                                                new PromoCodeModel{ _id ="003",CampaignName="Dashara",NoOfPromoCodes=16,
                    PromocodeCost=30,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DA",PromocodeGenerated="",
                    Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
                CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0}
            };

            var mockRepository = new MockRepository(MockBehavior.Default);

            var mockPromoCodeDomainServices = mockRepository.Create<IPromoCodeDoaminServices>();

            mockPromoCodeDomainServices.Setup(s => s.viewCampagin()).Returns(mockCampaigns);

            var promocodeController = new PromoCodeController(mockPromoCodeDomainServices.Object);

            var result = promocodeController.ViewAllCampaigns();

            Assert.NotNull(result);

            var convertedResults = result as OkObjectResult;

            Assert.NotNull(convertedResults);

            var actualCampaignList = convertedResults.Value as IEnumerable<PromoCodeModel>;

            Assert.NotNull(actualCampaignList);

            var expectedNoOfCampaigns = 3;

            var actualNoOfCampaigns = actualCampaignList.Count();

            Assert.Equal(expectedNoOfCampaigns, actualNoOfCampaigns);
        }

        [Fact]
        public void ShouldInsertNewCampaignRecord()
        {

            //var mockInsertDate = new List<PromoCodeModel>
            //{
            //    new PromoCodeModel{ _id ="001",CampaignName="Diwali",NoOfPromoCodes=10,
            //        PromocodeCost=75,StartDate=DateTime.Now,EndDate=DateTime.Now,Prefix="2020DW",PromocodeGenerated="",
            //        Remarks="Test",CampaignCreatedDate=DateTime.Now,CreatedUserName="Prasad",CampaignStatus="Inactive",
            //    CancellaionResaon="Limit reached",CancelledDate=DateTime.Now,CancelledUser="Vara",PortalPercentage=30,FranschisePercentage=25,ChefPercentage=0}
            //};

            PromoCodeModel insertdate = new PromoCodeModel
            {
                _id = "001",
                CampaignName = "Diwali",
                NoOfPromoCodes = 10,
                PromocodeCost = 75,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Prefix = "2020DW",
                PromocodeGenerated = "",
                Remarks = "Test",
                CampaignCreatedDate = DateTime.Now,
                CreatedUserName = "Prasad",
                CampaignStatus = "Inactive",
                CancellaionResaon = "Limit reached",
                CancelledDate = DateTime.Now,
                CancelledUser = "Vara",
                PortalPercentage = 30,
                FranschisePercentage = 25,
                ChefPercentage = 0
            };


            var mockInsertRepository = new MockRepository(MockBehavior.Default);

            var mockPromoCodeDomainServices = mockInsertRepository.Create<IPromoCodeDoaminServices>();

            mockPromoCodeDomainServices.Setup(s => s.AddNewCampaign(insertdate)).Returns(insertdate);

            var promocodeController = new PromoCodeController(mockPromoCodeDomainServices.Object);

            var result = promocodeController.CreateNewCampaign(insertdate);

            Assert.NotNull(result);

            var convertedResults = result as OkObjectResult;

            Assert.NotNull(convertedResults);

            var actualCampaignList = convertedResults.Value as PromoCodeModel;

            Assert.NotNull(actualCampaignList);

            //var expectedNoOfCampaigns = 1;

            //var actualNoOfCampaigns = actualCampaignList.Count();

            //Assert.Equal(expectedNoOfCampaigns, actualNoOfCampaigns);

        }


        [Fact]
        public void ShouldUpdateExistingCampaignRecord()
        {

            var existingRecord = new PromoCodeModel
            {
                _id = "001",
                CampaignName = "Diwali",
                NoOfPromoCodes = 10,
                PromocodeCost = 75,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Prefix = "2020DW",
                PromocodeGenerated = "",
                Remarks = "Test",
                CampaignCreatedDate = DateTime.Now,
                CreatedUserName = "Prasad",
                CampaignStatus = "Inactive",
                CancellaionResaon = "Limit reached",
                CancelledDate = DateTime.Now,
                CancelledUser = "Vara",
                PortalPercentage = 30,
                FranschisePercentage = 25,
                ChefPercentage = 0
            };


            PromoCodeModel updatedRecord = new PromoCodeModel
            {
                _id = "001",
                CampaignName = "Diwali",
                NoOfPromoCodes = 10,
                PromocodeCost = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Prefix = "2020DW",
                PromocodeGenerated = "",
                Remarks = "Test",
                CampaignCreatedDate = DateTime.Now,
                CreatedUserName = "Prasad",
                CampaignStatus = "Inactive",
                CancellaionResaon = "Limit reached",
                CancelledDate = DateTime.Now,
                CancelledUser = "Vara",
                PortalPercentage = 30,
                FranschisePercentage = 25,
                ChefPercentage = 0
            };


            var mockInsertRepository = new MockRepository(MockBehavior.Default);

            var mockPromoCodeDomainServices = mockInsertRepository.Create<IPromoCodeDoaminServices>();

            mockPromoCodeDomainServices.Setup(s => s.UpdateCampaign(updatedRecord)).Returns(updatedRecord);

            var promocodeController = new PromoCodeController(mockPromoCodeDomainServices.Object);

            var result = promocodeController.UpdateCampaign(updatedRecord);

            Assert.NotNull(result);

            var convertedResults = result as OkObjectResult;

            Assert.NotNull(convertedResults);

            var actualCampaignList = convertedResults.Value as PromoCodeModel;

            Assert.NotNull(actualCampaignList);

            //var expectedNoOfCampaigns = 1;

            //var actualNoOfCampaigns = actualCampaignList.Count();

            //Assert.Equal(expectedNoOfCampaigns, actualNoOfCampaigns);

        }
    }
}
