using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static Photon.Pun.UtilityScripts.TabViewManager;

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
        bool gotcats;
        bool gotrends;

        int a;
        int b;

        public Renderer rChest;

        List<GameObject> Hats = new List<GameObject>();
        List<GameObject> Badges = new List<GameObject>();
        List<Renderer> hatRends = new List<Renderer>();
        List<Transform> hatcats = new List<Transform>();
        List<Transform> BadgeCats = new List<Transform>();
        public List<Renderer> BadgeRend = new List<Renderer>();
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
            player = rig.creator;
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
                if (tr.name != "gorillaface")
                {
                    Hats.Add(tr.gameObject);
                }
            }
            if (a == hatcats.Count)
            {
                yield return gothats = true;
            }
            else
            {
                getting = false;
                yield return a++; StartCoroutine(GetHats());
            }
        }
        IEnumerator GetBadges()
        {
            foreach (Transform tr in BadgeCats[b])
            {
                if (tr.GetComponent<Renderer>() != rChest && tr.name != "gorilla" && tr.name != "head")
                {
                    Badges.Add(tr.gameObject);
                }
            }
            if (b == BadgeCats.Count)
            {
                yield return gothats = true;
            }
            else
            {
                getting = false;
                yield return b++; StartCoroutine(GetBadges());
            }
        }
        void FirstSetup()
        {
            foreach (Transform tt in transform.GetChild(4).GetChild(1).GetChild(21))
            {
                if (tt.childCount > 0)
                {
                    hatcats.Add(tt);
                }
            }
            foreach (Transform t in transform.GetChild(4).GetChild(1))
            {
                BadgeCats.Add(t);
            }
            gotcats = true;
            FirsteverSetup = true;
            HasModStart();
        }
        void HasModStart()
        {
            cando = true;
            if(FirsteverSetup == false) 
            {
                FirstSetup();
            }
            else if(FirsteverSetup == true) 
            {
                cando = true;
                StartCoroutine(GetHats());
                StartCoroutine(GetBadges());
            }
            rChest = transform.GetChild(4).GetChild(1).GetChild(20) .GetComponent<MeshRenderer>();
        }

        void GetHoldRend(TransferrableObject tob)
        {
            if (tob.GetComponent<Renderer>() != null)
            {
                if (!HoldbaleRend.Contains(tob.GetComponent<Renderer>()))
                {
                    HoldbaleRend.Add(tob.GetComponent<Renderer>());
                }
            }
            foreach (Transform t in tob.transform)
            {
                if (t.GetComponent<Renderer>() != null)
                {
                    if (!HoldbaleRend.Contains(t.GetComponent<Renderer>()))
                    {
                        HoldbaleRend.Add(t.GetComponent<Renderer>());
                    }
                }
            }
        }
        void GetHatRend(Transform StartT)
        {
            if (StartT.GetComponent<Renderer>() != null)
            {
                if (!hatRends.Contains(StartT.GetComponent<Renderer>()))
                {
                    hatRends.Add(StartT.GetComponent<Renderer>());
                }
            }
            foreach (Transform t in StartT.transform)
            {
                if (t.GetComponent<Renderer>() != null)
                {
                    if (!hatRends.Contains(t.GetComponent<Renderer>()))
                    {
                        hatRends.Add(t.GetComponent<Renderer>());
                    }
                }
            }
        }
        void GetBadgeRend(Transform StartT)
        {
            if (StartT.GetComponent<Renderer>() != null)
            {
                if (!BadgeRend.Contains(StartT.GetComponent<Renderer>()))
                {
                    BadgeRend.Add(StartT.GetComponent<Renderer>());
                }
            }
            foreach (Transform t in StartT.transform)
            {
                if (t.GetComponent<Renderer>() != null)
                {
                    if (!BadgeRend.Contains(t.GetComponent<Renderer>()))
                    {
                        BadgeRend.Add(t.GetComponent<Renderer>());
                    }
                }
            }
        }
        void Update()
        {
            if (cando && gotrends == false)
            {
                SetColours();
                if (gotrends == false)
                {
                    foreach (TransferrableObject trb in Plugin.Instance.OthersHoldables.Keys)
                    {
                        GetHoldRend(trb);
                    }
                    foreach (GameObject hat in Hats)
                    {
                        GetHatRend(hat.transform);
                    }
                    foreach (GameObject badge in Badges)
                    {
                        GetBadgeRend(badge.transform);
                    }
                    gotrends = true;
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
