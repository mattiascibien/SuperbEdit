using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace SuperbEdit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using SuperbEdit.Base;
    using SuperbEdit.ViewModels;

    public class AppBootstrapper : BootstrapperBase
    {
        CompositionContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
                Assembly.GetExecutingAssembly(),
                Assembly.LoadFrom("SuperbEdit.Base.dll")
            };
        }

        protected override void Configure()
        {
            container = new CompositionContainer(
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>())
                );

            var batch = new CompositionBatch();


            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override object GetInstance(Type service, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = container.GetExportedValues<object>(contract);
            if (exports.Any())
                return exports.First();
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}