namespace MGPC.Game.EditorPart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Animations;
    using UnityEditor.VersionControl;
    using UnityEngine;

    public partial class OrganizeAsset
    {

        [MenuItem("Assets/MGPC/Organize/Quirky")]
        // Start is called before the first frame update
        public static void Organize()
        {
            var baseAssetPath = Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y", "Preprocessed Assets");
            var quirkyAssetPath = Path.Combine(baseAssetPath, "Quirky Series Vol 3");
            var quirkyArcticAssetPath = Path.Combine(quirkyAssetPath, "Arctic Vol 2");

            var flatKitPath = Path.Combine("Assets", "Standard Assets", "FlatKit");
            var specificShaderPath = Path.Combine(flatKitPath, "Shaders", "StylizedSurface", "StylizedSurface.shader");
            var specificShader = AssetDatabase.LoadAssetAtPath<Shader>(specificShaderPath);

            if (specificShader == null)
            {
                return;
            }

            try
            {
                //
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Beluga", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Cougar", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Hare", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Husky", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Lynx", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Moose", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Narwhal", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Puffin", specificShader);
                OrganizeQuirkyAsset(quirkyArcticAssetPath, "Snow Leopard", specificShader);
            }
            finally
            {
            }
        }

        private static void OrganizeQuirkyAsset(string parentPath, string folderName, Shader shader)
        {
            var specificAssetPath = Path.Combine(parentPath, folderName);

            folderName = folderName.Replace(" ", "");

            var animationAssetPath = Path.Combine(specificAssetPath, "Animations");
            var materialAssetPath = Path.Combine(specificAssetPath, "Materials");
            var modelAssetPath = Path.Combine(specificAssetPath, "Models");
            var prefabAssetPath = Path.Combine(specificAssetPath, "Prefabs");


            // //
            // var specificAnimatorControllerPath = Path.Combine(animationAssetPath, $"AC_{folderName}.controller");
            // var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(specificAnimatorControllerPath);
            //
            // if (animatorController == null)
            // {
            //     Debug.Log($"{folderName} - Can not find animator controller at {specificAnimatorControllerPath}");
            //     return;
            // }

            //
            var specificMaterialAssetPath = Path.Combine(materialAssetPath, $"Mat_{folderName}.mat");
            var material = AssetDatabase.LoadAssetAtPath<Material>(specificMaterialAssetPath);

            // No material found, just skip the function right away
            if (material == null)
            {
                Debug.Log($"{folderName} - Can not find material");
                return;
            }

            var specificTextureAssetPath = Path.Combine(materialAssetPath, $"Tex_{folderName}.png");
            var texture = AssetDatabase.LoadAssetAtPath<Texture>(specificTextureAssetPath);
            if (texture == null)
            {
                Debug.Log($"{folderName} - Can not find texture");
                return;
            }

            material.shader = shader;
            material.SetTexture("_BaseMap", texture);

            //
            // for (var i = 0; i < 4; ++i)
            // {
            //     var lodPrefabPath = Path.Combine(prefabAssetPath, $"{folderName}_LOD{i}.prefab");
            //     var lodPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(lodPrefabPath);
            //
            //     // var lodPrefabInstance = PrefabUtility.LoadPrefabContents(lodPrefabPath);
            //     var lodPrefabInstance = PrefabUtility.InstantiatePrefab(lodPrefab) as GameObject;
            //
            //     // Can not find prefab for this lod, skip to next
            //     if (lodPrefabInstance == null)
            //     {
            //         Debug.Log($"{folderName} - Can not create lod prefab instance {i}");
            //         return;
            //     }
            //
            //     var lodModelPath = Path.Combine(modelAssetPath, $"{folderName}_LOD{i}.fbx");
            //     var allModels = AssetDatabase.LoadAllAssetsAtPath(lodModelPath);
            //     var hasAnyModel = allModels.Any();
            //
            //     // Can not find lod model to assign, skip to next
            //     if (!hasAnyModel)
            //     {
            //         Debug.Log($"{folderName} - Can not find lod model {i}");
            //         return;
            //     }
            //
            //     //
            //     var filteredMeshes =
            //         allModels
            //             .Where(m => m is Mesh && m.ToString().Contains($"{folderName}"))
            //             .ToList();
            //     var hasSpecificMesh = filteredMeshes.Any();
            //
            //     if (!hasSpecificMesh)
            //     {
            //         Debug.Log($"{folderName} - Can not find mesh");
            //         return;
            //     }
            //
            //     var specificMesh = filteredMeshes.First() as Mesh;
            //
            //     // filteredMeshes.ForEach(fm => Debug.Log($"{fm.GetType().ToString()} {fm.name}"));
            //
            //     //
            //     var filteredAvatars =
            //         allModels
            //             .Where(a => a is Avatar && a.ToString().Contains($"{folderName}_LOD{i}Avatar"))
            //             .ToList();
            //     var hasSpecificAvatar = filteredAvatars.Any();
            //
            //     if (!hasSpecificAvatar)
            //     {
            //         Debug.Log($"{folderName} - Can not find avatar");
            //         return;
            //     }
            //
            //     var specificAvatar = filteredAvatars.First() as Avatar;
            //
            //     //
            //
            //     var animator = lodPrefabInstance.GetComponent<Animator>();
            //     if (animator == null)
            //     {
            //         Debug.Log($"{folderName} - Can not find avatar");
            //         return;
            //     }
            //
            //     animator.runtimeAnimatorController = animatorController;
            //     animator.avatar = specificAvatar;
            //
            //     // var newInstance = new GameObject( $"{folderName}");
            //     // var smrComp = newInstance.AddComponent<SkinnedMeshRenderer>();
            //     //
            //     // GameObject toBeRemovedGO = null;
            //     //
            //     foreach (Transform child in lodPrefabInstance.transform)
            //     {
            //         var childSmrComp = child.GetComponent<SkinnedMeshRenderer>();
            //         if (childSmrComp != null)
            //         {
            //             // toBeRemovedGO = child.gameObject;
            //             var hasAnyMaterial = childSmrComp.materials.Any();
            //             if (hasAnyMaterial)
            //             {
            //                 childSmrComp.materials[0] = material;
            //             }
            //
            //             // smrComp.bo = new Bounds(childSmrComp.bounds.center, childSmrComp.bounds.size);
            //             //
            //             // smr.materials[0] = material;
            //             //
            //             // smr.sharedMesh = specificMesh;
            //         }
            //     }
            //
            //     PrefabUtility.ApplyPrefabInstance(lodPrefabInstance, InteractionMode.AutomatedAction);
            //     PrefabUtility.SaveAsPrefabAsset(lodPrefab, lodPrefabPath);
            //
            //     PrefabUtility.UnloadPrefabContents(lodPrefab);
            //
            //     // smrComp.materials = (new List<Material> {material}).ToArray();
            //     //
            //     // newInstance.transform.SetParent(lodPrefab.transform);
            //     //
            //     // if (toBeRemovedGO != null)
            //     // {
            //     //     GameObject.DestroyImmediate(toBeRemovedGO);
            //     // }
            // }
            //
            // // AssetDatabase.LoadAssetAtPath<GameObject>(prefabAssetPath)

        }
    }
}
