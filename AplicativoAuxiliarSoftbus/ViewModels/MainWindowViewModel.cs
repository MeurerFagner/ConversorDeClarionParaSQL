using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace AplicativoAuxiliarSoftbus.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string view)
        {
            if (!string.IsNullOrEmpty(view))
                _regionManager.RequestNavigate("ContentRegion", view);
        }
    }
}
