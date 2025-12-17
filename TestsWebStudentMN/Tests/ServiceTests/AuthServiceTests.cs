using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using StudentMN.Data;
using StudentMN.DTOs.Request;
using StudentMN.Models.Entities.Account;
using StudentMN.Models.Entities.PermissionModels;
using StudentMN.Repositories.Interface;
using StudentMN.Services;
using Xunit;

namespace StudentMN.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AppDbContext _db;
        private readonly AuthService _service;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new AppDbContext(options);

            _userRepoMock = new Mock<IUserRepository>();
            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(x => x["Jwt:Key"])
                .Returns("VerySecureJwtKey_12345678901234567890");
            _configMock.Setup(x => x["Jwt:Issuer"]).Returns("TestIssuer");
            _configMock.Setup(x => x["Jwt:Audience"]).Returns("TestAudience");

            _service = new AuthService(
                _userRepoMock.Object,
                _configMock.Object,
                _db
            );
        }


        [Fact]
        public void HashPassword_ShouldReturnHashedValue()
        {
            var hash = _service.HashPassword("123456");

            Assert.NotNull(hash);
            Assert.NotEqual("123456", hash);
        }

        [Fact]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            var password = "123456";
            var hash = _service.HashPassword(password);

            Assert.True(_service.VerifyPassword(password, hash));
        }

        [Fact]
        public void VerifyPassword_WrongPassword_ReturnsFalse()
        {
            var hash = _service.HashPassword("123456");

            Assert.False(_service.VerifyPassword("wrong", hash));
        }


        [Fact]
        public async Task Login_EmptyUsernameOrPassword_ReturnsFail()
        {
            var res = await _service.LoginAsync(new LoginRequest());

            Assert.False(res.Success);
            Assert.Contains("không được để trống", res.Message);
        }

        [Fact]
        public async Task Login_UserNotFound_ReturnsFail()
        {
            _userRepoMock.Setup(x => x.GetUserByUsernameAsync("abc"))
                .ReturnsAsync((User)null);

            var res = await _service.LoginAsync(new LoginRequest
            {
                Username = "abc",
                Password = "123"
            });

            Assert.False(res.Success);
            Assert.Contains("không tồn tại", res.Message);
        }

        [Fact]
        public async Task Login_WrongPassword_ReturnsFail()
        {
            var user = FakeUser();

            _userRepoMock.Setup(x => x.GetUserByUsernameAsync(user.Username))
                .ReturnsAsync(user);
            _userRepoMock.Setup(x => x.ValidateCredentialsAsync(user.Username, "wrong"))
                .ReturnsAsync(false);

            var res = await _service.LoginAsync(new LoginRequest
            {
                Username = user.Username,
                Password = "wrong"
            });

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Login_Valid_ReturnsToken()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            _db.Roles.Add(role);
            await _db.SaveChangesAsync();

            var user = FakeUser(role);

            _userRepoMock.Setup(x => x.GetUserByUsernameAsync(user.Username))
                .ReturnsAsync(user);
            _userRepoMock.Setup(x => x.ValidateCredentialsAsync(user.Username, "123"))
                .ReturnsAsync(true);
            _userRepoMock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var res = await _service.LoginAsync(new LoginRequest
            {
                Username = user.Username,
                Password = "123"
            });

            Assert.True(res.Success);
            Assert.NotNull(res.AccessToken);
            Assert.NotNull(res.RefreshToken);
            Assert.Equal("Admin", res.User.Role);
        }


        [Fact]
        public void GenerateJwtToken_ValidUser_ReturnsToken()
        {
            var token = _service.GenerateJwtToken(FakeUser(), "Admin");

            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void GenerateJwtToken_EmptyUsername_Throws()
        {
            var user = FakeUser();
            user.Username = "";

            Assert.Throws<Exception>(() =>
                _service.GenerateJwtToken(user, "Admin"));
        }


        [Fact]
        public async Task ValidateToken_Invalid_ReturnsFalse()
        {
            Assert.False(await _service.ValidateTokenAsync("invalid"));
        }


        [Fact]
        public async Task RefreshToken_Expired_ReturnsFail()
        {
            var user = FakeUser();
            user.RefreshToken = "token";
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(-1);

            _userRepoMock.Setup(x => x.GetUserByRefreshTokenAsync("token"))
                .ReturnsAsync(user);
              
            var res = await _service.RefreshTokenAsync("token");

            Assert.False(res.Success);
        }


        private User FakeUser(Role? role = null)
        {
            return new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@test.com",
                FullName = "Admin",
                Password = BCrypt.Net.BCrypt.HashPassword("123"),
                RoleId = role?.Id ?? 0,
                Role = role
            };
        }
    }
}
