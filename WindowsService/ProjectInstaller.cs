using System.ComponentModel;
using System.Configuration.Install;

namespace Artebit.Restaurante.ArtebitGourmetWS
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}