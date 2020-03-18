using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests.DAL
{

    [TestClass]
    public class ReservationDAOTest : POTestInitialize
    {
        [TestMethod]
        public void AddReservationTest()
        {
            DateTime fromDate = DateTime.Parse("03/20/2020");
            DateTime toDate = DateTime.Parse("03/25/2020");
            DateTime createDate = DateTime.Parse("02/26/2020");

            ReservationSqlDAO psd = new ReservationSqlDAO(ConnectionString);
            IList<Reservation> originalReservationsList = psd.GetAllReservations();

            psd.AddReservation(5, "Henry Edwards", fromDate, toDate, createDate);
            IList<Reservation> newReservationsList = psd.GetAllReservations();
           

            Assert.IsTrue(originalReservationsList.Count < newReservationsList.Count);
        }

    }
}