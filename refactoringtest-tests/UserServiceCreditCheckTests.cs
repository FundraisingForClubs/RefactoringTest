using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserServiceCreditCheckTests
    {
        private readonly UserService _userService;

        public UserServiceCreditCheckTests()
        {
            _userService = new UserService();
        }

        [Theory]
        [InlineData("VeryImportantClient")]
        [InlineData("veryImportantclient")]
        [InlineData("veryimportantclient")]
        public void HasCreditLimit_Valid_ClientName_Returns_False(string clientName)
        {
            // Arrange

            // Act
            bool result = _userService.HasCreditLimit(clientName);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Very Important Client")]
        [InlineData("Importantclient")]
        [InlineData("importantclient")]
        [InlineData("AnoyOtherClient")]
        public void HasCreditLimit_Valid_ClientName_Returns_True(string clientName)
        {
            // Arrange

            // Act
            bool result = _userService.HasCreditLimit(clientName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetCreditLimitMultiplier_ImportantClient_Result2()
        {
            // Arrange
            string clientName = "ImportantClient";

            // Act
            int result = _userService.CreditLimitMultiplier(clientName);

            // Assert
            Assert.Equal(2, result);
        }

        public void GetCreditLimitMultiplier_Not_ImportantClient_Result1()
        {
            // Arrange
            string clientName = "AnyOtherClient";

            // Act
            int result = _userService.CreditLimitMultiplier(clientName);

            // Assert
            Assert.Equal(1, result);

        }



    }
}
