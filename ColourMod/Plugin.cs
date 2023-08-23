using BepInEx;
using BepInEx.Configuration;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilla;

namespace ColourMod
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
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
        int a;

        ConfigEntry<Color> HatColour;
        ConfigEntry<Color> ChestColour;
        ConfigEntry<Color> BadgeColour;
        ConfigEntry<Color> HoldableColour;

        public Renderer localChest;
        Material ChestMat;

        List<Color> Colours = new List<Color>();

        void Start()
        {
            Instance = this;
            Utilla.Events.GameInitialized += OnGameInitialized;HarmonyPatches.ApplyHarmonyPatches();
            HatColour = Config.Bind("Hats", "Hat Colour", Color.clear, "The colour you wish to have your hat");
            ChestColour = Config.Bind("Chest", "Chest Colour", Color.black, "The colour you want your chest");
            BadgeColour = Config.Bind("Badge", "Badge/Hand Colour", Color.clear, "The colour you want your badges/Glove Items to be");
            HoldableColour = Config.Bind("Holdable", "Holdbale Colour", Color.clear, "The colour you want your Holdables");
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            foreach (Transform t in GameObject.Find("Main Camera/Cosmetics").transform)
            {
                hatcats.Add(t);
            }
            localChest = GorillaTagger.Instance.offlineVRRig.mainSkin.transform.parent.Find("rig/body/gorillachest").GetComponent<MeshRenderer>();
            ChestMat = localChest.material;
            GameObject.Find("Local Gorilla Player/rig/body").AddComponent<BadgeRendFinder>();

            //commented out for now as i cant seralize Lists i dont think
        /*  Colours.Add(HatColour.Value);
            Colours.Add(ChestColour.Value);
            Colours.Add(BadgeColour.Value);
            Colours.Add(HoldableColour.Value);
            PhotonNetwork.LocalPlayer.CustomProperties.AddOrUpdate("ColourMod",Colours);*/

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


            if (ChestMat.color != ChestColour.Value)
            {
                ChestMat.color = ChestColour.Value;
               // PhotonNetwork.LocalPlayer.CustomProperties.AddOrUpdate("ColourMod", Colours);
            }
            foreach (Renderer rend in hatRends)
            {
                if (rend.material.color != HatColour.Value)
                {
                    rend.material.color = HatColour.Value;
                   // PhotonNetwork.LocalPlayer.CustomProperties.AddOrUpdate("ColourMod", Colours);
                }
            }
            foreach (Renderer rend2 in BadgeRend)
            {
                if (rend2.material.color != BadgeColour.Value)
                {
                    rend2.material.color = BadgeColour.Value;
                   // PhotonNetwork.LocalPlayer.CustomProperties.AddOrUpdate("ColourMod", Colours);
                }
            }
            foreach (Renderer rend3 in HoldbaleRend)
            {
                if (rend3.material.color != HoldableColour.Value)
                {
                    rend3.material.color = HoldableColour.Value;
                   // PhotonNetwork.LocalPlayer.CustomProperties.AddOrUpdate("ColourMod", Colours);
                }
            }
        }
    }
}