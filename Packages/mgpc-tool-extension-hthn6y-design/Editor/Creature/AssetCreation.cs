namespace MGPC.Tool.Extension.HThN6Y.Design.Creature.EditorPart
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using BehaviorDesigner.Runtime;
    using FSG.MeshAnimator;
    using FSG.MeshAnimator.ShaderAnimated;
    using MoreMountains.Feedbacks;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        [MenuItem("Assets/MGPC/HThN6Y/Creature/Create")]
        // Start is called before the first frame update
        public static void Organize()
        {
            // Pre setup
            var gameBasePath = Path.Combine("_", "1 - Game");
            var specificGameBasePath = Path.Combine(gameBasePath, "MG - HThN6Y");

            //
            var generatedCreatureBasePath = Path.Combine(specificGameBasePath, "_Generated - Creatures");

            //
            var specificAssetBasePath = Path.Combine(specificGameBasePath, "Preprocessed Assets");
            var specificDesignAssetPath = Path.Combine(specificAssetBasePath, "design");
            var specificDesignCreatureAssetPath = Path.Combine(specificDesignAssetPath, "creatures");

            //
            Utility.EditorPart.Utility.RecreateDirectory(generatedCreatureBasePath);

            //
            var createdCreatures = CreateCreatures(
                generatedCreatureBasePath,
                specificDesignCreatureAssetPath,
                specificGameBasePath);
            createdCreatures.ToList().ForEach(x => GameObject.DestroyImmediate(x));

            // Post setup
        }

        private static IEnumerable<GameObject> CreateCreatures(
            string generatedCreatureBasePath,
            string specificDesignCreatureAssetPath,
            string specificGameBasePath)
        {
            //
            var creatureDataAssetBasePath = Path.Combine("Assets", specificGameBasePath, "Creatures", "Data Assets");
            var scgCreatureDataAssetPath = Path.Combine(creatureDataAssetBasePath, "ScG - Creature.asset");
            var scriptGraphAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(scgCreatureDataAssetPath);

            var creaturePrefabBasePath = Path.Combine("Assets", specificGameBasePath, "Creatures", "Prefabs");
            var feedbackCreaturePrefabPath = Path.Combine(creaturePrefabBasePath, "MMFeedbacks.prefab");
            var feedbackCreaturePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(feedbackCreaturePrefabPath);

            //
            //
            // var prefabBaseAssetPath = Path.Combine("Assets", specificGameBasePath, "Prefabs");
            // var hitPrefabAssetPath = Path.Combine(prefabBaseAssetPath, "Hit.prefab");
            // var hitPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(hitPrefabAssetPath);
            //
            //

            var absoluteCreatureJsonPath = Path.Combine(Application.dataPath, specificDesignCreatureAssetPath);
            var creatures = GetCreatures(absoluteCreatureJsonPath);

            //
            var creatureGOs = new List<GameObject>();
            creatures.ToList().ForEach(creature =>
            {
                var creatureGO = CreateCreatureGO(
                    specificGameBasePath,
            //         prefabBaseAssetPath,
                    creature,
            //         hitPrefab,
                    scriptGraphAsset,
                    feedbackCreaturePrefab);

                creatureGOs.Add(creatureGO);

                //
                Utility.EditorPart.Utility.RecreateDirectory(Path.Combine(Application.dataPath, generatedCreatureBasePath, $"{creature.Id}"));
                var creatureBasePath = Path.Combine("Assets", generatedCreatureBasePath, $"{creature.Id}");

                var prefabAssetPath = Path.Combine(creatureBasePath, $"{creature.Id}.prefab");
                PrefabUtility.SaveAsPrefabAsset(creatureGO, prefabAssetPath);
            });
            AssetDatabase.Refresh();

            return creatureGOs;
        }

        private static GameObject CreateCreatureGO(
            string specificGameBasePath,
            Generated.Creature creature,
            ScriptGraphAsset scriptGraphAsset,
            GameObject feedbackCreaturePrefab)
        {
            //
            var instanceGO = new GameObject($"{creature.Id}");
            instanceGO.layer = 12;

            //
            var boxCollider = instanceGO.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;

            var centerSplitResult = creature.ColliderCenter.Split(' ');
            var collierCenterParsedResult = centerSplitResult.Select(sr => float.Parse(sr)).ToList();
            var colliderCenter = (new Vector3(
                collierCenterParsedResult[0],
                collierCenterParsedResult[1],
                collierCenterParsedResult[2]));

            //
            var sizeSplitResult = creature.ColliderSize.Split(' ');
            var collierSizeParsedResult = sizeSplitResult.Select(sr => float.Parse(sr)).ToList();
            var colliderSize = (new Vector3(
                collierSizeParsedResult[0],
                collierSizeParsedResult[1],
                collierSizeParsedResult[2]));

            boxCollider.center = colliderCenter * creature.ScaleSize;
            boxCollider.size = colliderSize * creature.ScaleSize;

            //
            var variablesComp = instanceGO.AddComponent<Variables>();
            var flowMachineComp = instanceGO.AddComponent<FlowMachine>();

            variablesComp.declarations.Set("creaturePool", "poolName");

            variablesComp.declarations.Set("gameController", instanceGO);

            //
            flowMachineComp.nest.SwitchToMacro(scriptGraphAsset);

            // BehaviorTree
            var creaturePrefabBasePath = Path.Combine("Assets", specificGameBasePath, "Creatures", "Prefabs");
            var exCreaturePrefabPath = Path.Combine(creaturePrefabBasePath, "Behavior 001.prefab");
            var exCreaturePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(exCreaturePrefabPath);

            UnityEditorInternal.ComponentUtility.CopyComponent(exCreaturePrefab.GetComponent<Behavior>());
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(instanceGO);

            // var behaviorTreeComp = instanceGO.AddComponent<BehaviorTree>();
            // var behaviorParentComp = behaviorTreeComp as Behavior;

            // // behaviorManagerComp.ex
            // var filedInfos = behaviorParentComp
            //     .GetType()
            //     // .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            //     .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            //     // .GetField("externalBehavior", BindingFlags.NonPublic | BindingFlags.Instance);
            //     // .SetValue(behaviorTreeComp, externalBehaviorTreeAsset);
            //
            // filedInfos.ToList().ForEach(fi => Debug.Log($"fieldinfo: {fi}"));
            //
            // // if (filedInfo != null)
            // if (filedInfos.Length > 0)
            // {
            //     // filedInfo.SetValue(behaviorTreeComp, externalBehaviorTreeAsset);
            //     // Debug.Log($"fieldInfo: {filedInfo}");
            // }


            //
            var mmFeedbackGO = CreateCreatureMMFeedbackGO(
                specificGameBasePath,
                feedbackCreaturePrefab);

            mmFeedbackGO.transform.SetParent(instanceGO.transform);

            variablesComp.declarations.Set("feedback", mmFeedbackGO);


            // // boxCollider.center = colliderCenter;
            // boxCollider.size = colliderSize;

            var avatarGO = CreateCreatureAvatarGO(
                specificGameBasePath,
                creature.ArtAssetName,
                creature.ScaleSize,
                colliderSize);

            avatarGO.transform.SetParent(instanceGO.transform);

            //
            return instanceGO;
        }

        private static GameObject CreateCreatureMMFeedbackGO(
            string specificGameBasePath,
            GameObject feedbackCreaturePrefab)
        {
            var instanceGO = new GameObject($"MMFeedbacks");

            var mmFeedbacksComp = feedbackCreaturePrefab.GetComponent<MMFeedbacks>();
            // instanceGO.AddComponent(mmFeedbacksComp)
            UnityEditorInternal.ComponentUtility.CopyComponent(mmFeedbacksComp);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(instanceGO);

            return instanceGO;
        }

        private static GameObject CreateCreatureAvatarGO(
            string specificGameBasePath,
            string creatureName,
            float scaleSize,
            Vector3 colliderSize)
        {
            var instanceGO = new GameObject($"Avatar");

            Debug.Log($"scaleSize: {scaleSize}");
            instanceGO.transform.localScale = Vector3.one * scaleSize;

            var backedBasePath = Path.Combine(specificGameBasePath, "Backed - Ma");
            var creatureBasePath = Path.Combine(backedBasePath, creatureName);

            //
            // var creatureModelPath = Path.Combine(creatureBasePath, "Models");

            // var lodModelPath = Path.Combine("Assets", creatureModelPath, $"{creatureName}_LOD3.fbx");
            var lodModelPath = Path.Combine("Assets", creatureBasePath, "Backed", $"{creatureName}_LOD3_AnimatedMesh_combinedMesh.asset");
            var allModels = AssetDatabase.LoadAllAssetsAtPath(lodModelPath);
            var hasAnyModel = allModels.Any();

            Debug.Log($"{lodModelPath} {allModels}");
            //
            allModels.ToList().ForEach(x => Debug.Log(x.GetType()));

            var filteredMeshes =
                allModels
                // .Where(m => m is Mesh && m.ToString().Contains($"{creatureName}"))
                .Where(m => m is Mesh)
                .ToList();

            var mesh = filteredMeshes.First() as Mesh;

            //
            var creatureMaterialPath = Path.Combine(creatureBasePath, "Materials");
            var specificMaterialAssetPath = Path.Combine("Assets", creatureMaterialPath, $"Mat_{creatureName}.mat");
            var material = AssetDatabase.LoadAssetAtPath<Material>(specificMaterialAssetPath);


            //
            var meshFilterComp = instanceGO.AddComponent<MeshFilter>();
            var meshRendererComp = instanceGO.AddComponent<MeshRenderer>();

            meshFilterComp.mesh = mesh;

            meshRendererComp.material = material;

            //
            var shaderMeshAnimatorComp = instanceGO.AddComponent<ShaderMeshAnimator>();
            shaderMeshAnimatorComp.baseMesh = mesh;

            var creatureBackedPath = Path.Combine(creatureBasePath, "Backed");

            var filteredFileNames =
                Directory.GetFiles(Path.Combine(Application.dataPath, creatureBackedPath))
                    .Where(file => !file.Contains(".meta") && !file.Contains(".prefab"))
                    .Where(file => !file.Contains("_combinedMesh"))
                    .Select(file => file.Replace(Application.dataPath, "Assets"));
                    // .Select(file => Path.Combine("Assets", file));

            // filteredFileNames.ToList().ForEach(x =>  Debug.Log(x));

            var filteredAnimations =
                filteredFileNames
                    .Select(filePath => AssetDatabase.LoadAssetAtPath<ScriptableObject>(filePath))
                    .Select(so => so as ShaderMeshAnimation)
                    .ToArray();

            shaderMeshAnimatorComp.meshAnimations = filteredAnimations;
            shaderMeshAnimatorComp.defaultMeshAnimation = filteredAnimations[0];

            return instanceGO;
        }

        // private static GameObject CreateCreatureGO(
        //     string prefabAssetPath,
        //     Generated.Creature creature,
        //     GameObject hitPrefab,
        //     ScriptGraphAsset scriptGraphAsset)
        // {
        //     var instanceGO = new GameObject($"{creature.Title}");
        //
        //     //
        //     var boxCollider = instanceGO.AddComponent<BoxCollider>();
        //     // var centerSplitResult = creature.ColliderCenter.Split(' ');
        //     // var collierCenterParsedResult = centerSplitResult.Select(sr => float.Parse(sr)).ToList();
        //     // var colliderCenter = (new Vector3(collierCenterParsedResult[0], collierCenterParsedResult[1], collierCenterParsedResult[2])) * creature.ScaleSize;
        //
        //     var sizeSplitResult = creature.ColliderSize.Split(' ');
        //     var collierSizeParsedResult = sizeSplitResult.Select(sr => float.Parse(sr)).ToList();
        //     var colliderSize = (new Vector3(collierSizeParsedResult[0], collierSizeParsedResult[1], collierSizeParsedResult[2])) * creature.ScaleSize;
        //
        //     // boxCollider.center = colliderCenter;
        //     boxCollider.size = colliderSize;
        //
        //     //
        //     var variablesComp = instanceGO.AddComponent<Variables>();
        //     var flowMachineComp = instanceGO.AddComponent<FlowMachine>();
        //
        //     //
        //     variablesComp.declarations.Set("creaturePool", $"Creature - {creature.Title}");
        //     variablesComp.declarations.Set("hitPrefab", hitPrefab);
        //
        //     //
        //     variablesComp.declarations.Set("gameController", instanceGO);
        //
        //     //
        //     flowMachineComp.nest.SwitchToMacro(scriptGraphAsset);
        //
        //     // Create feedback child
        //     // var creatureArtInstance = PrefabUtility.InstantiatePrefab(creatureArtPrefab) as GameObject;
        //     var feedbackGO = new GameObject("Feedback");
        //     feedbackGO.transform.SetParent(instanceGO.transform);
        //
        //     variablesComp.declarations.Set("feedback", feedbackGO);
        //
        //     // Create avatar child
        //     var creatureArtPath = GetCreatureArtPath(creature.Id, creature.ArtAssetName);
        //     var creatureArtPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(creatureArtPath);
        //
        //     var creatureArtInstance = PrefabUtility.InstantiatePrefab(creatureArtPrefab) as GameObject;
        //     if (creatureArtInstance != null)
        //     {
        //         var avatarPositionSplitResult = creature.AvatarPosition.Split(' ');
        //         var avatarPositionParsedResult = avatarPositionSplitResult.Select(sr => float.Parse(sr)).ToList();
        //         var avatarPosition = (new Vector3(avatarPositionParsedResult[0], avatarPositionParsedResult[1], avatarPositionParsedResult[2]));
        //
        //         creatureArtInstance.transform.localPosition = avatarPosition * creature.ScaleSize;
        //         creatureArtInstance.transform.localScale = Vector3.one * creature.ScaleSize;
        //
        //         creatureArtInstance.transform.SetParent(instanceGO.transform);
        //     }
        //
        //     return instanceGO;
        // }

        private static string GetCreatureArtPath(
            string creatureId,
            string artAssetName)
        {
            var gameAssetPath = Path.Combine("_", "1 - Game");
            var creatureArtBasePath = Path.Combine("Assets", gameAssetPath, "Level - Fishing", $"Backed - MA");

            var category = "";
            var creatureArtPath = "";
            if (creatureId == "c13001000")
            {
                category = "Sea";
            }
            else if (creatureId == "c13002000")
            {
                category = "Sea";
            }
            else if (creatureId == "c13003000")
            {
                category = "Sea";
            }
            else if (creatureId == "c13004000")
            {
                category = "Sea";
            }
            else if (creatureId == "c13005000")
            {
                category = "Sea";
            }

            var adjustedAssetName = "";
            var splitResult = artAssetName.Split(' ');
            if (splitResult.Length > 1)
            {
                adjustedAssetName += splitResult[0];
                for (var i = 1; i < splitResult.Length; ++i)
                {
                    var loweredString = splitResult[i].ToLower();
                    adjustedAssetName += loweredString;
                }
            }
            else
            {
                adjustedAssetName = artAssetName;
            }

            creatureArtPath = Path.Combine(creatureArtBasePath, category, $"Baked - {artAssetName}", $"{adjustedAssetName}_LOD3_AnimatedMesh.prefab");

            return creatureArtPath;
        }
    }
}
