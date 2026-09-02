using BCrypt.Net;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
namespace CleaningServiceBookingSystemMain.Application
{


	public class Encryption
	{
		public string HashPassword(string password)// hashes inputted password with salt
		{
			
			return BCrypt.Net.BCrypt.HashPassword(password, 12);// bcrypt stores salt with hashed password 

		}
		public bool VerifyPassword(string hashPassword, string enteredPassword)//checks if password is the same as the password in database
		{
			var isValid = BCrypt.Net.BCrypt.Verify(enteredPassword, hashPassword);
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
