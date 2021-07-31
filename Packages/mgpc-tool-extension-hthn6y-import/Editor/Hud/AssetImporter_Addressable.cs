namespace MGPC.Tool.Extension.HThN6Y.Hud.Import.EditorPart
{
    using System.IO;
    using UnityEditor;
    using UnityEditor.AddressableAssets.Settings;
    using UnityEngine;

    using GameResource = MGPC.Game.Resource;

    public partial class AssetImporter
    {
        private const string groupName = "Hud - HThN6Y";

        [MenuItem("Assets/MGPC/Addressable/Remove/Hud")]
        public static void RemoveAddressableGroup()
        {
            var assetSettings = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings;

            // Create group
            GameResource.EditorPart.Utility.AddressableHelper.RemoveGroup(assetSettings, groupName);
        }

        [MenuItem("Assets/MGPC/Addressable/Create/Hud")]
        public static void CreateAddressableGroup()
        {
            var assetSettings = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings;

            // Create group
            var assetGroup = GameResource.EditorPart.Utility.AddressableHelper.CreateGroup(assetSettings, groupName);

            PlaceAssetIntoGroup(
                assetSettings, assetGroup,
                "Hud - HThN6Y",
                "Hud - HThN6Y");
        }

        private static void PlaceAssetIntoGroup(
            AddressableAssetSettings assetSettings,
            AddressableAssetGroup assetGroup,
            string label,
            string addressableName)
        {
            assetSettings.AddLabel(label);

            var generatedBasePath = Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y",
                "_Generated - Hud");
            var dataAssetPath = Path.Combine(generatedBasePath, "Data Assets");

            var hudDataAssetPath = Path.Combine(dataAssetPath, "Hud Data.asset");

            GameResource.EditorPart.Utility.AddressableHelper.PlaceAssetInAddressble(
                assetSettings,
                assetGroup,
                hudDataAssetPath,
                label,
                addressableName);

        }
    }
}
