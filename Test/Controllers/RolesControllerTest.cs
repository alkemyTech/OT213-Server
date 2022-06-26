using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Roles;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class RolesControllerTest
    {
        private IRoleRepository rolesRepository;
        private IRolesBusiness rolesBusiness;
        private RolesController rolesController;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new RoleMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            rolesRepository = new RolesRepository(ContextHelper.DbContext);
            rolesBusiness = new RolesBusiness(ContextHelper.UnitOfWork, rolesRepository);
            rolesController = new RolesController(rolesBusiness, _mapper);
        }

        [TestMethod]
        public void RolesGetAll_Ok()
        {
            var response = rolesController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RolesGet_Ok()
        {
            var response = await rolesController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RolesGet_NotFound()
        {
            var response = await rolesController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RolesCreate_Ok()
        {
            var model = new RoleRequest
            {
                Name = "Admin"
            };

            var response = await rolesController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RolesCreate_BadRequest()
        {
            var response = await rolesController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task RolesEdit_Ok()
        {
            var model = new RoleRequest
            {
                Name = "Admin"
            };

            var response = await rolesController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RolesEdit_NotFound()
        {
            var model = new RoleRequest
            {
                Name = "Admin"
            };

            var response = await rolesController.Edit(-1, model);

            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RolesDelete_Ok()
        {
            var response = await rolesController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RolesDelete_NotFound()
        {
            var response = await rolesController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
