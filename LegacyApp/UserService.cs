using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!UserVerify.IsValidName(firstName, lastName))
            {
                return false;
            }

            if (!UserVerify.IsValidEmail(email))
            {
                return false;
            }

            if (!UserVerify.IsOldEnough(dateOfBirth))
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            UserVerify.SetCreditDetails(user, client);

            if (!UserVerify.IsValidCreditLimit(user))
            {
                return false;
            }
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
