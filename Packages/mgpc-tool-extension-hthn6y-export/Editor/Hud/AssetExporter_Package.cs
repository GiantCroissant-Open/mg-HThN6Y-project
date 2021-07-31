namespace MGPC.Tool.Extension.HThN6Y.Export.EditorPart
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    using GameResource = MGPC.Game.Resource;

    public partial class AssetExporter
    {
        [MenuItem("Assets/MGPC/Export/Hud Asset Package")]
        public static void ExportPackage()
        {
            var startTimeStamp = DateTime.Now;
            Debug.Log($"AssetExporter - Export - start {startTimeStamp:mm:ss.fff}");

            var parentDirectory = Directory.GetParent(Application.dataPath);
            var packagePath = Path.Combine(parentDirectory.FullName, "Hud Asset.unitypackage");

            var paths = new List<string>
            {
                "Assets/_/1 - Game/MG - HThN6Y/_Generated - Hud"
            };

            AssetDatabase.ExportPackage(paths.ToArray(), packagePath, ExportPackageOptions.Recurse);

            var endTimeStamp = DateTime.Now;
            Debug.Log($"AssetExporter - Export - end {endTimeStamp:mm:ss.fff}");
        }
    }
}
