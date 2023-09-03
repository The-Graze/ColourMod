using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ColourMod.Patches
{
    [HarmonyPatch(typeof(TransferrableObject))]
    [HarmonyPatch("OnEnable")]
    class HoldablePatch
    {
        private static void Postfix(TransferrableObject __instance)
        {
            if (__instance.ownerRig == GorillaTagger.Instance.offlineVRRig)
            {
                if (__instance.GetComponent<Renderer>() != null)
                {
                    Plugin.Instance.HoldbaleRend.Add(__instance.GetComponent<Renderer>());
                }
                foreach (Transform t in __instance.transform)
                {
                   if (t.GetComponent<Renderer>() != null)
                   {
                       Plugin.Instance.HoldbaleRend.Add(t.GetComponent<Renderer>());
                   }
                }
                
            }
            if (__instance.ownerRig != GorillaTagger.Instance.offlineVRRig && !Plugin.Instance.OthersHoldables.ContainsKey(__instance))
            {
               Plugin.Instance.OthersHoldables.Add(__instance, __instance.ownerRig.creator);
            }
        }
    }
}