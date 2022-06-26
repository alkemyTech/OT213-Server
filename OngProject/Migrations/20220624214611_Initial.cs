using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OngProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Welcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutUs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.id);
                    table.ForeignKey(
                        name: "FK_News_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.id);
                    table.ForeignKey(
                        name: "FK_Slides_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comments_News_NewId",
                        column: x => x.NewId,
                        principalTable: "News",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "id", "Content", "CreatedAt", "DeletedAt", "Image", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Attending sporting events", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(6043), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://d3hjzzsa8cr26l.cloudfront.net/c0b7ce93-2d00-11e6-bce7-6ff134176666.jpg", false, "(Social Activities)", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(6060) },
                    { 2, "Puppetry", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8147), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://d3hjzzsa8cr26l.cloudfront.net/fdf731b0-ac0d-11eb-80db-158c47a6e2fc.jpg", false, "(Social Activities)", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8156) },
                    { 3, "Going to the park", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8162), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://d3hjzzsa8cr26l.cloudfront.net/3470a674-6a05-11ea-b459-9d2edb98bc96.jpg", false, "(Social Activities)", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8164) },
                    { 4, "Going to concerts", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8167), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://d3hjzzsa8cr26l.cloudfront.net/340eb0ee-6a05-11ea-b459-9d2edb98bc96.jpg", false, "(Social Activities)", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8169) },
                    { 5, "Volunteering", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8173), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://d3hjzzsa8cr26l.cloudfront.net/cfe95939-2d0d-11e6-a4bd-71dbf5f2854a.jpg", false, "(Social Activities)", new DateTime(2022, 6, 24, 18, 46, 9, 915, DateTimeKind.Local).AddTicks(8175) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "CreatedAt", "Description", "Image", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 24, 18, 46, 9, 919, DateTimeKind.Local).AddTicks(8062), "Animals and pets", "https://www.jg-cdn.com/assets/jg-homepage/339d41967797c7d4f41a0addcc659196.svg", false, "Animals and pets", new DateTime(2022, 6, 24, 18, 46, 9, 919, DateTimeKind.Local).AddTicks(8084) },
                    { 2, new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(280), "Art and culture", "https://www.jg-cdn.com/assets/jg-homepage/187aa4c1ceded5ddeab457ba26c1eb65.svg", false, "Art and culture", new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(291) },
                    { 3, new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(298), "Education", "https://www.jg-cdn.com/assets/jg-homepage/7d2169169f7d1316cec9b5733dd59718.svg", false, "Education", new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(300) },
                    { 4, new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(303), "International aid", "https://www.jg-cdn.com/assets/jg-homepage/675f647d01d3751a0113d305ce2baf8e.svg", false, "International aid", new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(305) },
                    { 5, new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(309), "Disability", "https://www.jg-cdn.com/assets/jg-homepage/cec5dbbde623a23c0e7239e969a366d1.svg", false, "Disability", new DateTime(2022, 6, 24, 18, 46, 9, 920, DateTimeKind.Local).AddTicks(311) }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "id", "CreatedAt", "Description", "FacebookUrl", "ImageUrl", "InstagramUrl", "IsDeleted", "LinkedInUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 5, new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(719), "Miembro activo de la organización", "https://www.facebook.com/juaniGue", "https://th.bing.com/th/id/OIP.P4OqoL2OPCNYbr1uSbYvWAAAAA?w=148&h=152&c=7&r=0&o=5&pid=1.7", "https://www.instagram.com/juaniGue", false, "https://www.linkedin.com/in/juan-guerra", "Juan Guerra", new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(721) },
                    { 4, new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(713), "Miembro activo de la organización", "https://www.facebook.com/joeStriani1", "https://th.bing.com/th/id/OIP.3X4EMm2OGqVqR77JQvJzagAAAA?pid=ImgDet&rs=1", "https://www.instagram.com/joeStriani1", false, "https://www.linkedin.com/in/joe-satriani", "Joe Satriani", new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(715) },
                    { 2, new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(681), "Miembro activo de la organización", "https://www.facebook.com/johnPetru1", "https://magazyngitarzysta.pl/i/images/9/7/6/dz0yNTE4Jmg9MzAwMA==_src_140976-GettyImages-911852516.jpg", "https://www.instagram.com/johnPetru1", false, "https://www.linkedin.com/in/john-petrucci", "John Petrucci", new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(696) },
                    { 1, new DateTime(2022, 6, 24, 18, 46, 9, 905, DateTimeKind.Local).AddTicks(7974), "Miembro activo de la organización", "https://www.facebook.com/daveMust22", "https://gcdn.emol.cl/rock/files/2019/09/megadeth-dave-mustaine.jpg", "https://www.instagram.com/daveMust22", false, "https://www.linkedin.com/in/dave-mustaine", "Dave Mustaine", new DateTime(2022, 6, 24, 18, 46, 9, 907, DateTimeKind.Local).AddTicks(4593) },
                    { 3, new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(706), "Miembro activo de la organización", "https://www.facebook.com/steveVai66", "https://273710-849646-raikfcquaxqncofqfm.stackpathdns.com/wp-content/uploads/2012/07/Steve-Vai-pic.jpg", "https://www.instagram.com/steveVai66", false, "https://www.linkedin.com/in/steve-vai", "Steve Vai", new DateTime(2022, 6, 24, 18, 46, 9, 908, DateTimeKind.Local).AddTicks(709) }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "id", "AboutUs", "Address", "CreatedAt", "Email", "FacebookUrl", "Image", "InstagramUrl", "IsDeleted", "LinkedInUrl", "Name", "Phone", "UpdatedAt", "Welcome" },
                values: new object[] { 1, "Somos Más", "Somos Más", new DateTime(2022, 6, 24, 18, 46, 9, 923, DateTimeKind.Local).AddTicks(4031), "somosfundacionmas@gmail.com", "Somos_Más", "Somos Más", "Somos_Más", false, "Somos Más", "Somos Más", 1160112988, new DateTime(2022, 6, 24, 18, 46, 9, 923, DateTimeKind.Local).AddTicks(4049), "Somos Más" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 24, 18, 46, 9, 921, DateTimeKind.Local).AddTicks(6073), "Administrator", false, "Admin", new DateTime(2022, 6, 24, 18, 46, 9, 921, DateTimeKind.Local).AddTicks(6091) },
                    { 2, new DateTime(2022, 6, 24, 18, 46, 9, 921, DateTimeKind.Local).AddTicks(7563), "Owner", false, "Owner", new DateTime(2022, 6, 24, 18, 46, 9, 921, DateTimeKind.Local).AddTicks(7572) }
                });

            migrationBuilder.InsertData(
                table: "Testimonials",
                columns: new[] { "id", "CreatedAt", "Description", "Image", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6515), "Ong Project Alkemy is worth much more than I paid.", "https://www.fakepersongenerator.com/Face/male/male20151086250510345.jpg", false, "David R Andrews", new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6517) },
                    { 1, new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(4304), "Just amazing. Thank You! The very best. Ong Project Alkemy is the real deal!", "https://www.fakepersongenerator.com/Face/female/female20111023425786712.jpg", false, "Florence F Brooks", new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(4334) },
                    { 2, new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6493), "Thank You! Just what I was looking for. All good. Ong Project Alkemy was worth a fortune to my company.", "https://www.fakepersongenerator.com/Face/female/female1022482473236.jpg", false, "Kathie D Green", new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6503) },
                    { 3, new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6510), "Ong Project Alkemy impressed me on multiple levels. Ong Project Alkemy is the real deal!", "https://www.fakepersongenerator.com/Face/male/male1084237525421.jpg", false, "Claude V Patterson", new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6512) },
                    { 5, new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6521), "We were treated like royalty.", "https://www.fakepersongenerator.com/Face/male/male1085177699859.jpg", false, "Frederick I Giroux", new DateTime(2022, 6, 24, 18, 46, 9, 913, DateTimeKind.Local).AddTicks(6523) }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "id", "CategoryId", "Content", "CreatedAt", "Image", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "No news, good news", new DateTime(2022, 6, 24, 18, 46, 9, 917, DateTimeKind.Local).AddTicks(7037), "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX", false, "No news", new DateTime(2022, 6, 24, 18, 46, 9, 917, DateTimeKind.Local).AddTicks(7054) },
                    { 2, 2, "No news, good news", new DateTime(2022, 6, 24, 18, 46, 9, 917, DateTimeKind.Local).AddTicks(9946), "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX", false, "No news", new DateTime(2022, 6, 24, 18, 46, 9, 917, DateTimeKind.Local).AddTicks(9958) },
                    { 3, 3, "No news, good news", new DateTime(2022, 6, 24, 18, 46, 9, 918, DateTimeKind.Local).AddTicks(45), "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX", false, "No news", new DateTime(2022, 6, 24, 18, 46, 9, 918, DateTimeKind.Local).AddTicks(49) },
                    { 4, 4, "No news, good news", new DateTime(2022, 6, 24, 18, 46, 9, 918, DateTimeKind.Local).AddTicks(54), "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX", false, "No news", new DateTime(2022, 6, 24, 18, 46, 9, 918, DateTimeKind.Local).AddTicks(56) },
                    { 5, 4, "No news, good news", new DateTime(2022, 6, 24, 18, 46, 9, 918, DateTimeKind.Local).AddTicks(59), "https://www.vidimsoft.com/sites/default/files/styles/blog_lg/public/articles/nonewsyet.jpg?itok=51EKSfgX", false, "No news", new DateTime(2022, 6, 24, 18, 46, 9, 918, DateTimeKind.Local).AddTicks(62) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NewId",
                table: "Comments",
                column: "NewId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Slides_OrganizationId",
                table: "Slides",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
