using Prism.Regions;
using System.Windows;

namespace AplicativoAuxiliarSoftbus.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(FormataSentencaClarion));
           // regionManager.RegisterViewWithRegion("ContentRegion", typeof(RegistroDeTarefas));
        }
    }
}
