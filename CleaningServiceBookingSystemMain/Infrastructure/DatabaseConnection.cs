using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class DatabaseConnection
    {
        public string ConnectionString { get; private set; }
        public DatabaseConnection()
        {
            ConnectionString = "Server=localhost\\SQLEXPRESS;Database=CleaningServiceBooking;Trusted_Connection=True;TrustServerCertificate=True;";
        }

    }
}
