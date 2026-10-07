using BCrypt.Net;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
namespace CleaningServiceBookingSystemMain.Application
{


	public class Encryption
	{
		private string hashedpassword;
		private readonly IAdminService _adminService;
		public Encryption(IAdminService adminService)
		{
            _adminService = adminService;

        }
		public string HashPassword(string password)// hashes inputted password with salt
		{
			
			return BCrypt.Net.BCrypt.HashPassword(password, 12);// bcrypt stores salt with hashed password 

		}
		public bool VerifyPassword(Admins enteredAdmin)//checks if password is the same as the password in database
		{
            hashedpassword = _adminService.FindAdminPassword(enteredAdmin.Username).AdminPassword;//finds the admin in storage
            //hashes enteredpassword with the salt in hashpassword then compares 2
            var isValid = BCrypt.Net.BCrypt.Verify(enteredAdmin.AdminPassword, hashedpassword);
			if (isValid)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		
	}
}
