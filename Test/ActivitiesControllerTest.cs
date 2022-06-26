using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class ActivitiesControllerTest
    {
        private IActivitiesRepository activitiesRepository;
        private IActivitiesBusiness activitiesBusiness;
        private ActivitiesController activitiesController;
        private IMapper _mapper;

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

            activitiesRepository = new ActivitiesRepository(ContextHelper.DbContext);
            activitiesBusiness = new ActivitiesBusiness(ContextHelper.UnitOfWork, activitiesRepository);
            activitiesController = new ActivitiesController(activitiesBusiness, _mapper);
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
                Image = "https://d3hjzzsa8cr26l.cloudfront.net/fdf731b0-ac0d-11eb-80db-158c47a6e2fc.jpg",
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
                Image = "https://d3hjzzsa8cr26l.cloudfront.net/fdf731b0-ac0d-11eb-80db-158c47a6e2fc.jpg",
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
                Image = "https://d3hjzzsa8cr26l.cloudfront.net/fdf731b0-ac0d-11eb-80db-158c47a6e2fc.jpg",
            };

            var response = await activitiesController.Edit(-1, model);
            
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
