using BCrypt.Net;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
namespace CleaningServiceBookingSystemMain.Application
{


	public class Encryption
	{
		public string HashPassword(string password)
		{
			//salt = CreateSalt();
			//string base64Salt = Convert.ToBase64String(salt);
			return BCrypt.Net.BCrypt.HashPassword(password, 12);

		}
		public bool VerifyPassword(string hashPassword, string enteredPassword, string salt="")
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
		/*static byte[] CreateSalt()
		{
			byte[] salt = new byte[16];
			RandomNumberGenerator.Fill(salt); //fills salt array with random numbers
			return salt;
		}*/
	}
}
