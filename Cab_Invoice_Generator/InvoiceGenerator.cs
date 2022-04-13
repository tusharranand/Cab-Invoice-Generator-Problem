using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab_Invoice_Generator
{
    public class InvoiceGenerator
    {
        readonly int pricePerKM;
        readonly int pricePerMinute;
        readonly int minimumFare;
        public double totalFare;
        public int rideCount;
        public double averagePerRide;
        public InvoiceGenerator()
        {
            this.pricePerKM = 10;
            this.pricePerMinute = 1;
            this.minimumFare = 5;
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
    }
}
