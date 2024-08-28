using System.Collections.Generic;
using System;
using System.Linq;

namespace FutbolSolution.DependencyInjection
{
        public class DIContainer
        {
            private readonly List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();
            private readonly Dictionary<Type, object> _singletonInstances = new Dictionary<Type, object>();

            public void RegisterSingleton<TService, TImplementation>()
                where TImplementation : TService
            {
                _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton));
            }

            public void RegisterSingleton<TService>(TService implementationInstance)
            {
                _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), implementationInstance));
            }

            public void RegisterTransient<TService, TImplementation>()
                where TImplementation : TService
            {
                _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient));
            }

            public TService Resolve<TService>()
            {
                return (TService)Resolve(typeof(TService));
            }

            private object Resolve(Type serviceType)
            {
                var descriptor = _serviceDescriptors.SingleOrDefault(x => x.ServiceType == serviceType);
                if (descriptor == null)
                {
                    throw new Exception($"Service of type {serviceType.Name} is not registered");
                }

                if (descriptor.ImplementationInstance != null)
                {
                    return descriptor.ImplementationInstance;
                }

                if (descriptor.Lifetime == ServiceLifetime.Singleton)
                {
                    if (_singletonInstances.ContainsKey(serviceType))
                    {
                        return _singletonInstances[serviceType];
                    }

                    var implementation = CreateInstance(descriptor.ImplementationType);
                    _singletonInstances[serviceType] = implementation;
                    return implementation;
                }

                return CreateInstance(descriptor.ImplementationType);
            }

            private object CreateInstance(Type implementationType)
            {
                var constructorInfo = implementationType.GetConstructors().First();
                var parameters = constructorInfo.GetParameters();
                if (!parameters.Any())
                {
                    return Activator.CreateInstance(implementationType);
                }

                var parameterImplementations = new List<object>();
                foreach (var parameter in parameters)
                {
                    var parameterImplementation = Resolve(parameter.ParameterType);
                    parameterImplementations.Add(parameterImplementation);
                }

                return constructorInfo.Invoke(parameterImplementations.ToArray());
            }
        }

}
