using System;
using ColourMod.Scripts;
using HarmonyLib;

namespace ColourMod.Patches
{
	[HarmonyPatch(typeof(VRRig))]
	[HarmonyPatch("LateUpdate")]
	internal class RigPatch
	{
		private static void Postfix(VRRig __instance)
		{
			if (__instance.GetComponent<ColorManager>() == null)
			{
				__instance.gameObject.AddComponent<ColorManager>();
			}
			if (__instance.GetComponent<ColorManager>().player != __instance.creator)
			{
				__instance.GetComponent<ColorManager>().player = __instance.creator;
				__instance.GetComponent<ColorManager>().rig = __instance;
			}
		}
	}
}
