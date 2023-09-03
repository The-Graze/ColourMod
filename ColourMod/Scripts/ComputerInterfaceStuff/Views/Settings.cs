using ComputerInterface;
using ComputerInterface.ViewLib;
using ComputerInterface.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColourMod.Scripts.ComputerInterfaceStuff.Views
{
    class Settings : ComputerView
    {
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            var str = new StringBuilder();
            str.BeginCenter().Append("Colour Mod Settings").AppendLine();
            str.BeginCenter().Append("\nPress 1 to Change Hat/Face Colours");
            str.BeginCenter().Append("\n\nPress 2 to Change Badge/Shirt Colours");
            str.BeginCenter().Append("\n\n\nPress 3 to Change Holdable Colour");
            if (Plugin.Instance.ChestMirror.Value == false) str.BeginCenter().Append("\n\n\n\nPress 4 to Chnage Chest Colour");
            Text = str.ToString();
        }
        void rewoo()
        {
            var str = new StringBuilder();
            str.BeginCenter().Append("Colour Mod Settings").AppendLine();
            str.BeginCenter().Append("\nPress 1 to Change Hat/Face cos Colours");
            str.BeginCenter().Append("\n\nPress 2 to Change Badge/Shirt Colours");
            str.BeginCenter().Append("\n\n\nPress 3 to Change Holdable Colour");
            if(Plugin.Instance.ChestMirror.Value == false) str.BeginCenter().Append("\n\n\n\nPress 4 to Chnage Chest Colour");
            Text = str.ToString();
        }
        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:
                    ShowView<MainColourView>();
                    break;
                case EKeyboardKey.Option1:
                    ReturnView();
                    break;
                case EKeyboardKey.NUM1:
                    Plugin.Instance.TheChanger = "Hat";
                    ShowView<ChangeColourView>();
                    break;
                case EKeyboardKey.NUM2:
                    Plugin.Instance.TheChanger = "Badge";
                    ShowView<ChangeColourView>();
                    break;
                case EKeyboardKey.NUM3:
                    Plugin.Instance.TheChanger = "Hold";
                    ShowView<ChangeColourView>();
                    break;
                case EKeyboardKey.NUM4:
                    if (Plugin.Instance.ChestMirror.Value == false)
                    {
                        Plugin.Instance.TheChanger = "Chest";
                        ShowView<ChangeColourView>();
                    }
                    break;
            }
        }
    }
}
