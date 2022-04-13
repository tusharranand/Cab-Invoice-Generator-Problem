using System;
using NUnit.Framework;
using Cab_Invoice_Generator;

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
        /// 
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
        /// 
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
        /// 
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
    }
}