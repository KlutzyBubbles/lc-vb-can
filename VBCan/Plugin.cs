using BepInEx;
using System;
using System.IO;
using UnityEngine;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine.UIElements;

namespace VBCan
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Texture2D texture;

        public static ManualLogSource StaticLogger;

        private void Awake()
        {
            StaticLogger = Logger;
            try
            {
                string path = Directory.GetFiles(Path.Combine(Paths.PluginPath, PluginInfo.PLUGIN_NAME), "vb.png")[0];
                texture = new Texture2D(2, 2);
                texture.LoadImage(File.ReadAllBytes(path));
            }
            catch (Exception e)
            {
                Logger.LogError($"Unable to load VB can texture with error: {e.Message}");
            }
            new Harmony(PluginInfo.PLUGIN_GUID).PatchAll(typeof(Patches));
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}