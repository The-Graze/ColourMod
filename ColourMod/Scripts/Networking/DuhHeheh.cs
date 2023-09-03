using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ColourMod.Scripts.Networking
{
    class DuhHeheh : MonoBehaviourPun
    {
        VRRig rig;

        Player player;

        bool cando;
        bool FirsteverSetup;
        bool gothats;
        bool getting;

        int a;

        public Renderer rChest;

        List<GameObject> Hats = new List<GameObject>();
        List<Renderer> hatRends = new List<Renderer>();
        List<Transform> hatcats = new List<Transform>();
        List<Renderer> BadgeRend = new List<Renderer>();
        List<Renderer> HoldbaleRend = new List<Renderer>();

        Color Hat()
        {
            ColorUtility.TryParseHtmlString("#" + (string)player.CustomProperties["c_Hat"], out Color a);
            return a;
        }
        Color Badge()
        {
            ColorUtility.TryParseHtmlString("#" + (string)player.CustomProperties["c_Badge"], out Color a);
            return a;
        }
        Color Hold()
        {
            ColorUtility.TryParseHtmlString("#" + (string)player.CustomProperties["c_Hold"], out Color a);
            return a;
        }
        Color Chest()
        {
            ColorUtility.TryParseHtmlString("#" + (string)player.CustomProperties["c_Chest"], out Color a);
            return a;
        }
        bool Mode()
        {
            return (bool)player.CustomProperties["c_Mode"];
        }
        void OnEnable()
        {
            rig = gameObject.GetComponent<VRRig>();
            if (!player.CustomProperties.ContainsKey("c_Mode"))
            {
                this.enabled= false;
                cando = false;
            }
            else
            {
                HasModStart();
            }
        }
        IEnumerator GetHats()
        {
            foreach (Transform tr in hatcats[a])
            {
                Hats.Add(tr.gameObject);
            }
            if (a == hatcats.Count)
            {
                yield return gothats = true;
            }
            else
            {
                getting = false;
                yield return a++;
            }
        }
        void FirstSetup()
        {
            gothats = false;
            foreach (Transform t in transform.Find("Cosmetics"))
            {
                if (t.childCount > 0)
                {
                    hatcats.Add(t);
                }
            }
            foreach (Transform tt in transform.Find("head"))
            {
                if (tt.childCount > 0)
                {
                    hatcats.Add(tt);
                }
            }
        }
        void HasModStart()
        {
            cando = true;
            if(FirsteverSetup == false) 
            {
                FirstSetup();
            }
            else if(FirsteverSetup) 
            {
                cando = true;
            }
            rChest = rig.mainSkin.transform.parent.Find("rig/body/gorillachest").GetComponent<MeshRenderer>();
        }
        void Update()
        {
            if (cando)
            {
                SetColours();
                if (gothats == false && getting == false)
                {
                    StartCoroutine(GetHats());
                    getting = true;
                }
            }
        }
        void SetColours()
        {
            foreach (Renderer rend in hatRends)
            {
                if (rend.material.HasProperty("_Color"))
                {

                    if (rend.material.color != Hat())
                    {
                        rend.material.color = Hat();
                    }
                }
            }
            foreach (Renderer rend2 in BadgeRend)
            {
                if (rend2.material.HasProperty("_Color"))
                {

                    if (rend2.material.color != Badge())
                    {
                        rend2.material.color = Badge();
                    }
                }
            }
            foreach (Renderer rend3 in HoldbaleRend)
            {
                if (rend3.material.HasProperty("_Color"))
                {

                    if (rend3.material.color != Hold())
                    {
                        rend3.material.color = Hold();
                    }
                }
            }
            if (Mode() == true && rChest.material.name != rig.mainSkin.material.name)
            {
                rChest.material = new Material(rig.mainSkin.material);
            }

            if (rChest.material.color != Chest() && Mode() == false)
            {
                rChest.material.color = Chest();
            }
        }

    }
}
