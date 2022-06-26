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
using OngProject.Core.Models.DTOs.Slides;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class SlidesControllerTest
    {
        private ISlidesRepository slidesRepository;
        private ISlidesBusiness slidesBusiness;
        private SlidesController slidesController;
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
                    mc.AddProfile(new SlideMappingProfile());
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

            slidesRepository = new SlidesRepository(ContextHelper.DbContext);
            slidesBusiness = new SlidesBusiness(ContextHelper.UnitOfWork, slidesRepository);
            
            slidesController = new SlidesController(slidesBusiness, _mapper, aws);
        }

        [TestMethod]
        public void SlidesGetAll_Ok()
        {
            var response = slidesController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SlidesGet_Ok()
        {
            var response = await slidesController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SlidesGet_NotFound()
        {
            var response = await slidesController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task SlidesCreate_Ok()
        {
            var model = new SlideRequest
            {
                Name = "Name",
                Text = "Text",
                ImageUrl = ImageHelper.CreateImage("logo512.png"),
                Order = 1,
                OrganizationId = 1
            };

            var response = await slidesController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SlidesCreate_BadRequest()
        {
            var response = await slidesController.Create(null);
            var result = response as BadRequestObjectResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task SlidesEdit_Ok()
        {
            var model = new SlideRequest
            {
                Name = "Name",
                Text = "Text",
                ImageUrl = ImageHelper.CreateImage("logo512.png"),
                Order = 1,
                OrganizationId = 1
            };

            var response = await slidesController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SlidesEdit_NotFound()
        {
            var model = new SlideRequest
            {
                Name = "Name",
                Text = "Text",
                ImageUrl = ImageHelper.CreateImage("logo512.png"),
                Order = 1,
                OrganizationId = 1
            };

            var response = await slidesController.Edit(-1, model);

            var result = response as NotFoundObjectResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task SlidesDelete_Ok()
        {
            var response = await slidesController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SlidesDelete_NotFound()
        {
            var response = await slidesController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
