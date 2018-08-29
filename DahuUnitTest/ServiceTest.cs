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
        protected List<Project> userProjects;
        Random rnd = new Random();
        protected ScrumBoard scrumBoardTest;

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
                Mail = "testBili43" + rnd.Next(1, 99999) + "@dahu.fr",
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
                Name = "BiliProject" + rnd.Next(1, 999999),
                Description = "This project is a unit test project"
            };
            
            Assert.AreEqual(true, await projectManager.Create(project));
        }

        /// <summary>
        /// Get projects of user and test scrum boards
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Method4()
        {
            UserManager userManager = new UserManager();
            Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            userProjects = await userManager.ChargeProjects(AppStaticInfo.Account.Uuid, null);
            Assert.AreNotEqual(null, userProjects);
            await ScrumBoardCreationTest();
        }


        /*
         *
         * ScrumBoard tests 
         * 
         */
        /// <summary>
        /// Create a scrum board test
        /// </summary>
        /// <returns></returns>
        public async Task ScrumBoardCreationTest()
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();

            ScrumBoard scrumBoard = new ScrumBoard
            {
                Name = "ScrumBoard test1" + rnd.Next(1, 999999)
            };
            scrumBoardTest = await scrumBoardManager.CreateScrumBoard(scrumBoard, userProjects[0].Uuid);
            Assert.AreNotEqual(null, scrumBoardTest);
            await ChargeOneScrumBoardTest();
        }

        /// <summary>
        /// Create a scrum board test
        /// </summary>
        /// <returns></returns>
        public async Task ChargeOneScrumBoardTest()
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();

            ScrumBoard oneScrumBoardTest = await scrumBoardManager.ChargeOneScrumBoard(scrumBoardTest.Uuid);
            Assert.AreEqual(scrumBoardTest.Uuid, oneScrumBoardTest.Uuid);
            await ModifyScrumBoard();
        }

        public async Task ModifyScrumBoard()
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
            ScrumBoard scrumBoard = new ScrumBoard
            {
                Name = "ScrumBoard test2" +  rnd.Next(1, 999999)
            };
            ScrumBoard oneScrumBoardTest = await scrumBoardManager.EditScrumBoard(scrumBoard, scrumBoardTest.Uuid);
            Assert.AreEqual(scrumBoardTest.Uuid, oneScrumBoardTest.Uuid);
            await DeleteScrumBoard();
        }

        public async Task DeleteScrumBoard()
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
            ScrumBoard scrumBoard = new ScrumBoard
            {
                Name = "ScrumBoard test2" + rnd.Next(1, 999999)
            };
            bool result = await scrumBoardManager.DeleteScrumBoard(userProjects[0].Uuid);
            //Assert.AreEqual(scrumBoardTest.Uuid, oneScrumBoardTest.Uuid);
        }
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
