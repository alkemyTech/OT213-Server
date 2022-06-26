using Amazon;
using Amazon.Runtime;
using Amazon.S3;
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
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
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

        private IAmazonS3 amazonS3;
        private IAmazonHelperService aws;

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
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();
            amazonS3 = new AmazonS3Client(new AnonymousAWSCredentials(), new AmazonS3Config { RegionEndpoint = RegionEndpoint.EUWest1 });

            aws = new AmazonHelperService(amazonS3, config);

            tokenService = new TokenService(config);

            accessor = new HttpContextAccessor();

            mailRepository = new MailRepository(config);
            mailBusiness = new MailBusiness(mailRepository);

            usersRepository = new UsersRepository(ContextHelper.DbContext);
            usersBusiness = new UsersBusiness(ContextHelper.UnitOfWork, usersRepository);

            authRepository = new AuthRepository(ContextHelper.DbContext);
            authBusiness = new AuthBusiness(ContextHelper.UnitOfWork, authRepository);

            authenticationontroller = new AuthenticationController(authBusiness, mailBusiness, _mapper, tokenService, accessor, usersBusiness, aws);
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
                ImgFile = ImageHelper.CreateImage("logo512.png")
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
                ImgFile = ImageHelper.CreateImage("logo512.png")
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
                ImgFile = ImageHelper.CreateImage("logo512.png")
            };

            var response = await authenticationontroller.Register(model);
            var result = response as BadRequestObjectResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AuhtLogin_Ok()
        {
            var model = new UserAuthLoginDTO
            {
                Email = "test@test.com",
                Password = "12345678"
            };

            var response = await authenticationontroller.Login(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AuthLogin_Unauthorized()
        {
            var model = new UserAuthLoginDTO
            {
                Email = "test1@test.com",
                Password = "12345678"
            };

            var response = await authenticationontroller.Login(model);
            var result = response as UnauthorizedResult;
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }
    }
}

