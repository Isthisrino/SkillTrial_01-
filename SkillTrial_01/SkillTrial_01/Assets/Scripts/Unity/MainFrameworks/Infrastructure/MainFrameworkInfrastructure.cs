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
    public class MainFrameworkInfrastructure : MonoBehaviour, IMainFrameworkInfrastructureProvider
    {
        private ILoggerEx _logger = LogAppSystem.In.CreateLogger<MainFrameworkInfrastructure>();

        private IMainFrameworkApplication _mainFrameworkApplication;

        private Dictionary<Type, IInfrastructure> _infrastructures;

        public IDomainSystemFactory DomainSystemFactory { get; protected set; }
        public IAppSystemFactory AppSystemFactory { get; protected set; }
        public IInfrastructureFactory InfrastructureFactory { get; protected set; }

        private void Awake()
        {
            InitializeUnityLogger();
            InitializeFactories();
            InitializeApplication();
            InitializeInfrastructure();
            SetupPersistence();
        }
        private void InitializeUnityLogger()
        {
            UnityLogger.In.SubscribeToLogAppService();
        }
        private void InitializeFactories()
        {
            CreateServiceFactory();
            CreateAppServiceFactory();
            CreateInfrastructFactory();
        }
        protected virtual void CreateServiceFactory()
        {
            DomainSystemFactory = new DomainSystemFactoryBase();
        }
        protected virtual void CreateAppServiceFactory()
        {
            AppSystemFactory = new AppSystemFactoryBase();
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
            _mainFrameworkApplication = new MainFrameworkApplication(this);
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
