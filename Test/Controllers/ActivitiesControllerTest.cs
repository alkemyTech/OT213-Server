using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class ActivitiesControllerTest
    {
        private IActivitiesRepository activitiesRepository;
        private IActivitiesBusiness activitiesBusiness;
        private ActivitiesController activitiesController;
        private IMapper _mapper;

        private IAmazonS3 amazonS3;
        private IAmazonHelperService aws;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ActivityMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();

            amazonS3 = new AmazonS3Client(new AnonymousAWSCredentials(), new AmazonS3Config { RegionEndpoint = RegionEndpoint.EUWest1 });
            aws = new AmazonHelperService(amazonS3, config);

            activitiesRepository = new ActivitiesRepository(ContextHelper.DbContext);
            activitiesBusiness = new ActivitiesBusiness(ContextHelper.UnitOfWork, activitiesRepository);
            activitiesController = new ActivitiesController(activitiesBusiness, _mapper, aws);
        }

        [TestMethod]
        public void ActivitiesGetAll_Ok()
        {
            var response = activitiesController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ActivitiesGet_Ok()
        {
            var response = await activitiesController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ActivitiesGet_NotFound()
        {
            var response = await activitiesController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ActivitiesCreate_Ok()
        {
            var model = new ActivityRequest
            {
                Name = "Test Name",
                Content = "Test Content",
                Image = ImageHelper.CreateImage("logo512.png")
            };

            var response = await activitiesController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ActivitiesCreate_BadRequest()
        {
            var response = await activitiesController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task ActivitiesEdit_Ok()
        {
            var model = new ActivityRequest
            {
                Name = "Test Name",
                Content = "Test Content",
                Image = ImageHelper.CreateImage("logo512.png")
            };

            var response = await activitiesController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ActivitiesEdit_NotFound()
        {
            var model = new ActivityRequest
            {
                Name = "Test Namee 1",
                Content = "Test Content 1",
                Image = ImageHelper.CreateImage("logo512.png")
            };

            var response = await activitiesController.Edit(-1, model);

            var result = response as NotFoundObjectResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task ActivitiesDelete_Ok()
        {
            var response = await activitiesController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ActivitiesDelete_NotFound()
        {
            var response = await activitiesController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
