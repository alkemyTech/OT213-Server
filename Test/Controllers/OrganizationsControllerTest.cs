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
using OngProject.Core.Models.DTOs.Organizations;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class OrganizationsControllerTest
    {
        private IOrganizationsRepository organizationsRepository;
        private IOrganizationsBusiness organizationsBusiness;
        private OrganizationsController organizationsController;
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
                    mc.AddProfile(new OrganizationMappingProfile());
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

            organizationsRepository = new OrganizationsRepository(ContextHelper.DbContext);
            organizationsBusiness = new OrganizationsBusiness(ContextHelper.UnitOfWork, organizationsRepository);
            organizationsController = new OrganizationsController(organizationsBusiness, _mapper, aws);
        }

        [TestMethod]
        public void OrganizationsGetAll_Ok()
        {
            var response = organizationsController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task OrganizationsGet_Ok()
        {
            var response = await organizationsController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task OrganizationsGet_NotFound()
        {
            var response = await organizationsController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task OrganizationsCreate_Ok()
        {
            var model = new OrganizationRequest
            {
                Name = "Ong Somos Más",
                Image = ImageHelper.CreateImage("logo512.png"),
                Email = "Email",
                Welcome = "Welcome",
                Address = "Address",
                Phone = 123,
                AboutUs = "AboutUs",
                FacebookUrl = "FacebookUrl",
                InstagramUrl = "InstagramUrl",
                LinkedInUrl = "LinkedInUrl"
            };

            var response = await organizationsController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task OrganizationsCreate_BadRequest()
        {
            var response = await organizationsController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task OrganizationsEdit_Ok()
        {
            var model = new OrganizationRequest
            {
                Name = "Ong Somos Más",
                Image = ImageHelper.CreateImage("logo512.png"),
                Email = "Email",
                Welcome = "Welcome",
                Address = "Address",
                Phone = 123,
                AboutUs = "AboutUs",
                FacebookUrl = "FacebookUrl",
                InstagramUrl = "InstagramUrl",
                LinkedInUrl = "LinkedInUrl"
            };

            var response = await organizationsController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task OrganizationsEdit_NotFound()
        {
            var model = new OrganizationRequest
            {
                Name = "Ong Somos Más",
                Image = ImageHelper.CreateImage("logo512.png"),
                Email = "Email",
                Welcome = "Welcome",
                Address = "Address",
                Phone = 123,
                AboutUs = "AboutUs",
                FacebookUrl = "FacebookUrl",
                InstagramUrl = "InstagramUrl",
                LinkedInUrl = "LinkedInUrl"
            };

            var response = await organizationsController.Edit(-1, model);

            var result = response as NotFoundObjectResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task OrganizationsDelete_Ok()
        {
            var response = await organizationsController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task OrganizationsDelete_NotFound()
        {
            var response = await organizationsController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
