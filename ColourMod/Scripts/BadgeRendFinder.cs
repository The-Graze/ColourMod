using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ColourMod
{
    class BadgeRendFinder : MonoBehaviour
    {
        Plugin p = Plugin.Instance;
        void Start()
        {
            foreach(Transform t in transform) 
            {
                if (t.GetComponent<Renderer>() != null && t.GetComponent<Renderer>() != p.localChest)
                {
                    p.BadgeRend.Add(t.GetComponent<Renderer>());
                }
                else if (t.name != "head")
                {
                    t.gameObject.AddComponent<BadgeRendFinder>();
                    Destroy(this);
                }
            }
        }
    }
}