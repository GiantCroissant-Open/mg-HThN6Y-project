namespace MGPC.Tool.Extension.HThN6Y.Design.Projectile.EditorPart
{
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using HellTap.PoolKit;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        public class CreationResult
        {
            public List<GameObject> ProjectileGOs { get; set; }
            public List<GameObject> ProjectilePoolGOs { get; set; }

            public CreationResult()
            {
                ProjectileGOs = new List<GameObject>();
                ProjectilePoolGOs = new List<GameObject>();
            }
        }

        [MenuItem("Assets/MGPC/HThN6Y/Projectile/Create")]
        // Start is called before the first frame update
        public static void Organize()
        {
            // Pre setup

            //
            var creationResult = CreateProjectiles();
            creationResult.ProjectileGOs.ToList().ForEach(x => GameObject.DestroyImmediate(x));
            creationResult.ProjectilePoolGOs.ToList().ForEach(x => GameObject.DestroyImmediate(x));

            // Post setup
        }

        private static CreationResult CreateProjectiles()
        {
            var gameAssetPath = Path.Combine("_", "1 - Game");
            var projectileBasePath = Path.Combine("Assets", gameAssetPath, "Level - Fishing", $"_Generated - Projectiles");
            var absoluteProjectileBasePath = Path.Combine(Application.dataPath, gameAssetPath, "Level - Fishing",
                $"_Generated - Projectiles");
            var absoluteProjectileBasePathExisted = Directory.Exists(absoluteProjectileBasePath);

            if (absoluteProjectileBasePathExisted)
            {
                FileUtil.DeleteFileOrDirectory(projectileBasePath);
            }

            Directory.CreateDirectory(absoluteProjectileBasePath);

            //
            var projectileDataAssetBaseAssetPath = Path.Combine("Assets", gameAssetPath, "Level - Fishing", "Projectiles", "Data Assets");
            var projectileDataAssetAssetPath = Path.Combine(projectileDataAssetBaseAssetPath, "ScG - Projectile.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(projectileDataAssetAssetPath);

            //
            var creationResult = new CreationResult();
            var projectiles = GetProjectiles();

            projectiles.ToList().ForEach(projectile =>
            {
                //
                var projectileGO = CreateProjectileGO(
                    // prefabBaseAssetPath,
                    projectile,
                    flowMachineAsset);

                creationResult.ProjectileGOs.Add(projectileGO);

                //
                var prefabAssetPath = Path.Combine(projectileBasePath, $"{projectile.Id}.prefab");
                var projectilePrefab = PrefabUtility.SaveAsPrefabAsset(projectileGO, prefabAssetPath);

                //
                var poolGO = CreatePoolGO(projectile.Id, projectilePrefab);

                creationResult.ProjectilePoolGOs.Add(poolGO);

                var poolPrefabAssetPath = Path.Combine(projectileBasePath, $"Pool - {projectile.Id}.prefab");
                PrefabUtility.SaveAsPrefabAsset(poolGO, poolPrefabAssetPath);
            });
            AssetDatabase.Refresh();

            return creationResult;
        }

        private static GameObject CreateProjectileGO(
            // string prefabAssetPath,
            Generated.Projectile projectile,
            ScriptGraphAsset scriptGraphAsset)
        {
            var instanceGO = new GameObject($"{projectile.Id}");
            instanceGO.layer = 12;

            //
            var boxCollider = instanceGO.AddComponent<BoxCollider>();

            boxCollider.isTrigger = true;

            var sizeSplitResult = projectile.ColliderSize.Split(' ');
            var collierSizeParsedResult = sizeSplitResult.Select(sr => float.Parse(sr)).ToList();
            var colliderSize = (new Vector3(collierSizeParsedResult[0], collierSizeParsedResult[1], collierSizeParsedResult[2]));

            // boxCollider.center = colliderCenter;
            boxCollider.size = colliderSize;

            var rigidbody = instanceGO.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            //
            var variablesComp = instanceGO.AddComponent<Variables>();
            var flowMachineComp = instanceGO.AddComponent<FlowMachine>();

            //
            variablesComp.declarations.Set("physicsSpeedZ", projectile.Speed);
            variablesComp.declarations.Set("duration", projectile.Duration);

            //
            // variablesComp.declarations.Set("gameController", instanceGO);

            //
            flowMachineComp.nest.SwitchToMacro(scriptGraphAsset);

            //
            var avatarGO = new GameObject("Avatar");
            avatarGO.transform.SetParent(instanceGO.transform);

            return instanceGO;
        }

        private static GameObject CreatePoolGO(
            string poolId,
            GameObject projectilePrefab)
        {
            var instanceGO = new GameObject($"{poolId}");

            var pool = instanceGO.AddComponent<Pool>();
            pool.poolName = $"{poolId}";

            var poolItem = new PoolItem();
            poolItem.prefabToPool = projectilePrefab;
            poolItem.poolSize = 50;

            pool.poolItems = new[] {poolItem};

            return instanceGO;
        }
    }
}
