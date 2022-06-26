using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Comments;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class CommentsControllerTest
    {
        private ICommentsRepository commentsRepository;
        private ICommentsBusiness commentsBusiness;
        private CommentsController commentsController;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new CommentMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                 _mapper = mapper;
            }

            commentsRepository = new CommentRepository(ContextHelper.DbContext);
            commentsBusiness = new CommentsBusiness(ContextHelper.UnitOfWork, commentsRepository);
            commentsController = new CommentsController(commentsBusiness, _mapper);
        }

        [TestMethod]
        public void CommentsGetAll_Ok()
        {
            var response = commentsController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CommentsGet_Ok()
        {
            var response = await commentsController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CommentsGet_NotFound()
        {
            var response = await commentsController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task CommentsCreate_Ok()
        {
            var model = new CommentRequest
            {
                UserId = 1,
                NewsId =1,
                Body = "Test"
            };

            var response = await commentsController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CommentsCreate_BadRequest()
        {
            var response = await commentsController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task CommentsEdit_Ok()
        {
            var model = new CommentRequest
            {
                UserId = 1,
                NewsId = 1,
                Body = "Test"
            };

            var response = await commentsController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CommentsEdit_NotFound()
        {
            var model = new CommentRequest
            {
                UserId = 1,
                NewsId = 1,
                Body = "Test"
            };

            var response = await commentsController.Edit(-1, model);
            
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CommentsDelete_Ok()
        {
            var response = await commentsController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CommentsDelete_NotFound()
        {
            var response = await commentsController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
