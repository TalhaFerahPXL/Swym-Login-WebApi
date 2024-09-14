using ClassLibrary1.Business.Entities;
using ClassLibrary1.Data.Framework;
using ClassLibrary1.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ClassLibrary1.Business
{
    public static class Accounts
    {
        public static SelectResult CheckIfEmailExist(string mail)
        {

            AccountsData accountsData = new AccountsData();

            SelectResult result = accountsData.SelectCheckEmail(mail);

            return result;

        }


        public static InsertResult AddAccounts(string email, string name, string password)
        {
            Account account = new Account(email, name, password);
            AccountsData accountData = new AccountsData();

            account.email = email;
            account.name = name;

            // Hash het wachtwoord voordat je het opslaat
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            account.password = hashedPassword;

            InsertResult result = accountData.insertRegistration(account);
            return result;
        }


        public static SelectResult CheckLoginCredentials(string email, string password)
        {
            AccountsData accountsData = new AccountsData();
            SelectResult result = accountsData.CheckLogin(email, password);
            return result;
        }

        public static SelectResult GetNameFromEmail(string email) {
            AccountsData accountsData = new AccountsData();
            return accountsData.GetName(email);
        }


        public static InsertResult changePassword(string email, string password)
        {
            AccountsData accountsData = new AccountsData();
            InsertResult result = accountsData.updatePassword(email, password);
            return result;

        }
    }
}
