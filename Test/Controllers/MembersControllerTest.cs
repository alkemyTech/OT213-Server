using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;
using Test.Mapper;

namespace Test.Controllers
{
    [TestClass]
    public class MembersControllerTest
    {
        private IMembersRepository membersRepository;
        private IMembersBusiness membersBusiness;
        private MembersController membersController;
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
                    mc.AddProfile(new MemberMappingProfile());
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

            membersRepository = new MembersRepository(ContextHelper.DbContext);
            membersBusiness = new MembersBusiness(ContextHelper.UnitOfWork, membersRepository);
            membersController = new MembersController(membersBusiness, _mapper, aws);
        }

        [TestMethod]
        public void MembersGetAll_Ok()
        {
            var response = membersController.GetAll();
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task MembersGet_Ok()
        {
            var response = await membersController.Get(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task MembersGet_NotFound()
        {
            var response = await membersController.Get(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task MembersCreate_Ok()
        {
            var model = new MemberRequest
            {
                Name = "John Petrucci",
                FacebookUrl = "https://www.facebook.com/johnPetru1",
                InstagramUrl = "https://www.instagram.com/johnPetru1",
                LinkedInUrl = "https://www.linkedin.com/in/john-petrucci",
                ImageUrl = ImageHelper.CreateImage("logo512.png"),
                Description = "Miembro activo de la organización"
            };

            var response = await membersController.Create(model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task MembersCreate_BadRequest()
        {
            var response = await membersController.Create(null);
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task MembersEdit_Ok()
        {
            var model = new MemberRequest
            {
                Name = "John Petrucci",
                FacebookUrl = "https://www.facebook.com/johnPetru1",
                InstagramUrl = "https://www.instagram.com/johnPetru1",
                LinkedInUrl = "https://www.linkedin.com/in/john-petrucci",
                ImageUrl = ImageHelper.CreateImage("logo512.png"),
                Description = "Miembro activo de la organización"
            };

            var response = await membersController.Edit(1, model);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task MembersEdit_NotFound()
        {
            var model = new MemberRequest
            {
                Name = "John Petrucci",
                FacebookUrl = "https://www.facebook.com/johnPetru1",
                InstagramUrl = "https://www.instagram.com/johnPetru1",
                LinkedInUrl = "https://www.linkedin.com/in/john-petrucci",
                ImageUrl = ImageHelper.CreateImage("logo512.png"),
                Description = "Miembro activo de la organización"
            };

            var response = await membersController.Edit(-1, model);

            var result = response as NotFoundObjectResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task MembersDelete_Ok()
        {
            var response = await membersController.SoftDelete(1);
            var result = response as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task MembersDelete_NotFound()
        {
            var response = await membersController.SoftDelete(-1);
            var result = response as NotFoundResult;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
