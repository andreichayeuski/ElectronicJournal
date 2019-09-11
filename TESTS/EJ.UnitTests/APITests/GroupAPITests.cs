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
    public class GroupAPITests : BaseUnitTest
    {
        [TestMethod]
        public void GroupREST()
        {
            var mock = new Mock<IGroupService>();
            var group = new GroupUi()
            {
                Number = 20,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                HalfGroup = false,
                CourseId = 1
            };
            /* mock.Setup(a => a.AddGroup(group)).Returns<GroupUi>(group => group);
             mock.Setup(a => a.GetGroup()).Returns<GroupUi>(group => group);
             mock.Setup(a => a.AddGroup(group)).Returns<GroupUi>(group => group);*/
            var controller = new GroupController(GroupService);

            var result = controller.Get(1) as JsonResult;
            Assert.IsNotNull(result);

            /*result = controller.Post(group) as JsonResult;
            Assert.IsNotNull(result);
            */
            /*var groupForUpdate = new GroupUi  {
                Number = 20,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                HalfGroup = true,
                CourseId = 2
            };
            result = controller.Put(((GroupUi) result.Value).Id, groupForUpdate) as JsonResult;
            Assert.IsNotNull(result);

            result = controller.Get(((GroupUi) result.Value).Id) as JsonResult;
            Assert.IsNotNull(result);

            result = controller.Delete(((GroupUi) result.Value).Id) as JsonResult;
            Assert.IsNotNull(result);*/
        }
    }
}
