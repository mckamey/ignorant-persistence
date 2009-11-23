using System;

namespace IgnorantPersistence
{
	/// <summary>
	/// Unit-of-Work interface
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Gets access to the persistence collection for <typeparamref name="T" />
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		ITable<T> GetTable<T>()
			where T : class;

		/// <summary>
		/// Persists any changes to the underlying storage
		/// </summary>
		void Save();
	}
}
