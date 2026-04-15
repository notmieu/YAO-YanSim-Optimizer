using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace YanSimOptimizer
{
    [BepInPlugin("com.yansimoptimizer.optimizer", "YanSim Optimizer", "1.3.6")]
    public class YanSimOptimizationMod : BaseUnityPlugin
    {
        private List<StudentScript> studentCache = new List<StudentScript>();
        private float nextCacheUpdate = 0f;

        private void Awake()
        {
            var harmony = new Harmony("com.yansimoptimizer.patches");
            harmony.PatchAll();
            Logger.LogInfo("YAO 1.3.6: Performance Update.");
        }

        private void Update()
        {
            if (Time.time > nextCacheUpdate)
            {
                studentCache.Clear();
                var allStudents = FindObjectsOfType<StudentScript>();
                if (allStudents != null) studentCache.AddRange(allStudents);
                nextCacheUpdate = Time.time + 5f;
            }

            if (Time.frameCount % 30 == 0) RunOptimization();
        }

        private void RunOptimization()
        {
            if (studentCache.Count == 0) return;

            foreach (var s in studentCache)
            {
                if (s == null || !s.gameObject.activeInHierarchy) continue;

                ///   optymalizacja sie wylacza jesli uczen ragdoll
                if (s.Ragdoll != null && s.Ragdoll.enabled) 
                {
                    if (s.CharacterAnimation != null) s.CharacterAnimation.enabled = true;
                    SetShadows(s, true);
                    continue; 
                }

                ///  sprawdzanie procesu umierania
                if (s.Dying || s.Attacked)
                {
                    s.enabled = true;
                    if (s.CharacterAnimation != null) s.CharacterAnimation.enabled = true;
                    SetShadows(s, true);
                    s.CurrentDestination = s.transform; 
                    continue;
                }

                /// optymalizacja zywych
                if (!s.Alive) continue;

                s.enabled = true; 
                
                float dist = s.DistanceToPlayer;
                bool isDistant = dist > 20f && !s.InEvent && !s.ClubActivity;

                if (s.CharacterAnimation != null) 
                {
                    s.CharacterAnimation.enabled = !isDistant;
                }

                /// optymalizacja cieni na dystansie
                SetShadows(s, dist < 40f);
            }
        }

        private void SetShadows(StudentScript s, bool state)
        {
            var renderers = s.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var r in renderers)
            {
                r.shadowCastingMode = state ? ShadowCastingMode.On : ShadowCastingMode.Off;
            }
        }

        [HarmonyPatch(typeof(StudentScript), "UpdateVision")]
        [HarmonyPrefix]
        public static bool UV_Prefix(StudentScript __instance)
        {
            if (__instance.DistanceToPlayer > 20f && Time.frameCount % 10 != 0) return false;
            return true;
        }
    }
}
