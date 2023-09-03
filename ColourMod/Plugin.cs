using BepInEx;
using BepInEx.Configuration;
using Bepinject;
using ColourMod.Scripts.ComputerInterfaceStuff;
using ColourMod.Scripts.Networking;
using GorillaNetworking;
using GorillaTag;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ColourMod
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
   // [BepInDependency("tonimacaroni.computerinterface")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static volatile Plugin Instance;

        List<GameObject> Hats = new List<GameObject>();
        List<Renderer> hatRends = new List<Renderer>();
        List<Transform> hatcats = new List<Transform>();
        public List<Renderer> BadgeRend = new List<Renderer>();
        public List<Renderer> HoldbaleRend = new List<Renderer>();

        bool gothats;
        bool getting;
        bool don;

        int a;

        public ConfigEntry<Color> HatColour;
        public ConfigEntry<Color> ChestColour;
        public ConfigEntry<Color> BadgeColour;
        public ConfigEntry<Color> HoldableColour;
        public ConfigEntry<bool> ChestMirror;

        public Dictionary<TransferrableObject, Player> OthersHoldables = new Dictionary<TransferrableObject, Player>();

        public string TheChanger;

        public Renderer localChest;
        public string ChestMode()
        {
            if (ChestMirror.Value == true)
            {
                return "Mirror";
            }
            else return "Colour";
        }

        void Start()
        {
            Instance = this;
            Utilla.Events.GameInitialized += OnGameInitialized;HarmonyPatches.ApplyHarmonyPatches();
            HatColour = Config.Bind("Hats", "Hat Colour", Color.clear, "The colour you wish to have your hat");
            ChestMirror = Config.Bind("Chest", "Chest Mirror", true, "If true the chest will match your players material, if false it will use the colour you pick");
            ChestColour = Config.Bind("Chest", "Chest Colour", Color.black, "The colour you want your chest");
            BadgeColour = Config.Bind("Badge", "Badge/Hand Colour", Color.black, "The colour you want your badges/Glove Items to be");
            HoldableColour = Config.Bind("Holdable", "Holdbale Colour", Color.black, "The colour you want your Holdables");
            Zenjector.Install<MainInstaller>().OnProject();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            foreach (Transform t in GameObject.Find("Main Camera/Cosmetics").transform)
            {
                if (t.childCount > 0)
                {
                    hatcats.Add(t);
                }
            }
            foreach (Transform tt in GameObject.Find("Local Gorilla Player/rig/body/head").transform)
            {
                if (tt.childCount > 0)
                {
                    hatcats.Add(tt);
                }
            }
            localChest = GorillaTagger.Instance.offlineVRRig.mainSkin.transform.parent.Find("rig/body/gorillachest").GetComponent<MeshRenderer>();
            GameObject.Find("Local Gorilla Player/rig/body").AddComponent<BadgeRendFinder>();
            gothats = false;
            foreach (Transform t in GameObject.Find("Player Objects/RigCache/Rig Parent").transform)
            {
                t.gameObject.AddComponent<DuhHeheh>();
            }
            UpdateProps();
        }

        public void UpdateProps()
        {
            Hashtable hashtable = new Hashtable();

            hashtable.AddOrUpdate("c_Hat", ColorUtility.ToHtmlStringRGBA(HatColour.Value));
            hashtable.AddOrUpdate("c_Badge", ColorUtility.ToHtmlStringRGBA(BadgeColour.Value));
            hashtable.AddOrUpdate("c_Hold", ColorUtility.ToHtmlStringRGBA(HoldableColour.Value));
            hashtable.AddOrUpdate("c_Chest", ColorUtility.ToHtmlStringRGBA(ChestColour.Value));
            hashtable.AddOrUpdate("c_Mode", ChestMirror.Value);

            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }

        IEnumerator GetHats()
        {
            foreach (Transform tr in hatcats[a])
            {
                Hats.Add(tr.gameObject);
            }
            if(a == hatcats.Count)
            {
                yield return gothats = true;
            }
            else
            {
                getting = false;
                yield return a++;
            }
        }

        IEnumerator SetHatColour()
        {
            gothats = true;
            foreach(GameObject hat in Hats) 
            {
                if (hat.GetComponent<Renderer>() != null)
                {
                    hatRends.Add(hat.GetComponent<Renderer>());
                }
                else
                {
                   foreach (Transform t in hat.transform)
                   {
                        if (t.GetComponent<Renderer>() != null)
                        {
                            hatRends.Add(t.GetComponent<Renderer>());
                        }
                   }
                }
            }
            yield return getting = false;
        }
        void Update()
        {
            if(getting == false && hatcats.Count > 3 && gothats == false)
            {
                getting = true;
                StartCoroutine(GetHats());
            }
            if (a == hatcats.Count && gothats == false)
            {
                StartCoroutine(SetHatColour());
                gothats = true;
            }

            if (ChestMirror.Value == true && localChest.material.name != GorillaTagger.Instance.offlineVRRig.mainSkin.material.name)
            {
                localChest.material = new Material(GorillaTagger.Instance.offlineVRRig.mainSkin.material);
            }

            if (localChest.material.color != ChestColour.Value && ChestMirror.Value == false)
            {
                localChest.material.color = ChestColour.Value;
                UpdateProps();
            }
            foreach (Renderer rend in hatRends)
            {
                if (rend.material.HasProperty("_Color"))
                {

                    if (rend.material.color != HatColour.Value)
                    {
                        rend.material.color = HatColour.Value;
                        UpdateProps();
                    }
                }
            }
            foreach (Renderer rend2 in BadgeRend)
            {
                if (rend2.material.HasProperty("_Color"))
                {

                    if (rend2.material.color != BadgeColour.Value)
                    {
                        rend2.material.color = BadgeColour.Value; 
                        UpdateProps();
                    }
                }
            }
            foreach (Renderer rend3 in HoldbaleRend)
            {
                if (rend3.material.HasProperty("_Color"))
                {

                    if (rend3.material.color != HoldableColour.Value)
                    {
                        rend3.material.color = HoldableColour.Value;
                        UpdateProps();
                    }
                }
            }
        }
    }
}