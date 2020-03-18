using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class CampgroundDAOTest : POTestInitialize
    {
        [TestMethod]
        public void GetCampgroundTest_ShouldReturnAll()
        {
            CampgroundSqlDAO dao = new CampgroundSqlDAO(ConnectionString);
            IList<Campground> campgrounds = dao.GetCampground(park_ID);
            Assert.IsTrue(campgrounds.Count > 0);
        }


        [TestMethod]
        public void CampgroundNotExistTest()
        {
            CampgroundSqlDAO campgroundSqlDAO = new CampgroundSqlDAO(ConnectionString);
            IList<Campground> camps = campgroundSqlDAO.GetCampground(park_ID);

            foreach (Campground foo in camps)
            {
                if (foo.name.Equals("Nonexistent Project"))
                {
                    Assert.IsTrue(false);
                }
            }
            
        }

        [TestMethod]
        public void CampgroundExistTest()
        {
            CampgroundSqlDAO campgroundSqlDAO = new CampgroundSqlDAO(ConnectionString);
            IList<Campground> camps = campgroundSqlDAO.GetCampground(park_ID);

            foreach (Campground foo in camps)
            {
                if (foo.name.Equals("Seawall"))
                {
                    Assert.IsTrue(true);
                }
            }

        }

    }
}
