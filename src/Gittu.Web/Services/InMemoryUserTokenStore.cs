using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Gittu.Web.Services
{
	public class InMemoryUserTokenStore : IUserTokenStore
	{
		private static readonly ConcurrentDictionary<string, Guid> TokenDictionary = new ConcurrentDictionary<string, Guid>();

		private static readonly Object TokenDictionaryLock = new Object();

		public void Save(string userName, Guid token)
		{
			lock (TokenDictionaryLock)
			{
				TokenDictionary[userName.ToLower()] = token;
			}
		}

		public string Get(Guid token)
		{
			lock (TokenDictionaryLock)
			{
				var entry = TokenDictionary.FirstOrDefault(x => x.Value == token);
				return entry.Equals(default(KeyValuePair<string, Guid>)) ? null : entry.Key;
			}
		}

		internal Guid Get(string userName)
		{
			lock (TokenDictionaryLock)
			{
				return TokenDictionary
					.Where(x => x.Key == userName.ToLower())
					.Select(a => a.Value)
					.FirstOrDefault();
			}
		}
	}
}