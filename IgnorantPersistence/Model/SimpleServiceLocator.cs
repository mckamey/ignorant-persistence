﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;

namespace IgnorantPersistence
{
	/// <summary>
	/// A simple ServiceLocator implementation
	/// </summary>
	public class SimpleServiceLocator : ServiceLocatorImplBase
	{
		#region Fields

		private readonly Dictionary<Type, Delegate> FactoryMethods = new Dictionary<Type, Delegate>();

		#endregion Fields

		#region Init

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="factoryMethods">must be of type Func&lt;string, T&gt;</param>
		public SimpleServiceLocator(params Delegate[] factoryMethods)
		{
			if (factoryMethods == null)
			{
				throw new ArgumentNullException("factories");
			}

			foreach (Delegate method in factoryMethods)
			{
				if (factoryMethods == null)
				{
					throw new ArgumentNullException("factories");
				}

				ParameterInfo[] parameters = method.Method.GetParameters();
				if (parameters.Length != 1 || parameters[0].ParameterType != typeof(string))
				{
					throw new ArgumentException("factoryMethods", "Factory methods must be Func<string, T>");
				}

				this.FactoryMethods[method.Method.ReturnType] = method;
			}
		}

		#endregion Init

		#region Methods

		/// <summary>
		/// Satisfies the ServiceLocatorProvider delegate.
		/// </summary>
		/// <returns></returns>
		public IServiceLocator ServiceLocatorProvider()
		{
			return this;
		}

		#endregion Methods

		#region ServiceLocatorImplBase Members

		/// <summary>
		/// Gets all instances of a given type
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns></returns>
		/// <remarks>
		/// Note this simplified ServiceLocator only supports one factory method per type
		/// </remarks>
		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			yield return this.GetInstance(serviceType);
		}

		/// <summary>
		/// Gets an instance of the specified type
		/// </summary>
		/// <param name="serviceType"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		protected override object DoGetInstance(Type serviceType, string key)
		{
			if (!this.FactoryMethods.ContainsKey(serviceType))
			{
				throw new ActivationException("Must set Func<string, T> factory methods in SimpleServiceLocator constructor.");
			}

			return this.FactoryMethods[serviceType].DynamicInvoke(key);
		}

		#endregion ServiceLocatorImplBase Members
	}
}
