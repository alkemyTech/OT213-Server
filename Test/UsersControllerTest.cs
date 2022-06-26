using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Users;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class UsersControllerTest
    {
        private IUsersRepository usersRepository;
        private IUsersBusiness usersBusiness;
        private UsersController usersController;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new UserMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                 _mapper = mapper;
            }

            usersRepository = new UsersRepository(ContextHelper.DbContext);
            usersBusiness = new UsersBusiness(ContextHelper.UnitOfWork, usersRepository);
            usersController = new UsersController(usersBusiness, _mapper);
        }

        [TestMethod]
        public void UsersGetAll_Ok()
        {
            var response = usersController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersGet_Ok()
        {
            var response = await usersController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersGet_NotFound()
        {
            var response = await usersController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UsersCreate_Ok()
        {
            var model = new UserRequest
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Email = "Email1@email.com",
                Password = "Password1",
                Photo = "Photo1",
                RoleId = 2
            };

            var response = await usersController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersCreate_BadRequest()
        {
            var response = await usersController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UsersEdit_Ok()
        {
            var model = new UserRequest
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Email = "Email1@email.com",
                Password = "Password1",
                Photo = "Photo1",
                RoleId = 2
            };

            var response = await usersController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersEdit_NotFound()
        {
            var model = new UserRequest
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Email = "Email1@email.com",
                Password = "Password1",
                Photo = "Photo1",
                RoleId = 2
            };

            var response = await usersController.Edit(-1, model);
            
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UsersDelete_Ok()
        {
            var response = await usersController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersDelete_NotFound()
        {
            var response = await usersController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
