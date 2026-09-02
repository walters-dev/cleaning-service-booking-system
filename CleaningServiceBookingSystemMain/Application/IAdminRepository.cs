using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IAdminRepository
    {
        IList<Admins> GetAdmins();      //gets list of all Admins from storage
        Admins GetAdminsById(int? Id);  //gets a specific Admin from storage
        void Add(Admins admins);        //Adds an admin to storage
        void Update(Admins admins);     //edits an admin 
        void Delete(Admins admins);     //
    }
}
