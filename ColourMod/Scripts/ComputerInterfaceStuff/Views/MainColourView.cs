using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColourMod.Scripts.ComputerInterfaceStuff.Views
{
    class MainColourView : ComputerView
    {
        string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        Color32 RandomColor()
        {
            return new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        }

        string RandomMenuColor()
        {
            string C = "<color=#" + ColorToHex(RandomColor()) + ">C</color>";
            string O = "<color=#" + ColorToHex(RandomColor()) + ">O</color>";
            string L = "<color=#" + ColorToHex(RandomColor()) + ">L</color>";
            string O1 = "<color=#" + ColorToHex(RandomColor()) + ">O</color>";
            string U = "<color=#" + ColorToHex(RandomColor()) + ">U</color>";
            string R = "<color=#" + ColorToHex(RandomColor()) + ">R</color>";
            string M = "<color=#" + ColorToHex(RandomColor()) + ">M</color>";
            string O2 = "<color=#" + ColorToHex(RandomColor()) + ">O</color>";
            string D = "<color=#" + ColorToHex(RandomColor()) + ">D</color>";
            string ex = "<color=#" + ColorToHex(RandomColor()) + ">!</color>";

            return C + O + L + O1 + U + R + " " + M + O2 + D + ex;
        }
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            rewoo();
        }
        void rewoo()
        {
            var str = new StringBuilder();
            str.BeginCenter().Append(RandomMenuColor()).AppendLine();
            str.BeginCenter().Append("\nPress Opion 1-Settings");
            str.BeginCenter().Append("\n\nPress Opion 2-Quick Toggle Chest Mode");
            str.BeginCenter().Append("\n\n\nCurrent Chest Settings: " + Plugin.Instance.ChestMode());
            Text = str.ToString();
        }
        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:

                    ReturnToMainMenu();
                    break;
                case EKeyboardKey.Option1:
                    ShowView<Settings>();
                    break;
                case EKeyboardKey.Option2:
                    Plugin.Instance.ChestMirror.Value = !Plugin.Instance.ChestMirror.Value;
                    Plugin.Instance.UpdateProps();
                    rewoo();
                    break;
                case EKeyboardKey.Option3:
                    ShowView<Credits>();
                    break;
            }
        }
    }
}
