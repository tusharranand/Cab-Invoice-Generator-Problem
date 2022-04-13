using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab_Invoice_Generator
{
    public class rideRepository
    {
        public Dictionary<string, List<Ride>> rideRecords;
        public rideRepository()
        {
            rideRecords = new Dictionary<string, List<Ride>>();
        }
        public void AddUserRideRecord(string UserID, Ride ride)
        {
            if (!rideRecords.ContainsKey(UserID))
                rideRecords.Add(UserID, new List<Ride>());
            rideRecords[UserID].Add(ride);
        }
        public List<Ride> ReturnUserRecord(string UserID) => 
            (rideRecords.ContainsKey(UserID)) ? rideRecords[UserID] : throw new 
            CustomCabExceptions(CustomCabExceptions.ExceptionType.INVALID_USER_ID, "User ID Invalid");
    }
}
