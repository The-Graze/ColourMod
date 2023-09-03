using ColourMod.Scripts.ComputerInterfaceStuff.Entry;
using ComputerInterface.Interfaces;
using Zenject;
namespace ColourMod.Scripts.ComputerInterfaceStuff
{
    class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IComputerModEntry>().To<ColourEntry>().AsSingle();
        }
    }
}
