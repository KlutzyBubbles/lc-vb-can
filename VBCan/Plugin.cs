using BepInEx;
using System;
using System.IO;
using UnityEngine;
using HarmonyLib;

namespace VBCan
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private static Texture2D texture;

        private void Awake()
        {
            try
            {
                string path = Directory.GetFiles(Paths.PluginPath, "LethalVBCan/vb.png")[0];
                texture = new Texture2D(2, 2);
                texture.LoadImage(File.ReadAllBytes(path));
            }
            catch (Exception e)
            {
                Logger.LogError($"Unable to load VB can texture with error: {e.Message}");
            }
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

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
                        material.mainTexture = texture;
                    }
                }
                /*
                 * Not sure if this is needed yet
                if (__instance.itemProperties.spawnPrefab != null)
                {
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