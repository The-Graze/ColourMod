using System;
using System.Collections.Generic;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ColourMod.Scripts
{
	class ColorManager : MonoBehaviourPunCallbacks
	{
        public Player player;

        public VRRig rig;

        private bool CosmeticsSorted;
        private bool mode;

        private Dictionary<GameObject, CosmeticsController.CosmeticCategory> CosmeticsCats = new Dictionary<GameObject, CosmeticsController.CosmeticCategory>();
        private List<GameObject> RawCosmeticsObjects = new List<GameObject>();

        private Color hat;
        private Color badge;
        private Color face;
        private Color holds;
        private Color glov;
        private Color sling;
        private Color Chest;
        private bool CanRun()
		{
			if (player.CustomProperties.ContainsKey("c_Hat"))
			{
				return true;
			}
			else if(rig.isOfflineVRRig)
			{
				 return true;
			}
			else
			{
				return false;
			}
			
		}
		private void Update()
		{
			if (!CosmeticsSorted && PhotonNetwork.IsConnectedAndReady)
			{
				SortCosmetics();
			}
			if (CanRun())
			{
				if (!player.IsLocal)
				{
					ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Hat"].ToString(), out  Color color);
                    ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Badge"].ToString(), out Color color2);
                    ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Hold"].ToString(), out Color color3);
                    ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Sling"].ToString(), out Color color4);
                    ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Glove"].ToString(), out Color color5);
                    ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Face"].ToString(), out Color color6);
                    ColorUtility.TryParseHtmlString("#" + player.CustomProperties["c_Chest"].ToString(), out Color chest);
                    mode = (bool)player.CustomProperties["c_Mode"];
					hat = color;
					badge = color2;
					holds = color3;
					Chest = chest;
					sling = color4;
					glov = color5;
					face = color6;
				}
				else
				{
					hat = Plugin.Instance.HatColour.Value;
					badge = Plugin.Instance.BadgeColour.Value;
					holds = Plugin.Instance.HoldableColour.Value;
					Chest = Plugin.Instance.ChestColour.Value;
					sling = Plugin.Instance.SlingColour.Value;
					glov = Plugin.Instance.GloveColour.Value;
					face = Plugin.Instance.FaceColour.Value;
					mode = Plugin.Instance.ChestMirror.Value;
				}
				if (CosmeticsSorted)
				{
					MakeColours();
				}
			}
			if (!CanRun())
			{
				RemoveColors();
			}
		}
		private void SortCosmetics()
		{
			foreach (GameObject gameObject in rig.cosmetics)
			{
				if (gameObject.name != "OVERRIDDEN")
				{
					if (!RawCosmeticsObjects.Contains(gameObject))
					{
						RawCosmeticsObjects.Add(gameObject);
					}
				}
			}
			foreach (GameObject gameObject2 in rig.overrideCosmetics)
			{

				if (gameObject2.name != "OVERRIDDEN")
				{
					if (!RawCosmeticsObjects.Contains(gameObject2))
					{
						RawCosmeticsObjects.Add(gameObject2);
					}
				}
			}
			SortCosmetics2();
		}
		private void SortCosmetics2()
		{
			foreach (GameObject gameObject in RawCosmeticsObjects)
			{
				string displayName = gameObject.name.Replace("LEFT.", "").Replace("RIGHT.", "");
				string itemNameFromDisplayName = CosmeticsController.instance.GetItemNameFromDisplayName(displayName);
				CosmeticsController.CosmeticCategory itemCategory = CosmeticsController.instance.GetItemFromDict(itemNameFromDisplayName).itemCategory;
				CosmeticsCats.Add(gameObject, itemCategory);
			}
			CosmeticsSorted = true;
		}
		private void RemoveColors()
		{
			foreach (GameObject g in CosmeticsCats.Keys)
			{
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Hat)
				{
					if (g.GetComponent<Renderer>() != null)
					{
						if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
						{
							g.GetComponent<Renderer>().material.color = Color.white;
						}
						if (g.transform.GetComponentInChildren<Renderer>(true) != null)
						{
							if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
							{
								g.transform.GetComponentInChildren<Renderer>(true).material.color = Color.white;
							}
						}
					}
					if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Badge)
					{
						if (g.GetComponent<Renderer>() != null)
						{
							if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
							{
								g.GetComponent<Renderer>().material.color = Color.white;
							}
						}
						if (g.transform.GetComponentInChildren<Renderer>(true) != null)
						{
							bool flag10 = g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color");
							if (flag10)
							{
								g.transform.GetComponentInChildren<Renderer>(true).material.color = Color.white;
							}
						}
					}
					if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Face)
					{
						if (g.GetComponent<Renderer>() != null)
						{
							bool flag13 = g.GetComponent<Renderer>().material.HasProperty("_Color");
							if (flag13)
							{
								g.GetComponent<Renderer>().material.color = Color.white;
							}
						}
						if (g.transform.GetComponentInChildren<Renderer>(true) != null)
						{
							if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
							{
								g.transform.GetComponentInChildren<Renderer>(true).material.color = Color.white;
							}
						}
					}
					if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Holdable)
					{
						if (g.GetComponent<Renderer>() != null)
						{
							if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
							{
								g.GetComponent<Renderer>().material.color = Color.white;
							}
						}
						if (g.transform.GetComponentInChildren<Renderer>(true) != null)
						{
							if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
							{
								g.transform.GetComponentInChildren<Renderer>(true).material.color = Color.white;
							}
						}
					}
					if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Gloves)
					{
						if (g.GetComponent<Renderer>() != null)
						{
							bool flag23 = g.GetComponent<Renderer>().material.HasProperty("_Color");
							if (flag23)
							{
								g.GetComponent<Renderer>().material.color = Color.white;
							}
						}
						if (g.transform.GetComponentInChildren<Renderer>(true) != null)
						{
							if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
							{
								g.transform.GetComponentInChildren<Renderer>(true).material.color = Color.white;
							}
						}
					}
					if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Slingshot)
					{
						if (g.GetComponent<Renderer>() != null)
						{
							if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
							{
								g.GetComponent<Renderer>().material.color = Color.white;
							}
						}
						if (g.transform.GetComponentInChildren<Renderer>(true) != null)
						{
							if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
							{
								g.transform.GetComponentInChildren<Renderer>(true).material.color = Color.white;
							}
						}
					}
				}
			}
		}
		private void MakeColours()
		{
			foreach (GameObject g in CosmeticsCats.Keys)
			{
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Hat)
				{
					if (g.GetComponent<Renderer>() != null)
					{
						if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
						{
							g.GetComponent<Renderer>().material.color = hat;
						}
					}
					if (g.transform.GetComponentInChildren<Renderer>(true) != null)
					{
						if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
						{
							g.transform.GetComponentInChildren<Renderer>(true).material.color = hat;
						}
					}
				}
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Badge)
				{
                    if (g.GetComponent<Renderer>() != null)
                    {
                        if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
                        {
                            g.GetComponent<Renderer>().material.color = badge;
                        }
                    }
                    if (g.transform.GetComponentInChildren<Renderer>(true) != null)
                    {
                        if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
                        {
                            g.transform.GetComponentInChildren<Renderer>(true).material.color = badge;
                        }
                    }
                }
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Face)
				{
                    if (g.GetComponent<Renderer>() != null)
                    {
                        if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
                        {
                            g.GetComponent<Renderer>().material.color = face;
                        }
                    }
                    if (g.transform.GetComponentInChildren<Renderer>(true) != null)
                    {
                        if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
                        {
                            g.transform.GetComponentInChildren<Renderer>(true).material.color = face;
                        }
                    }
                }
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Holdable)
				{
                    if (g.GetComponent<Renderer>() != null)
                    {
                        if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
                        {
                            g.GetComponent<Renderer>().material.color = holds;
                        }
                    }
                    if (g.transform.GetComponentInChildren<Renderer>(true) != null)
                    {
                        if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
                        {
                            g.transform.GetComponentInChildren<Renderer>(true).material.color = holds;
                        }
                    }
                }
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Slingshot)
				{
                    if (g.GetComponent<Renderer>() != null)
                    {
                        if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
                        {
                            g.GetComponent<Renderer>().material.color = sling;
                        }
                    }
                    if (g.transform.GetComponentInChildren<Renderer>(true) != null)
                    {
                        if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
                        {
                            g.transform.GetComponentInChildren<Renderer>(true).material.color = sling;
                        }
                    }
                }
				if (CosmeticsCats[g] == CosmeticsController.CosmeticCategory.Gloves)
                {
                    if (g.GetComponent<Renderer>() != null)
                    {
                        if (g.GetComponent<Renderer>().material.HasProperty("_Color"))
                        {
                            g.GetComponent<Renderer>().material.color = glov;
                        }
                    }
                    if (g.transform.GetComponentInChildren<Renderer>(true) != null)
                    {
                        if (g.transform.GetComponentInChildren<Renderer>(true).material.HasProperty("_Color"))
                        {
                            g.transform.GetComponentInChildren<Renderer>(true).material.color = glov;
                        }
                    }
                }
			}
		}
	}
}
