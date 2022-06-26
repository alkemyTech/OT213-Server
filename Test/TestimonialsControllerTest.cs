using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Testimonials;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class TestimonialsControllerTest
    {
        private ITestimonialsRepository testimonialsRepository;
        private ITestimonialsBusiness testimonialsBusiness;
        private TestimonialsController testimonialsController;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TestimonialMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                 _mapper = mapper;
            }

            testimonialsRepository = new TestimonialsRepository(ContextHelper.DbContext);
            testimonialsBusiness = new TestimonialsBusiness(ContextHelper.UnitOfWork, testimonialsRepository);
            testimonialsController = new TestimonialsController(testimonialsBusiness, _mapper);
        }

        [TestMethod]
        public void TestimonialsGetAll_Ok()
        {
            var response = testimonialsController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestimonialsGet_Ok()
        {
            var response = await testimonialsController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestimonialsGet_NotFound()
        {
            var response = await testimonialsController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TestimonialsCreate_Ok()
        {
            var model = new TestimonialRequest
            {
                Name = "Kathie D Green",
                Image = "https://www.fakepersongenerator.com/Face/female/female1022482473236.jpg",
                Description = "Thank You! Just what I was looking for. All good. Ong Project Alkemy was worth a fortune to my company."
            };

            var response = await testimonialsController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestimonialsCreate_BadRequest()
        {
            var response = await testimonialsController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task TestimonialsEdit_Ok()
        {
            var model = new TestimonialRequest
            {
                Name = "Kathie D Green",
                Image = "https://www.fakepersongenerator.com/Face/female/female1022482473236.jpg",
                Description = "Thank You! Just what I was looking for. All good. Ong Project Alkemy was worth a fortune to my company."
            };

            var response = await testimonialsController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestimonialsEdit_NotFound()
        {
            var model = new TestimonialRequest
            {
                Name = "Kathie D Green",
                Image = "https://www.fakepersongenerator.com/Face/female/female1022482473236.jpg",
                Description = "Thank You! Just what I was looking for. All good. Ong Project Alkemy was worth a fortune to my company."
            };

            var response = await testimonialsController.Edit(-1, model);
            
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TestimonialsDelete_Ok()
        {
            var response = await testimonialsController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestimonialsDelete_NotFound()
        {
            var response = await testimonialsController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
