namespace Gittu.Web.Security
{
	public interface IHasher
	{
		byte[] Hash(string value, byte[] salt);
		byte[] Hash(byte[] value, byte[] salt);
	}
}