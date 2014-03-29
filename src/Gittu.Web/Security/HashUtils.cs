using System.Security.Cryptography;

namespace Gittu.Web.Security
{
	public static class HashUtils
	{
		private readonly static RNGCryptoServiceProvider SaltGenerator = new RNGCryptoServiceProvider();

		//Code in this class was shamelessly copied from http://stackoverflow.com/questions/6415724/best-way-to-generate-random-salt-in-c
		public static byte[] GenerateSalt(int saltLength = 20)
		{
			var randomSalt = new byte[saltLength];
			SaltGenerator.GetNonZeroBytes(randomSalt);
			return randomSalt;
		}
	}
}