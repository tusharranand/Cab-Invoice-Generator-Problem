using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab_Invoice_Generator
{
    public class InvoiceGenerator
    {
        private int pricePerKM, pricePerMinute, minimumFare;
        public double totalFare, averagePerRide;
        public int rideCount;

        public InvoiceGenerator()
        {
            this.pricePerKM = 10;       //15 for premium
            this.pricePerMinute = 1;    //2
            this.minimumFare = 5;       //20
        }
        public double TotalFare_for_SingleRide(Ride ride)
        {
            if (ride.distance <= 0)
                throw new CustomCabExceptions(CustomCabExceptions.ExceptionType.INVALID_DISTANCE, "Invaid Distance");
            if (ride.time <= 0)
                throw new CustomCabExceptions(CustomCabExceptions.ExceptionType.INVALID_TIME, "Invaid Time");
            return Math.Max(minimumFare, ride.distance * pricePerKM + ride.time * pricePerMinute);
        }
        public double TotalFare_for_MultipileRides(List<Ride> listOfRides)
        {
            foreach (Ride ride in listOfRides)
            {
                totalFare += TotalFare_for_SingleRide(ride);
                rideCount++;
            }
            averagePerRide = totalFare / (double)rideCount;
            return totalFare;
        }
        public double TotalFare_for_SinglePremiumRide(Ride ride)
        {
            pricePerKM = 15;
            pricePerMinute = 2;
            minimumFare = 20;
            double totalFare = TotalFare_for_SingleRide(ride);
            pricePerKM = 10;
            pricePerMinute = 1;
            minimumFare = 5;
            return totalFare;
        }
    }
}
