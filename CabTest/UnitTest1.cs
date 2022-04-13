using System;
using NUnit.Framework;
using Cab_Invoice_Generator;
using System.Collections.Generic;

namespace CabTest
{
    public class Tests
    {
        InvoiceGenerator newInvoice;
        [SetUp]
        public void Setup()
        {
            newInvoice = new InvoiceGenerator();
        }
        /// <summary>
        /// UC1 When given proper distance and time should return calculated fare
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <param name="expected"></param>
        [TestCase(5, 3, 53)]
        [TestCase(5.4, 3.3, 57.3)]
        [TestCase(0.4, 0.3, 5)]
        [TestCase(8.451, 3.47, 87.98)]
        public void GivenProper_TimeAndDistance_CalculateFare(double distance, double time, double expected)
        {
            Ride ride = new(distance, time);
            Assert.AreEqual(expected, newInvoice.TotalFare_for_SingleRide(ride));
        }
        /// <summary>
        /// TC 1.1 When given improper distance(negative) should throw respective exception
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        [TestCase(-5, 3)]
        [TestCase(-5.4, 3.3)]
        [TestCase(-0.4, 0.3)]
        [TestCase(-8.451, 3.47)]
        public void GivenInvalid_Distance_ThrowException(double distance, double time)
        {
            Ride ride = new(distance, time);
            CustomCabExceptions exception = Assert.Throws<CustomCabExceptions>(() => 
            newInvoice.TotalFare_for_SingleRide(ride));
            Assert.AreEqual(exception.type, CustomCabExceptions.ExceptionType.INVALID_DISTANCE);
        }
        /// <summary>
        /// TC 1.2 When given improper time(negative) should throw respective exception
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        [TestCase(5, -3)]
        [TestCase(5.4, -3.3)]
        [TestCase(0.4, -0.3)]
        [TestCase(8.451, -3.47)]
        public void GivenInvalid_Time_ThrowException(double distance, double time)
        {
            Ride ride = new(distance, time);
            CustomCabExceptions exception = Assert.Throws<CustomCabExceptions>(() =>
            newInvoice.TotalFare_for_SingleRide(ride));
            Assert.AreEqual(exception.type, CustomCabExceptions.ExceptionType.INVALID_TIME);
        }
        /// <summary>
        /// UC2 When given list of rides should return total fare for multiple rides
        /// UC3 For multiple rides check for average fare and number of rides
        /// </summary>
        [Test]
        public void GivenA_ListOfRides_Calculates_TotalFare_NumberOfRides_and_AverageFare()
        {
            Ride rideOne = new Ride(2, 1);
            Ride rideTwo = new Ride(2, 2);
            List<Ride> rides = new List<Ride>();

            rides.Add(rideOne);
            rides.Add(rideTwo);

            Assert.AreEqual(43.0d, newInvoice.TotalFare_for_MultipileRides(rides));
            Assert.AreEqual(21.5d, newInvoice.averagePerRide);
            Assert.AreEqual(2, newInvoice.rideCount);
        }
    }
}