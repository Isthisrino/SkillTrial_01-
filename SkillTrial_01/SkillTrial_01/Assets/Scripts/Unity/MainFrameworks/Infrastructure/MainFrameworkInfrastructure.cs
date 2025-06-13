using Elder.Core.Common.BaseClasses;
using Elder.Core.Common.Interfaces;
using Elder.Core.MainFrameworks.Application;
using Elder.Core.MainFrameworks.Interfaces;
using Elder.Unity.Logging.Infrastructure;
using UnityEngine;

namespace Elder.Unity.MainFrameworks.Infrastructure
{
    public class MainFrameworkInfrastructure : MonoBehaviour, IMainFrameworkInfrastructureProvider
    {
        private IMainFrameworkApplication _mainFrameworkApplication;
        
        public IServiceFactory ServiceFactory { get; protected set; }
        public IAppServiceFactory AppServiceFactory { get; protected set; }
        public IInfrastructureFactory InfrastructureFactory { get; protected set; }

        private void Awake()
        {
            InitializeUnityLogger();
            
            CreateServiceFactory();
            CreateAppServiceFactory();
            CreateInfrastructFactory();
            
            CreateMainFrameworkApplication();
            RegisterDontDestroyOnLoad();
        }
        protected virtual void CreateServiceFactory()
        {
            ServiceFactory = new ServiceFactoryBase();
        }
        protected virtual void CreateAppServiceFactory()
        {
            AppServiceFactory = new AppServiceFactoryBase();
        }
        protected virtual void CreateInfrastructFactory()
        {
            InfrastructureFactory = new InfrastructureFactoryBase();
        }
        private void InitializeUnityLogger()
        {
            UnityLogger.In.SubscribeToLogAppService();
        }
        private void CreateMainFrameworkApplication()
        {
            _mainFrameworkApplication = new MainFrameworkApplication(this);
        }
        private void RegisterDontDestroyOnLoad()
        {
            DontDestroyOnLoad(this);
        }
        private void OnDestroy()
        {
            DisposeMainFrameworkApplication();
            DisposeeUnityLogger();
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
