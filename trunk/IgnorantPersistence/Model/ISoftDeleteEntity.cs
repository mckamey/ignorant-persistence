using System;

namespace IgnorantPersistence
{
	/// <summary>
	/// An interface entities can implement to support
	/// </summary>
	public interface ISoftDeleteEntity
	{
		/// <summary>
		/// Gets and sets the DateTime which the entity was deleted. null means not deleted
		/// </summary>
		DateTime? DeletedDate { get; set; }

		/// <summary>
		/// Gets a value which is used to identify the entity after it is deleted
		/// </summary>
		string Signature { get; }

		/// <summary>
		/// Updates an existing entity from the values of another
		/// </summary>
		/// <param name="item"></param>
		void CopyValuesFrom(object item);
	}
}
