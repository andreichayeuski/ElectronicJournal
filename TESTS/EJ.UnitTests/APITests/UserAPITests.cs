using System.Collections.Generic;
using EJ.UnitTests.Services;
using EJ.Web.Areas.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using EJ.Domain.Services;
using EJ.Entities.Models;
using System;
using EJ.Models.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EJ.UnitTests.APITests
{
    [TestClass]
    public class UserAPITests : BaseUnitTest
    {
        [TestMethod]
        public void UserREST()
        {
            /*var mock = new Mock<IGroupService>();
            var user = new User()
            {
                Number = 20,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                HalfGroup = false,
                CourseId = 1
            };*/
            /* mock.Setup(a => a.AddGroup(group)).Returns<GroupViewModel>(group => group);
             mock.Setup(a => a.GetGroup()).Returns<GroupViewModel>(group => group);
             mock.Setup(a => a.AddGroup(group)).Returns<GroupViewModel>(group => group);*/
            var controller = new UserController(UserService);

            var result = controller.Get() as JsonResult;
            Assert.IsNotNull(result);

            /*result = controller.Post(group) as JsonResult;
            Assert.IsNotNull(result);
            */
            /*var groupForUpdate = new GroupViewModel  {
                Number = 20,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                HalfGroup = true,
                CourseId = 2
            };
            result = controller.Put(((GroupViewModel) result.Value).Id, groupForUpdate) as JsonResult;
            Assert.IsNotNull(result);

            result = controller.Get(((GroupViewModel) result.Value).Id) as JsonResult;
            Assert.IsNotNull(result);

            result = controller.Delete(((GroupViewModel) result.Value).Id) as JsonResult;
            Assert.IsNotNull(result);*/
        }
    }
}
