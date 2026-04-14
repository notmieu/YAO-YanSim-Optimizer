using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace YanSimOptimizer
{
    [BepInPlugin("com.yansimoptimizer.optimizer", "YanSim Optimizer", "1.3.5")]
    public class YanSimOptimizationMod : BaseUnityPlugin
    {
        private void Awake()
        {
            var harmony = new Harmony("com.yansimoptimizer.patches");
            harmony.PatchAll();
            Logger.LogInfo("YAO 1.3.5 initialized.");
        }

        private void Update()
        {
            if (Time.frameCount % 60 == 0) 
            {
                ProcessOptimization();
            }
        }

        private void ProcessOptimization()
        {
            var students = Resources.FindObjectsOfTypeAll<StudentScript>();
            if (students == null) return;

            foreach (var student in students)
            {
                if (student == null || !student.gameObject.activeInHierarchy) continue;

                if (student.Ragdoll != null && student.Ragdoll.enabled) 
                {
                    if (student.CharacterAnimation != null) 
                    {
                        student.CharacterAnimation.enabled = true;
                    }
                    continue;
                }

                if (student.Dying || student.Attacked)
                {
                    student.enabled = true;
                    if (student.CharacterAnimation != null) 
                    {
                        student.CharacterAnimation.enabled = true;
                    }
                    
                    student.CurrentDestination = student.transform;
                    continue;
                }

                if (!student.Alive) continue;

                student.enabled = true;

                if (student.DistanceToPlayer > 20f && !student.InEvent)
                {
                    if (student.CharacterAnimation != null) 
                    {
                        student.CharacterAnimation.enabled = false;
                    }
                }
                else
                {
                    if (student.CharacterAnimation != null) 
                    {
                        student.CharacterAnimation.enabled = true;
                    }
                }
            }
        }

        private void DisablePathfindingComponent(StudentScript student)
        {
            try 
            {
                FieldInfo pathfindingField = typeof(StudentScript).GetField("Pathfinding");
                if (pathfindingField != null) 
                {
                    object pathfindingValue = pathfindingField.GetValue(student);
                    if (pathfindingValue is MonoBehaviour component) 
                    {
                        component.enabled = false;
                    }
                }
            } 
            catch 
            { 
                // Ignore errors
            }
        }

        [HarmonyPatch(typeof(StudentScript), "UpdateVision")]
        [HarmonyPrefix]
        public static bool UpdateVisionPrefix(StudentScript __instance)
        {
            if (__instance.DistanceToPlayer > 20f && Time.frameCount % 10 != 0) 
            {
                return false;
            }
            return true;
        }
    }
}