using System;
using BepInEx;
using BepInEx.Configuration;
using ExitGames.Client.Photon;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

namespace ColourMod
{
	[BepInPlugin("com.graze.gorillatag.colourmod", "ColourMod", "1.0.0")]
	public class Plugin : BaseUnityPlugin
	{
        public static volatile Plugin Instance;
        private bool gothats;
        private bool getting;
        private bool don;

        private int a;

        public ConfigEntry<Color> HatColour;
        public ConfigEntry<Color> ChestColour;
        public ConfigEntry<Color> BadgeColour;
        public ConfigEntry<Color> HoldableColour;
        public ConfigEntry<Color> GloveColour;
        public ConfigEntry<Color> SlingColour;
        public ConfigEntry<Color> FaceColour;
        public ConfigEntry<bool> ChestMirror;

        public string TheChanger;

        public Renderer localChest;
        public string ChestMode()
		{
			if (ChestMirror.Value)
			{
				return "Mirror";
			}
			else
			{
				return "Colour";
			}
		}

		private void Awake()
		{
			HarmonyPatches.ApplyHarmonyPatches();
            Plugin.Instance = this;
            HatColour = base.Config.Bind<Color>("Hats", "Hat Colour", Color.black, "The colour you wish to have your hat");
            ChestMirror = base.Config.Bind<bool>("Chest", "Chest Mirror", true, "If true the chest will match your players material, if false it will use the colour you pick");
            ChestColour = base.Config.Bind<Color>("Chest", "Chest Colour", Color.black, "The colour you want your chest");
            BadgeColour = base.Config.Bind<Color>("Badge", "Badge/Hand Colour", Color.black, "The colour you want your badges/Glove Items to be");
            HoldableColour = base.Config.Bind<Color>("Holdable", "Holdbale Colour", Color.black, "The colour you want your Holdables");
            GloveColour = base.Config.Bind<Color>("Glove", "Glove Colour", Color.black, "The colour you want your Glove Cosmetics");
            SlingColour = base.Config.Bind<Color>("Slingshot", "Slingshot Colour", Color.black, "The Colour you want your slingshot");
            FaceColour = base.Config.Bind<Color>("Face", "Face Colour", Color.black, "The colour you want your face cosmetics");
        }
		public void UpdateProps()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.AddOrUpdate("c_Hat", ColorUtility.ToHtmlStringRGBA(HatColour.Value));
			hashtable.AddOrUpdate("c_Badge", ColorUtility.ToHtmlStringRGBA(BadgeColour.Value));
			hashtable.AddOrUpdate("c_Hold", ColorUtility.ToHtmlStringRGBA(HoldableColour.Value));
			hashtable.AddOrUpdate("c_Chest", ColorUtility.ToHtmlStringRGBA(ChestColour.Value));
			hashtable.AddOrUpdate("c_Face", ColorUtility.ToHtmlStringRGBA(FaceColour.Value));
			hashtable.AddOrUpdate("c_Sling", ColorUtility.ToHtmlStringRGBA(SlingColour.Value));
			hashtable.AddOrUpdate("c_Glove", ColorUtility.ToHtmlStringRGBA(GloveColour.Value));
			hashtable.AddOrUpdate("c_Mode", ChestMirror.Value);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable, null, null);
		}
		private void Update()
		{
			if (PhotonNetwork.IsConnectedAndReady && !PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("c_Hat"))
			{
				UpdateProps();
			}
		}
	}
}
