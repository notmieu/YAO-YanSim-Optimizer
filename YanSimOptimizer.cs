using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
using System.IO;

namespace YanSimOptimizer
{
    [BepInPlugin("com.yansimoptimizer.optimizer", "YanSim Optimizer", "1.3.1")]
    public class YanSimOptimizationMod : BaseUnityPlugin
    {
        private ConfigEntry<float> configUpdateRate;
        private List<StudentScript> studentCache = new List<StudentScript>();
        private float nextCacheUpdate = 0f;
        private float nextLogicTick = 0f;

        
        private Texture2D logoTexture;
        private float displayTimer = 5f;

        private void Awake()
        {
            configUpdateRate = Config.Bind("Performance", "UpdateTickRate", 0.5f, "Odswiezanie NPC");

            var harmony = new Harmony("com.yansimoptimizer.patches");
            harmony.PatchAll();

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
            
            QualitySettings.lodBias = 0.5f;

            
            LoadEmbeddedLogo();

            Logger.LogInfo("YAO 1.5.0 initialized. Logo loaded from resources.");
        }

        private void LoadEmbeddedLogo()
        {
            try
            {
                
                string resourceName = "YanSimOptimizer.yaologo.png";
                Assembly assembly = Assembly.GetExecutingAssembly();
                
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        
                        logoTexture = new Texture2D(2, 2);
                        logoTexture.LoadImage(buffer);
                    }
                    else
                    {
                        Logger.LogWarning("Nie znaleziono pliku logo w zasobach .csproj!");
                    }
                }
            }
            catch (System.Exception e)
            {
                Logger.LogError("Blad logo: " + e.Message);
            }
        }

        private void OnGUI()
        {
            if (displayTimer > 0 && logoTexture != null)
            {
                
                float w = 480;
                float h = 120;
              
                Rect pos = new Rect(Screen.width - w - 20, Screen.height - h - 20, w, h);
                GUI.DrawTexture(pos, logoTexture);
                
                displayTimer -= Time.deltaTime;
            }
        }

        private void Update()
        {
            if (Time.frameCount % 10000 == 0)
            {
                System.GC.Collect();
            }

            if (Time.time > nextCacheUpdate)
            {
                UpdateStudentCache();
                nextCacheUpdate = Time.time + 10f;
            }

            if (Time.time > nextLogicTick)
            {
                RunOptimization();
                nextLogicTick = Time.time + configUpdateRate.Value;
            }
        }

        private void UpdateStudentCache()
        {
            studentCache.Clear();
            StudentScript[] allStudents = Object.FindObjectsOfType<StudentScript>();
            if (allStudents != null) studentCache.AddRange(allStudents);
        }

        private void RunOptimization()
        {
            if (studentCache.Count == 0) return;

            foreach (var s in studentCache)
            {
                if (s == null || !s.gameObject.activeInHierarchy) continue;

                if ((s.Ragdoll != null && s.Ragdoll.enabled) || s.Dying || s.Attacked)
                {
                    ResetNPC(s);
                    continue; 
                }

                if (!s.Alive) continue;

                float dist = s.DistanceToPlayer;
                bool isDistant = dist > 20f && !s.InEvent && !s.ClubActivity;

                if (s.CharacterAnimation != null) 
                    s.CharacterAnimation.enabled = !isDistant;

                ToggleNPCRendering(s, !isDistant);
            }
        }

        private void ToggleNPCRendering(StudentScript s, bool fullPower)
        {
            var renderers = s.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var r in renderers)
            {
                r.shadowCastingMode = fullPower ? ShadowCastingMode.On : ShadowCastingMode.Off;
                r.receiveShadows = fullPower;
            }
        }

        private void ResetNPC(StudentScript s)
        {
            if (s.CharacterAnimation != null) s.CharacterAnimation.enabled = true;
            ToggleNPCRendering(s, true);
        }

        [HarmonyPatch(typeof(StudentScript), "UpdateVision")]
        [HarmonyPrefix]
        public static bool UV_Prefix(StudentScript __instance)
        {
            if (__instance.DistanceToPlayer > 20f) return Time.frameCount % 10 == 0;
            return true;
        }
    }
}
