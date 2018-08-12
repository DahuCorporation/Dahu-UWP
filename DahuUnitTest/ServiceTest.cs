using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services.ModelManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUnitTest
{
    [TestClass]
    public class ServiceTest
    {
        public User userTest;

        /*
         *
         * User tests
         * 
         */

        /// <summary>
        /// CreateAccountTest
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Method1()
        {
            UserManager userManager = new UserManager();
            // Set account in static
            AppStaticInfo.Account = new Account
            {
                Password = "123456",
                Type = "meme"
            };
            User user = new User
            {
                Gender = DahuUWP.Utils.Enum.Gender.Sir,
                Address = "8 route du circuit",
                Biography = "Bonjour je suis le test n°1",
                Birthdate = new DateTime(2018, 8, 12),
                City = "Lurcy-Levis",
                Country = "France",
                FirstName = "Teste",
                LastName = "Bili",
                Mail = "testBili36@dahu.fr",
                Phone = "060000000",
                PostalCode = "33200",
                Account = AppStaticInfo.Account
            };
            AppStaticInfo.Account.Mail = user.Mail;
            Assert.AreEqual(true, await userManager.Create(user));
        }


        /// <summary>
        /// Connection
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Method2()
        {
            AccountDataService accounDataService = new AccountDataService();
            Assert.AreEqual(true, await accounDataService.Connect());
        }

        /**
        *
        * Project tests 
        * 
        **/
        /// <summary>
        /// Create project
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Method3()
        {
            ProjectManager projectManager = new ProjectManager();
            Project project = new Project
            {
                Name = "BiliProject",
                Description = "This project is a unit test project"
            };
            
            Assert.AreEqual(true, await projectManager.Create(project));
        }


        /**
         *
         * ScrumBoard tests 
         * 
         **/

        //[TestMethod]
        //public async Task CreateAScrumBoardTest()
        //{
        //    ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
        //    Account account = new Account
        //    {
        //        Password = "123456",
        //        Type = "meme"
        //    };
        //    User user = new User
        //    {
        //        Gender = DahuUWP.Utils.Enum.Gender.Sir,
        //        Address = "8 route du circuit",
        //        Biography = "Bonjour je suis le test n°1",
        //        Birthdate = new DateTime(2018, 8, 12),
        //        City = "Lurcy-Levis",
        //        Country = "France",
        //        FirstName = "Teste",
        //        LastName = "Bili",
        //        Mail = "testBili26@dahu.fr",
        //        Phone = "060000000",
        //        PostalCode = "33200",
        //        Account = account
        //    };
        //    Assert.AreEqual(true, await userManager.Create(user));
        //}
    }
}
