using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gittu.Web.Security
{
	//Code in this class was shamelessly copied from the answers posted @ http://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp
	internal static class Hasher
	{
		private readonly static RNGCryptoServiceProvider SaltGenerator = new RNGCryptoServiceProvider();

		public static byte[] Hash(string value, byte[] salt)
		{
			return Hash(Encoding.UTF8.GetBytes(value), salt);
		}

		public static byte[] Hash(byte[] value, byte[] salt)
		{
			byte[] saltedValue = value.Concat(salt).ToArray();
			return new SHA256Managed().ComputeHash(saltedValue);
		}

		//and this from http://stackoverflow.com/questions/6415724/best-way-to-generate-random-salt-in-c
		public static byte[] GenerateSalt(int saltLength=20)
		{
			var randomSalt = new byte[saltLength];
			SaltGenerator.GetNonZeroBytes(randomSalt);
			return randomSalt;
		}
	}
}