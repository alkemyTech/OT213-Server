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
using OngProject.Core.Models.DTOs.Categories;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private ICategoriesRepository categoriesRepository;
        private ICategoriesBusiness categoriesBusiness;
        private CategoriesController categoriesController;
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
                    mc.AddProfile(new CategoryMappingProfile());
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

            categoriesRepository = new CategoriesRepository(ContextHelper.DbContext);
            categoriesBusiness = new CategoriesBusiness(ContextHelper.UnitOfWork, categoriesRepository);
            categoriesController = new CategoriesController(categoriesBusiness, _mapper, aws);
        }

        [TestMethod]
        public void CategoriesGetAll_Ok()
        {
            var response = categoriesController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CategoriesGet_Ok()
        {
            var response = await categoriesController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CategoriesGet_NotFound()
        {
            var response = await categoriesController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task CategoriesCreate_Ok()
        {
            var model = new CategoryRequest
            {
                Name = "Animals and pets",
                Image = ImageHelper.CreateImage("logo512.png"),
                Description = "Animals and pets"
            };

            var response = await categoriesController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CategoriesCreate_BadRequest()
        {
            var response = await categoriesController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task CategoriesEdit_Ok()
        {
            var model = new CategoryRequest
            {
                Name = "Animals and pets",
                Image = ImageHelper.CreateImage("logo512.png"),
                Description = "Animals and pets"
            };

            var response = await categoriesController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CategoriesEdit_NotFound()
        {
            var model = new CategoryRequest
            {
                Name = "Animals and pets",
                Image = ImageHelper.CreateImage("logo512.png"),
                Description = "Animals and pets"
            };

            var response = await categoriesController.Edit(-1, model);

            var result = response as NotFoundObjectResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task CategoriesDelete_Ok()
        {
            var response = await categoriesController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CategoriesDelete_NotFound()
        {
            var response = await categoriesController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
