namespace MGPC.Tool.Extension.HThN6Y.Design.Weapon.EditorPart
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        private static IEnumerable<Generated.Weapon> GetWeapons()
        {
            var baseAssetPath = Path.Combine("_", "1 - Game", "Level - Fishing", "Preprocessed Assets");
            var designAssetPath = Path.Combine(baseAssetPath, "design");
            var creatureAssetPath = Path.Combine(designAssetPath, "weapons");

            var sourceDirectory = Path.Combine(Application.dataPath, creatureAssetPath);

            var jsonFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");
            var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt");

            var dataFiles = jsonFiles.Concat(txtFiles);

            var weapons = dataFiles
                .Select(dataFile =>
                {
                    var jsonText = File.ReadAllText(dataFile);

                    var weapon = Generated.Weapon.FromJson(jsonText);

                    return weapon;
                });

            return weapons;
        }

        private static void RecreateDirectory(string directoryPath)
        {
            var relativeDirectoryPath = Path.Combine("Assets", directoryPath);
            var absoluteDirectoryPath = Path.Combine(Application.dataPath, directoryPath);
            var absoluteDirectoryPathExisted = Directory.Exists(absoluteDirectoryPath);

            if (absoluteDirectoryPathExisted)
            {
                FileUtil.DeleteFileOrDirectory(relativeDirectoryPath);
            }

            Directory.CreateDirectory(absoluteDirectoryPath);
        }
    }
}
