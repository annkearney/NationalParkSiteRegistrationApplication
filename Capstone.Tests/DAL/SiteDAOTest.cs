using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class SiteDAOTest : POTestInitialize
    {
        [TestMethod]
        public void GetAllSiteInfoTest()
        {
            SiteSqlDAO dao = new SiteSqlDAO(ConnectionString);
            IList<Site> sites = dao.GetAllSiteInfo(campground_id, from_date, to_date);
            Assert.IsTrue(sites.Count > 0);
        }
    }
}
