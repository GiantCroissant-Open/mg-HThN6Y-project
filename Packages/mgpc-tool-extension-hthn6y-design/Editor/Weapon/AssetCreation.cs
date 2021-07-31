namespace MGPC.Tool.Extension.HThN6Y.Design.Weapon.EditorPart
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using HellTap.PoolKit;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        [MenuItem("Assets/MGPC/HThN6Y/Weapon/Create")]
        // Start is called before the first frame update
        public static void Organize()
        {
            // Pre setup

            //
            var createdWeapons = CreateWeapons();
            createdWeapons.ToList().ForEach(x => GameObject.DestroyImmediate(x));

            // Post setup
        }

        private static IEnumerable<GameObject> CreateWeapons()
        {
            var gameAssetPath = Path.Combine("_", "1 - Game");
            var creatureBasePath = Path.Combine("Assets", gameAssetPath, "Level - Fishing", $"_Generated - Weapons");
            var absoluteCreatureBasePath = Path.Combine(Application.dataPath, gameAssetPath, "Level - Fishing",
                $"_Generated - Weapons");
            var absoluteCreatureBasePathExisted = Directory.Exists(absoluteCreatureBasePath);

            if (absoluteCreatureBasePathExisted)
            {
                FileUtil.DeleteFileOrDirectory(creatureBasePath);
            }

            Directory.CreateDirectory(absoluteCreatureBasePath);

            //
            var weaponDataAssetBaseAssetPath = Path.Combine("Assets", gameAssetPath, "Level - Fishing", "Weapons", "Data Assets");
            var weaponDataAssetAssetPath = Path.Combine(weaponDataAssetBaseAssetPath, "ScG - Weapon.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(weaponDataAssetAssetPath);

            //
            var weapons = GetWeapons();
            var weaponGOs = new List<GameObject>();

            weapons.ToList().ForEach(weapon =>
            {
                var weaponGO = CreateWeaponGO(
                    // prefabBaseAssetPath,
                    weapon,
                    flowMachineAsset);

                weaponGOs.Add(weaponGO);

                //
                var prefabAssetPath = Path.Combine(creatureBasePath, $"{weapon.Title} - {weapon.Rank}.prefab");
                PrefabUtility.SaveAsPrefabAsset(weaponGO, prefabAssetPath);
            });
            AssetDatabase.Refresh();

            return weaponGOs;
        }

        private static GameObject CreateWeaponGO(
            // string prefabAssetPath,
            Generated.Weapon weapon,
            ScriptGraphAsset scriptGraphAsset)
        {
            var instanceGO = new GameObject($"{weapon.Title} - {weapon.Rank}");

            //
            var variablesComp = instanceGO.AddComponent<Variables>();
            var flowMachineComp = instanceGO.AddComponent<FlowMachine>();

            //
            var gameAssetPath = Path.Combine("_", "1 - Game");
            var projectilePrefabBaseAssetPath = Path.Combine("Assets", gameAssetPath, "Level - Fishing", "_Generated - Projectiles");
            var projectilePrefabAssetPath = Path.Combine(projectilePrefabBaseAssetPath, $"{weapon.NormalProjectile}.prefab");
            var projectilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(projectilePrefabAssetPath);

            variablesComp.declarations.Set("projectilePrefab", projectilePrefab);


            //
            flowMachineComp.nest.SwitchToMacro(scriptGraphAsset);

            //
            var shootingOrigin = new GameObject("Shooting Origin");
            shootingOrigin.transform.SetParent(instanceGO.transform);
            shootingOrigin.transform.localRotation = Quaternion.identity;
            shootingOrigin.transform.localPosition = new Vector3(0.0f, 0.0f, 1.5f);

            variablesComp.declarations.Set("shootingOrigin", shootingOrigin);

            // Create avatar child
            var weaponArtPath = GetWeaponArtPath(weapon.Id, weapon.Title, weapon.ArtAssetName, (int)weapon.Rank);
            var weaponArtPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(weaponArtPath);

            var weaponArtInstance = PrefabUtility.InstantiatePrefab(weaponArtPrefab) as GameObject;
            if (weaponArtInstance != null)
            {
                weaponArtInstance.transform.SetParent(instanceGO.transform);
            }

            return instanceGO;
        }

        private static string GetWeaponArtPath(
            string weaponId,
            string title,
            string artAssetName,
            int rank)
        {
            var gameAssetPath = Path.Combine("_", "1 - Game");
            var weaponArtBasePath = Path.Combine("Assets", gameAssetPath, "Level - Fishing",
                "Preprocessed Assets", "art", "Wands Pack Cute Series", "Prefabs");

            var category = "";
            if (title == "Fire")
            {
                category = "Red";
            }
            else if (title == "Ice")
            {
                category = "Blue";
            }

            var weaponArtPath = Path.Combine(weaponArtBasePath, category, $"Wand {rank:00} {category}.prefab");

            return weaponArtPath;
        }
    }
}
