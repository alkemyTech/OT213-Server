using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Business;
using OngProject.Core.Business.Auth;
using OngProject.Core.Business.Mail;
using OngProject.Core.Business.Mail.Interfaces;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Repositories;
using OngProject.Repositories.Auth;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;
using OngProject.Repositories.Mail;
using OngProject.Repositories.Mail.Interfaces;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test
{
    [TestClass]
    public class AuthControllerTest
    {
        private IUsersRepository usersRepository;
        private IUsersBusiness usersBusiness;

        private IAuthBusiness authBusiness;
        private IAuthRepository authRepository;

        private IMailRepository mailRepository;
        private IMailBusiness mailBusiness;

        private ITokenService tokenService;
        private IHttpContextAccessor accessor;

        private IMapper _mapper;

         private AuthenticationController authenticationontroller;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AuthMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            IConfiguration config = new ConfigurationBuilder().Build();

            tokenService = new TokenService(config);

            accessor = new HttpContextAccessor();

            mailRepository = new MailRepository(config);
            mailBusiness = new MailBusiness(mailRepository);

            usersRepository = new UsersRepository(ContextHelper.DbContext);
            usersBusiness = new UsersBusiness(ContextHelper.UnitOfWork, usersRepository);

            authRepository = new AuthRepository(ContextHelper.DbContext);
            authBusiness = new AuthBusiness(ContextHelper.UnitOfWork, authRepository);

            authenticationontroller = new AuthenticationController(authBusiness, mailBusiness, _mapper, tokenService, accessor, usersBusiness);
        }

        [TestMethod]
        public async Task AuthRegister_Ok()
        {
            var model = new UserAuthDTO
            {
                Email = "test@test.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "123456",
                Photo = ImageHelper.CreateImage("logo512.png")
            };

            var response = await authenticationontroller.Register(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AuthRegister_Unauthorized()
        {
            var model = new UserAuthDTO
            {
                Email = "test12@test.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "12345888888",
                Photo = ImageHelper.CreateImage("logo512.png")
            };

            var response = await authenticationontroller.Register(model);
            var result = response as UnauthorizedResult;
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task AuthRegister_BadRequest()
        {
            var model = new UserAuthDTO
            {
                Email = "test@test.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "12345689",
                Photo = ImageHelper.CreateImage("logo512.png")
            };

            var response = await authenticationontroller.Register(model);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task AuhtLogin_Ok()
        {
            var model = new UserAuthLoginDTO
            {
            };

            var response = await authenticationontroller.Login(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AuthLogin_Unauthorized()
        {
            var response = await authenticationontroller.Login(null);
            var result = response as UnauthorizedResult;
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void AuthMe_Ok()
        {
            var response = authenticationontroller.GetMe();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AuthMe_BadRequest()
        {
            var response = authenticationontroller.GetMe();
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}

