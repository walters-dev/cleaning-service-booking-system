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
            ConnectionString = "Server=localhost\\SQLEXPRESS01;Database=CleaningServiceBooking;Trusted_Connection=True;TrustServerCertificate=True;";//sets the connection string to local database
        }

    }
}
