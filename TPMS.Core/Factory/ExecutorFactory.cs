using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TPMS.Core.Interfaces;

namespace TPMS.Core.Factory
{
    /// <summary>
    /// Factory for creating executor instances based on client configuration
    /// </summary>
    public static class ExecutorFactory
    {
        private static readonly Dictionary<string, Type> _responsibilityExecutors = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Type> _leadTimeExecutors = new Dictionary<string, Type>();
        private static string _defaultClient = null;

        /// <summary>
        /// Registers a responsibility executor implementation for a specific client
        /// </summary>
        /// <param name="clientName">The client name</param>
        /// <param name="executorType">The executor type to register</param>
        public static void RegisterResponsibilityExecutor(string clientName, Type executorType)
        {
            if (!typeof(IPrimaryResponsibilityExecutor).IsAssignableFrom(executorType))
                throw new ArgumentException($"Type {executorType.Name} does not implement IResponsibilityExecutor");

            _responsibilityExecutors[clientName] = executorType;

            if (_defaultClient == null)
                _defaultClient = clientName;
        }

        /// <summary>
        /// Registers a lead time executor implementation for a specific client
        /// </summary>
        /// <param name="clientName">The client name</param>
        /// <param name="executorType">The executor type to register</param>
        //public static void RegisterLeadTimeExecutor(string clientName, Type executorType)
        //{
        //    if (!typeof(ILeadTimeExecutor).IsAssignableFrom(executorType))
        //        throw new ArgumentException($"Type {executorType.Name} does not implement ILeadTimeExecutor");

        //    _leadTimeExecutors[clientName] = executorType;

        //    if (_defaultClient == null)
        //        _defaultClient = clientName;
        //}

        /// <summary>
        /// Sets the default client to use when no specific client is specified
        /// </summary>
        /// <param name="clientName">The client name to set as default</param>
        public static void SetDefaultClient(string clientName)
        {
            if (!_responsibilityExecutors.ContainsKey(clientName) && !_leadTimeExecutors.ContainsKey(clientName))
                throw new ArgumentException($"Client {clientName} has no registered executors");

            _defaultClient = clientName;
        }

        /// <summary>
        /// Creates a responsibility executor instance for the specified client
        /// </summary>
        /// <param name="clientName">The client name (optional, uses default if not specified)</param>
        /// <returns>An instance of IResponsibilityExecutor</returns>
        public static IPrimaryResponsibilityExecutor CreateResponsibilityExecutor(string clientName = null)
        {
            string client = clientName ?? _defaultClient;

            if (client == null)
                throw new InvalidOperationException("No default client has been set");

            if (!_responsibilityExecutors.TryGetValue(client, out Type executorType))
                throw new InvalidOperationException($"No responsibility executor registered for client {client}");

            return (IPrimaryResponsibilityExecutor)Activator.CreateInstance(executorType);
        }

        ///// <summary>
        ///// Creates a lead time executor instance for the specified client
        ///// </summary>
        ///// <param name="clientName">The client name (optional, uses default if not specified)</param>
        ///// <returns>An instance of ILeadTimeExecutor</returns>
        //public static ILeadTimeExecutor CreateLeadTimeExecutor(string clientName = null)
        //{
        //    string client = clientName ?? _defaultClient;

        //    if (client == null)
        //        throw new InvalidOperationException("No default client has been set");

        //    if (!_leadTimeExecutors.TryGetValue(client, out Type executorType))
        //        throw new InvalidOperationException($"No lead time executor registered for client {client}");

        //    return (ILeadTimeExecutor)Activator.CreateInstance(executorType);
        //}

        /// <summary>
        /// Auto-discovers and registers all executor implementations in the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to scan for executor implementations</param>
        /// <param name="clientName">The client name to register the found executors under</param>
        public static void DiscoverAndRegisterExecutors(Assembly assembly, string clientName)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPrimaryResponsibilityExecutor).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    RegisterResponsibilityExecutor(clientName, type);
                }

                //if (typeof(ILeadTimeExecutor).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                //{
                //    RegisterLeadTimeExecutor(clientName, type);
                //}
            }
        }
    }
}
