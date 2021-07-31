namespace MGPC.Tool.Extension.HThN6Y.Design.Projectile.EditorPart
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEngine;

    public partial class AssetCreation
    {
        private static IEnumerable<Generated.Projectile> GetProjectiles()
        {
            var baseAssetPath = Path.Combine("_", "1 - Game", "Level - Fishing", "Preprocessed Assets");
            var designAssetPath = Path.Combine(baseAssetPath, "design");
            var creatureAssetPath = Path.Combine(designAssetPath, "projectiles");

            var sourceDirectory = Path.Combine(Application.dataPath, creatureAssetPath);

            var jsonFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");
            var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt");

            var dataFiles = jsonFiles.Concat(txtFiles);

            var creatures = dataFiles
                .Select(dataFile =>
                {
                    var jsonText = File.ReadAllText(dataFile);

                    var creature = Generated.Projectile.FromJson(jsonText);

                    return creature;
                });

            return creatures;
        }
    }
}
