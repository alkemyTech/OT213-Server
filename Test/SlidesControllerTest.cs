using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Slides;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class SlidesControllerTest
    {
        private ISlidesRepository slidesRepository;
        private ISlidesBusiness slidesBusiness;
        private SlidesController slidesController;
        private IMapper _mapper;

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

            slidesRepository = new SlidesRepository(ContextHelper.DbContext);
            slidesBusiness = new SlidesBusiness(ContextHelper.UnitOfWork, slidesRepository);
            slidesController = new SlidesController(slidesBusiness, _mapper);
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
                ImageUrl = "ImageUrl",
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
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task SlidesEdit_Ok()
        {
            var model = new SlideRequest
            {
                Name = "Name",
                Text = "Text",
                ImageUrl = "ImageUrl",
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
                ImageUrl = "ImageUrl",
                Order = 1,
                OrganizationId = 1
            };

            var response = await slidesController.Edit(-1, model);
            
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
