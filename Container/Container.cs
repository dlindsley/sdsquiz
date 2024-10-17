using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly IDictionary<Type, Type> _typeDictionary;

        public Container()
        {
            _typeDictionary = new Dictionary<Type, Type>();
        }

        public void Bind(Type interfaceType, Type implementationType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException(nameof(interfaceType));
            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));

            if (!interfaceType.IsInterface)
                throw new ArgumentException("Argument should be an interface", nameof(interfaceType));
            if (!implementationType.IsClass || implementationType.IsAbstract)
                throw new ArgumentException("Argument should be a class", nameof(implementationType));

            if (!implementationType.GetInterfaces().Contains(interfaceType))
                throw new ArgumentException($"{nameof(implementationType)} must implement {nameof(interfaceType)}");

            //note, if there is already an implementation registered for this interface, last in wins
            _typeDictionary[interfaceType] = implementationType;
        }

        public T Get<T>() where T : class
        {
            if (!_typeDictionary.ContainsKey(typeof(T)))
                throw new ArgumentException($"no implementation registered for type {typeof(T)}");  //or just return null

            Type concreteType = _typeDictionary[typeof(T)];
            return Activator.CreateInstance(concreteType) as T;
        }
    }
}