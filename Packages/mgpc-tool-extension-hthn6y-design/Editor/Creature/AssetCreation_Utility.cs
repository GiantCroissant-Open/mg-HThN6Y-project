namespace MGPC.Tool.Extension.HThN6Y.Design.Creature.EditorPart
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        private static IEnumerable<Generated.Creature> GetCreatures(string sourceDirectory)
        {
            var jsonFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");
            var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt");

            var dataFiles = jsonFiles.Concat(txtFiles);

            var creatures = dataFiles
                .Select(dataFile =>
                {
                    var jsonText = File.ReadAllText(dataFile);

                    var creature = Generated.Creature.FromJson(jsonText);

                    return creature;
                });

            return creatures;
        }
    }
}
