using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.News;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class NewsControllerTest
    {
        private INewsRepository newsRepository;
        private INewsBusiness newsBusiness;
        private NewsController newsController;
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
                    mc.AddProfile(new NewsMappingProfile());
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

            newsRepository = new NewsRepository(ContextHelper.DbContext);
            newsBusiness = new NewsBusiness(ContextHelper.UnitOfWork, newsRepository);
            newsController = new NewsController(newsBusiness, _mapper, aws);
        }

        [TestMethod]
        public void NewsGetAll_Ok()
        {
            var response = newsController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task NewsGet_Ok()
        {
            var response = await newsController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task NewsGet_NotFound()
        {
            var response = await newsController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task NewsCreate_Ok()
        {
            var model = new NewsRequest
            {
                Name = "No news",
                Image = ImageHelper.CreateImage("logo512.png"),
                Content = "No news, good news",
                CategoryId = 1
            };

            var response = await newsController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task NewsCreate_BadRequest()
        {
            var response = await newsController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task NewsEdit_Ok()
        {
            var model = new NewsRequest
            {
                Name = "No news",
                Image = ImageHelper.CreateImage("logo512.png"),
                Content = "No news, good news",
                CategoryId = 1
            };

            var response = await newsController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task NewsEdit_NotFound()
        {
            var model = new NewsRequest
            {
                Name = "No news",
                Image = ImageHelper.CreateImage("logo512.png"),
                Content = "No news, good news",
                CategoryId = 1
            };

            var response = await newsController.Edit(-1, model);

            var result = response as NotFoundObjectResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task NewsDelete_Ok()
        {
            var response = await newsController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task NewsDelete_NotFound()
        {
            var response = await newsController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
