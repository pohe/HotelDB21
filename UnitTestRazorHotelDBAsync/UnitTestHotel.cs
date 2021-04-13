using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorHotelDBasync.Models;
using RazorHotelDBasync.Services;

namespace UnitTestRazorHotelDBAsync
{
    [TestClass]
    public class UnitTestHotel
    {

        // denne klasse tester HotelService

        private string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB2021;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; 
        [TestMethod]
        public void TestAddHotel()
        {
            //Arrange
            HotelService hotelService = new HotelService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            //Act
            int numbersOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(1001, "TestHotel", "Testvej");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;

            int numbersOfHotelsAfter = hotels.Count;
            Hotel h = hotelService.DeleteHotelAsync(newHotel.HotelNr).Result;
            
            //Assert
            Assert.AreEqual(numbersOfHotelsBefore+1, numbersOfHotelsAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNr, newHotel.HotelNr);

        }
    }
}
