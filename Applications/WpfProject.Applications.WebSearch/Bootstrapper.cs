namespace WpfProject.Applications.WebSearch
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;
    using WpfProject.Applications.WebSearch.ViewModels;
    using WpfProject.Services;

    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            this.Initialize();
        }

        protected override void Configure()
        {
            this.container = new SimpleContainer();

            this.container.Singleton<IWindowManager, WindowManager>();
            this.container.PerRequest<MainWindowViewModel>();
            this.container.PerRequest<SearchViewModel>();

            this.container.PerRequest<IRequestService, RequestService>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewForAsync<MainWindowViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }
    }
}
