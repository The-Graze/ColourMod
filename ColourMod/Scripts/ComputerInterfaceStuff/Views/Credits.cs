using ComputerInterface;
using ComputerInterface.ViewLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColourMod.Scripts.ComputerInterfaceStuff.Views
{
    internal class Credits : ComputerView
    {
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            var str = new StringBuilder();
            str.BeginCenter().Append("Main Idea By Epicly").AppendLine();
            str.BeginCenter().Append("\nLarge Start and 2nd maker, blauk");
            str.BeginCenter().Append("\n\nMain Mod/code by Graze");
            str.BeginCenter().Append("\n\n\nAnd thank you for using :P");
            str.BeginCenter().Append("\n\n\n\n(any any key to go back)");
            Text = str.ToString();
        }
        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                default:
                    ReturnView();
                    break;
            }
        }
    }
}
