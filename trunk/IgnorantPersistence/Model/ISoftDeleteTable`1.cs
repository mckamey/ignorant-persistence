using System;
using System.Collections.Generic;
using System.Linq;

namespace IgnorantPersistence
{
	/// <summary>
	/// A table which doesn't actually delete but instead flags as deleted
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ISoftDeleteTable<T> :
		ITable<T>
		where T : class
	{
		/// <summary>
		/// Gets a queryable collection of all items, deleted or not
		/// </summary>
		IQueryable<T> AllItems { get; }

		/// <summary>
		/// Gets a queryable collection of all the deleted items
		/// </summary>
		IQueryable<T> Deleted { get; }
	}
}
