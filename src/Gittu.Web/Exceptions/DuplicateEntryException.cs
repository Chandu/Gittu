using System;
using System.Collections.Generic;
using System.Linq;

namespace Gittu.Web.Exceptions
{
	public class DuplicateEntryException : Exception
	{
		public Type Type { get; protected set; }
		public IReadOnlyList<object> Values { get; protected set; }
		public IReadOnlyList<string> Keys { get; protected set; }
		private IDictionary<string, IEnumerable<string>> _errors;
		public DuplicateEntryException(string key, object value, Type type)
			:this(new[] {key}, new[] {value},type)
		{
			Type = type;
		}

		public DuplicateEntryException(IEnumerable<string> keys, IEnumerable<object> values, Type type):base(ConstructExceptionMessage(keys, values, type))
		{
			Values = new List<object>(values ?? Enumerable.Empty<object>());
			Keys = new List<string>(keys ?? Enumerable.Empty<string>());
		}

		private static string ConstructExceptionMessage(IEnumerable<string> keys, IEnumerable<object> values, Type type)
		{
			return string.Format("An entry for {0} already exists with values {1} for the attributes {2}.", type.Name, values, keys);
		}

		private static IDictionary<string, IEnumerable<string>> ConstructMessages(IEnumerable<string> keys, IReadOnlyList<object> values, Type type)
		{
			return keys
				.Select((a, i) => new
				{
					Property = a,
					Message = string.Format("{1} of {0} has invalid value {2}.", type.Name, a, values[i])
				})
				.GroupBy(a => a.Property)
				.ToDictionary(a => a.Key, a => a.Select( b=> b.Message));
		}

		public virtual IDictionary<string, IEnumerable<string>> Errors
		{
			get { return _errors ?? (_errors = ConstructMessages(Keys,Values, Type)); }
		}
	}
}