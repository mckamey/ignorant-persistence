using System;

namespace IgnorantPersistence
{
	/// <summary>
	/// An interface entities can implement to support
	/// </summary>
	public interface ISoftDeleteEntity
	{
		DateTime? DeletedDate { get; set; }

		string Signature { get; }

		void CopyValuesFrom(object item);
	}
}
