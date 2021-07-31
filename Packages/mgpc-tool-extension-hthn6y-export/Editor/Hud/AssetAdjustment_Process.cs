namespace MGPC.Tool.Extension.HThN6Y.Export.EditorPart
{
    using System;
    using UnityEditor;

    public partial class AssetAdjustment
    {
        [MenuItem("Assets/MGPC/Adjust/Preprocess/Hud Asset")]
        public static void PreProcess()
        {
            var originalPath = System.IO.Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y", "Hud");
            var toPath = System.IO.Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y", "_Generated - Hud");
            try
            {
                AssetDatabase.StartAssetEditing();
                AssetDatabase.MoveAsset(originalPath, toPath);
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        [MenuItem("Assets/MGPC/Adjust/Postprocess/Hud Asset")]
        public static void PostProcess()
        {
            var originalPath = System.IO.Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y", "_Generated - Hud");
            var toPath = System.IO.Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y", "Hud");

            try
            {
                AssetDatabase.StartAssetEditing();
                AssetDatabase.MoveAsset(originalPath, toPath);
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}
