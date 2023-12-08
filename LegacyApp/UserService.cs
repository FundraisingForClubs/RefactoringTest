using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class UserService
    {

        public bool AddUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var user = CreateUser(firstname, surname, email, dateOfBirth);

            if (user == null) 
            {
                return false;
            }

            user.Client = GetClient(clientId);

            UpdateCreditLimit(ref user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }

        public User CreateUser(string firstname, string surname, string email, DateTime dateOfBirth)
        {
            var user = new User()
            {
                Firstname = firstname,
                Surname = surname,
                EmailAddress = email,
                DateOfBirth = dateOfBirth
            };

            //Data validated through Data Anotations in the class - Name, Email and Age

            ICollection<ValidationResult> errorResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, new ValidationContext(user), errorResults, true))
            {
                return null;
            }

            return user;
        }

        public bool HasCreditLimit(string clientName)
        {
            if (clientName.ToLower() == "veryimportantclient")
            {
                return false;
            }
            return true; 
        }

        public int CreditLimitMultiplier(string clientName)
        {
            if (clientName.ToLower() == "importantclient")
            {
                return 2;
            }
            return 1;
        }

        public void UpdateCreditLimit(ref User user)
        {
            user.HasCreditLimit = HasCreditLimit(user.Client.Name);

            if (user.HasCreditLimit) 
            {
                using (var userCreditService = new UserCreditServiceClient())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit * CreditLimitMultiplier(user.Client.Name);
                }
            }
        }

        public Client GetClient(int ClientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(ClientId);
        }
    }
}
