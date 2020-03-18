using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class ParkDOATest : POTestInitialize
    {
        [TestMethod]
        public void GetAllParkInfoTest()
        {
            ParkSqlDAO dao = new ParkSqlDAO(ConnectionString);
            IList<Park> parks = dao.GetAllParkInfo();
            Assert.IsTrue(parks.Count > 0);
        }
    }
}
