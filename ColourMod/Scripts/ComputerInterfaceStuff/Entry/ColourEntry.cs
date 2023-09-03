using ColourMod.Scripts.ComputerInterfaceStuff.Views;
using ComputerInterface.Interfaces;
using System;

namespace ColourMod.Scripts.ComputerInterfaceStuff.Entry
{
    internal class ColourEntry : IComputerModEntry
    {
        public string EntryName => "Colour Mod";
        public Type EntryViewType => typeof(MainColourView);
    }
}
