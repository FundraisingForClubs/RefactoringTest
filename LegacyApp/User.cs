using System;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DateOfBirthAttribute(21,ErrorMessage = "Minimum age is 21")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid pattern.")]
        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Client Client { get; set; }
    }
}
