using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.Logging.Application;
using Elder.Core.Logging.Interfaces;
using Elder.Core.MainFrameworks.Application;
using Elder.Core.MainFrameworks.Interfaces;
using Elder.Unity.Logging.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Elder.Unity.MainFrameworks.Infrastructure
{
    public class MainFrameworkInfrastructure : MonoBehaviour, IFactoryProvider, IInfrastructureProvider
    {
        private ILoggerEx _logger = LogApplication.In.CreateLogger<MainFrameworkInfrastructure>();

        private IMainFrameworkApplication _mainFrameworkApplication;

        private Dictionary<Type, IInfrastructure> _infrastructures;

        public IDomainFactory DomainFactory { get; protected set; }
        public IApplicationFactory ApplicationFactory { get; protected set; }
        public IInfrastructureFactory InfrastructureFactory { get; protected set; }

        private void Awake()
        {
            InitializeUnityLogger();
            InitializeFactories();
            InitializeInfrastructure();
            InitializeApplication();
            SetupPersistence();
        }
        private void InitializeUnityLogger()
        {
            UnityLogger.In.SubscribeToLogAppplication();
        }
        private void InitializeFactories()
        {
            CreateDomainFactory();
            CreateApplicationFactory();
            CreateInfrastructFactory();
        }
        protected virtual void CreateDomainFactory()
        {
            DomainFactory = new DomainFactoryBase();
        }
        protected virtual void CreateApplicationFactory()
        {
            ApplicationFactory = new ApplicationFactoryBase();
        }
        protected virtual void CreateInfrastructFactory()
        {
            InfrastructureFactory = new InfrastructureFactoryBase();
        }
        private void InitializeApplication()
        {
            CreateMainFrameworkApplication();
        }
        private void CreateMainFrameworkApplication()
        {
            _mainFrameworkApplication = new MainFrameworkApplication(this, this);
        }
        private void InitializeInfrastructure()
        {
            RegisterInfrastructures();
            InitializeInfrastructures();
        }
        private void RegisterInfrastructures()
        {

        }
        private void InitializeInfrastructures()
        {
            foreach (var infrastructure in _infrastructures.Values)
                infrastructure.Initialize();
        }
        private void SetupPersistence()
        {
            RegisterDontDestroyOnLoad();
        }

        private void RegisterDontDestroyOnLoad()
        {
            DontDestroyOnLoad(this);
        }
        public bool TryGetInfrastructure<T>(out T infrastructure) where T : IInfrastructure
        {
            if (_infrastructures.TryGetValue(typeof(T), out var s) && s is T typed)
            {
                infrastructure = typed;
                return true;
            }
            infrastructure = default!;
            return false;
        }
        private void OnDestroy()
        {
            ClearLogger();

            DisposeMainFrameworkApplication();
            DisposeeUnityLogger();
        }
        private void ClearLogger()
        {
            _logger = null;
        }
        private void DisposeeUnityLogger()
        {
            UnityLogger.In.Dispose();
        }
        private void DisposeMainFrameworkApplication()
        {
            _mainFrameworkApplication?.Dispose();
            _mainFrameworkApplication = null;
        }

     
    }
}
