using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.News;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class NewsControllerTest
    {
        private INewsRepository newsRepository;
        private INewsBusiness newsBusiness;
        private NewsController newsController;
        private IMapper _mapper;

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

            newsRepository = new NewsRepository(ContextHelper.DbContext);
            newsBusiness = new NewsBusiness(ContextHelper.UnitOfWork, newsRepository);
            newsController = new NewsController(newsBusiness, _mapper);
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
                Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
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
                Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
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
                Image = "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX",
                Content = "No news, good news",
                CategoryId = 1
            };

            var response = await newsController.Edit(-1, model);
            
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
