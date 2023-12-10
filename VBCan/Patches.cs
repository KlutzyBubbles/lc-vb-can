using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VBCan
{
    internal class Patches
    {
        [HarmonyPatch(typeof(GrabbableObject), "SetScrapValue")]
        [HarmonyPostfix]
        private static void SetScrapValuePatch(GrabbableObject __instance)
        {
            if (__instance.itemProperties.itemName == "Red soda")
            {
                if (__instance.mainObjectRenderer != null)
                {
                    foreach (Material material in __instance.mainObjectRenderer.materials)
                    {
                        material.mainTexture = Plugin.texture;
                    }
                }
                /*
                 *Not sure if this is needed yet
                if (__instance.itemProperties.spawnPrefab != null)
                {
                    StaticLogger.LogInfo($"not null");
                    foreach (Material material in __instance.itemProperties.spawnPrefab.GetComponent<MeshRenderer>().materials)
                    {
                        material.mainTexture = Plugin.texture;
                    }
                }
                */
            }
        }
    }
}
