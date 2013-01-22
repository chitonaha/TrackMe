using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TrackMe.Support.Configuration;
using TrackMe.Model.Interfaces;
using TrackMe.Data.Repositories;
using TrackMe.Business.Interfaces;
using TrackMe.Business;
using TrackMe.Support.IoC.Resources;
using TrackMe.Data;
using TrackMe.Model;
using TrackMe.Support.IoC.LifetimeManagers;

namespace TrackMe.Support.IoC
{
    sealed class IOCUnityContainer : IIOCContainer
    {
        #region private fields

        private readonly IDictionary<string, IUnityContainer> _containerDictionary;

        #endregion

        #region constructor

        public IOCUnityContainer()
        {
            _containerDictionary = new Dictionary<string, IUnityContainer>();

            // Root container
            IUnityContainer rootContainer = new UnityContainer();
            _containerDictionary.Add("RootContext", rootContainer);

            // Real context container.
            IUnityContainer realContainer = rootContainer.CreateChildContainer();
            _containerDictionary.Add("RealContext", realContainer);

            // Fake container
            IUnityContainer fakeContainer = rootContainer.CreateChildContainer();
            _containerDictionary.Add("FakeContext", fakeContainer);

            ConfigureRootContainer(rootContainer);
            ConfigureRealContainer(realContainer);
            ConfigureFakeContainer(fakeContainer);
        }

        #endregion

        #region IServiceFactory Members

        public TService Resolve<TService>()
        {
            IUnityContainer container = GetContainer();

            return container.Resolve<TService>();
        }

        public object Resolve(Type type)
        {
            IUnityContainer container = GetContainer();
            return container.Resolve(type, null);
        }

        public void RegisterType(Type type)
        {
            IUnityContainer container = _containerDictionary["RootContext"];

            if (container != null)
            {
                container.RegisterType(type, new TransientLifetimeManager());
            }
        }

        #endregion

        #region private methods

        private IUnityContainer GetContainer()
        {
            string containerName = AppSettingsManager.Instance.DefaultIoCContainer;

            if (string.IsNullOrEmpty(containerName) || string.IsNullOrWhiteSpace(containerName))
            {
                // Value is not being retrieved from app.config correctly during Application tests
                // Temporarily commenting the exception and overriding until cause can be identified
                //throw new ArgumentNullException(Messages.DefaultIOCSettings);
                containerName = "FakeContext";
            }

            if (!_containerDictionary.ContainsKey(containerName))
            {
                throw new InvalidOperationException(Messages.ContainerCannotBeFound);
            }

            return _containerDictionary[containerName];
        }

        private static void ConfigureRootContainer(IUnityContainer container)
        {
            // Register Repositories mappings
            container.RegisterType<IUserRepository, UserRepository>(new TransientLifetimeManager());
            container.RegisterType<IDeviceRepository, DeviceRepository>(new TransientLifetimeManager());
            container.RegisterType<ITrackRepository, TrackRepository>(new TransientLifetimeManager());
            container.RegisterType<IUserDeviceRepository, UserDeviceRepository>(new TransientLifetimeManager());
            container.RegisterType<IDeviceImageRepository, DeviceImageRepository>(new TransientLifetimeManager());
            container.RegisterType<IImageRepository, ImageRepository>(new TransientLifetimeManager());
            container.RegisterType<IDeviceTypeRepository, DeviceTypeRepository>(new TransientLifetimeManager());
            
            // Register business layer mappings
            container.RegisterType<ITrackingManager, TrackingManager>(new TransientLifetimeManager());
            container.RegisterType<IDeviceManager, DeviceManager>(new TransientLifetimeManager());
            container.RegisterType<IUserManager, UserManager>(new TransientLifetimeManager());

            // Register unit of work mapping
            container.RegisterType<IUnitOfWork, UnitOfWork>(new TransientLifetimeManager());
        }

        private static void ConfigureRealContainer(IUnityContainer container)
        {
            container.RegisterType<IDataContext, TrackMeContext>(new PerExecutionContextLifetimeManager(), new InjectionConstructor());
        }

        private static void ConfigureFakeContainer(IUnityContainer container)
        {
            //container.RegisterType<IQueryableUnitOfWork, FakeMainModuleUnitOfWork>(new PerExecutionContextLifetimeManager(), new InjectionConstructor());
            //container.RegisterType(typeof(IMainModuleUnitOfWork), typeof(FakeMainModuleUnitOfWork), new PerExecutionContextLifetimeManager());
            //container.RegisterType<IApplicationLogger, FakeApplicationLogger>(new TransientLifetimeManager());
        }

        #endregion
    }
}
