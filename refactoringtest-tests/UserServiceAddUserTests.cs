using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserServiceAddUserTests
    {
        private readonly UserService _userService;

        public UserServiceAddUserTests()
        {
            _userService = new UserService();
        }

        [Fact]
        public void CreateUser_All_Valid_Fields()
        {
            // Arrange
            string firstName = "New";
            string surname = "User";
            string email = "newuser@ukcows.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-33);

            // Act
            var User = _userService.CreateUser(firstName, surname, email, dateOfBirth);
            
            // Assert
            Assert.NotNull(User);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void CreateUser_Invalid_Firstname(string firstName) 
        {
            string surname = "User";
            string email = "newuser@ukcows.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-33);

            // Act
            var User = _userService.CreateUser(firstName, surname, email, dateOfBirth);

            // Assert
            Assert.Null(User);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void CreateUser_Invalid_Surname(string surname)
        {
            string firstName = "New";
            string email = "newuser@ukcows.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-33);

            // Act
            var User = _userService.CreateUser(firstName, surname, email, dateOfBirth);

            // Assert
            Assert.Null(User);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("newuser@ukcows")]
        [InlineData("newuser.ukcows.com")]
        public void CreateUser_Invalid_Email(string email)
        {
            // Arrange
            string firstName = "New";
            string surname = "User";
            DateTime dateOfBirth = DateTime.Now.AddYears(-33);

            // Act
            var User = _userService.CreateUser(firstName, surname, email, dateOfBirth);

            // Assert
            Assert.Null(User);
        }

        [Fact]
        public void CreateUser_Invalid_DateOfBirth()
        {
            // Arrange
            string firstName = "New";
            string surname = "User";
            string email = "newuser@ukcows.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-15);

            // Act
            var User = _userService.CreateUser(firstName, surname, email, dateOfBirth);

            // Assert
            Assert.Null(User);
        }
    }
}
