using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gittu.Web.Security
{
	//Code in this class was shamelessly copied from the answers posted @ http://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp
	internal class SHA256Hasher : IHasher
	{
		public static byte[] Hash(string value, byte[] salt)
		{
			return Hash(Encoding.UTF8.GetBytes(value), salt);
		}

		public static byte[] Hash(byte[] value, byte[] salt)
		{
			var saltedValue = value.Concat(salt).ToArray();
			return new SHA256Managed().ComputeHash(saltedValue);
		}

		byte[] IHasher.Hash(string value, byte[] salt)
		{
			return Hash(value, salt);
		}
		
		byte[] IHasher.Hash(byte[] value, byte[] salt)
		{
			return Hash(value, salt);
		}
	}
}