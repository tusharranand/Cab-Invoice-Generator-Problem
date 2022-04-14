using System;
using NUnit.Framework;
using Cab_Invoice_Generator;
using System.Collections.Generic;

namespace CabTest
{
    public class Tests
    {
        InvoiceGenerator newInvoice;
        rideRepository rideRecords;
        [SetUp]
        public void Setup()
        {
            newInvoice = new InvoiceGenerator();
            rideRecords = new rideRepository();
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
        /// <summary>
        /// TC 4.1 Given valid UserId generate invoice
        /// </summary>
        [Test]
        public void Given_ValidUserID_GenerateInvoice()
        {
            Ride rideOne = new Ride(2, 1);
            Ride rideTwo = new Ride(2, 2);

            rideRecords.AddUserRideRecord("Xyz", rideOne);
            rideRecords.AddUserRideRecord("Xyz", rideTwo);

            Assert.AreEqual(43.0d, newInvoice.TotalFare_for_MultipileRides(
                rideRecords.ReturnUserRecord("Xyz")));
            Assert.AreEqual(21.5d, newInvoice.averagePerRide);
            Assert.AreEqual(2, newInvoice.rideCount);
        }
        /// <summary>
        /// Given invalid userID throw exception
        /// </summary>
        [Test]
        public void Given_InvalidUserID_ThrowException()
        {
            Ride rideOne = new Ride(2, 1);
            Ride rideTwo = new Ride(2, 2);

            rideRecords.AddUserRideRecord("Xyz", rideOne);
            rideRecords.AddUserRideRecord("Xyz", rideTwo);

            CustomCabExceptions exception = Assert.Throws<CustomCabExceptions>(() =>
            newInvoice.TotalFare_for_MultipileRides(rideRecords.ReturnUserRecord("Pqr")));
            Assert.AreEqual(exception.type, CustomCabExceptions.ExceptionType.INVALID_USER_ID);
        }
        /// <summary>
        /// UC5 When given proper distance and time should return calculated fare for premium ride
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <param name="expected"></param>
        [TestCase(5, 3, 81)]
        [TestCase(5.4, 3.3, 87.6d)]
        [TestCase(0.4, 0.3, 20)]
        [TestCase(8.451, 3.47, 133.705)]
        public void GivenProper_TimeAndDistance_CalculatePremiumFare(double distance, double time, double expected)
        {
            Ride ride = new(distance, time);
            Assert.AreEqual(expected, newInvoice.TotalFare_for_SinglePremiumRide(ride));
        }
    }
}