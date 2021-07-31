namespace MGPC.Tool.Extension.HThN6Y.Player.Creature.EditorPart
{
    using System.Linq;
    using System.Reflection;
    using Lightbug.CharacterControllerPro.Core;
    using Lightbug.CharacterControllerPro.Demo;
    using Lightbug.CharacterControllerPro.Implementation;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        [MenuItem("Assets/MGPC/HThN6Y/Player/Create")]
        // Start is called before the first frame update
        public static void Organize()
        {
            var instanceGO = new GameObject("Player");

            instanceGO.AddComponent<CharacterActor>();
            instanceGO.AddComponent<CharacterBody>();

            var graphicsGO = new GameObject("Graphics");
            graphicsGO.transform.SetParent(instanceGO.transform);

            graphicsGO.AddComponent<CharacterGraphicsRootController>();

            var statesGO = new GameObject("States");
            var characterStateControllerComp = statesGO.AddComponent<CharacterStateController>();
            var normalMovementComp = statesGO.AddComponent<NormalMovement>();

            var actionsGO = new GameObject("Actions");
            var characterBrainComp = actionsGO.AddComponent<CharacterBrain>();

            characterBrainComp.SetBrainType(false);

            var inputHandlerSettings = new InputHandlerSettings();
            Debug.Log("aaaaa");
            var humanInputTypeField = inputHandlerSettings.GetType().GetField("humanInputType", BindingFlags.NonPublic | BindingFlags.Instance);
            humanInputTypeField.SetValue(inputHandlerSettings, HumanInputType.Custom);
            // humanInputTypeField(inputHandlerSettings, HumanInputType.Custom);
            Debug.Log(humanInputTypeField);
            // inputHandlerSettings.GetType().GetMembers(BindingFlags.NonPublic).ToList().ForEach(x => Debug.Log(x));
            // inputHandlerSettings.GetType().GetMembers().ToList().ForEach(x => Debug.Log(x));
            // inputHandlerSettings.GetType().GetFields().ToList().ForEach(x => Debug.Log(x));
            // inputHandlerSettings.GetType().GetProperties().ToList().ForEach(x => Debug.Log(x));
            // inputHandlerSettings.GetType().GetMethods().ToList().ForEach(x => Debug.Log(x));
            // // typeof(InputHandlerSettings).GetMembers(BindingFlags.NonPublic).ToList().ForEach(x => Debug.Log(x));
            // // typeof(InputHandlerSettings).GetMembers().ToList().ForEach(x => Debug.Log(x));
            // // typeof(InputHandlerSettings).GetFields().ToList().ForEach(x => Debug.Log(x));
            // // typeof(InputHandlerSettings).GetProperties().ToList().ForEach(x => Debug.Log(x));
            // // typeof(InputHandlerSettings).GetMethods().ToList().ForEach(x => Debug.Log(x));
            // typeof(InputHandlerSettings).GetNestedTypes().ToList().ForEach(x => Debug.Log(x));
            // // Debug.Log($"len: {len}");
            // var fields = typeof(InputHandlerSettings).GetFields();
            // Debug.Log($"count: {fields.Length}");
            // // inputHandlerSettings.GetType().GetFields(BindingFlags.NonPublic).ToList().ForEach(x => Debug.Log(x));
            // typeof(InputHandlerSettings).GetProperties(BindingFlags.NonPublic).ToList().ForEach(x => Debug.Log(x));
            // // inputHandlerSettings.GetType().GetProperties(BindingFlags.NonPublic).ToList().ForEach(x => Debug.Log(x));
            //
            // characterBrainComp.GetType().GetFields().ToList().ForEach(x => Debug.Log(x));
            // characterBrainComp.GetType().GetMembers().ToList().ForEach(x => Debug.Log(x));
            // characterBrainComp.GetType().GetProperties().ToList().ForEach(x => Debug.Log(x));
            // characterBrainComp.GetType().GetMethods().ToList().ForEach(x => Debug.Log(x));
            Debug.Log("bbbbb");
            // typeof(InputHandlerSettings).GetField("humanInputType").SetValue(inputHandlerSettings, HumanInputType.Custom);
            // typeof(CharacterBrain).GetField("inputHandlerSettings")
            //     .SetValue(characterBrainComp, inputHandlerSettings);

            var environmentGO = new GameObject("Environment");
            environmentGO.AddComponent<MaterialController>();

            statesGO.transform.SetParent(instanceGO.transform);
            actionsGO.transform.SetParent(instanceGO.transform);
            environmentGO.transform.SetParent(instanceGO.transform);
        }

        private static GameObject CreateGraphicsGO()
        {
            var instanceGO = new GameObject("Graphics");
            instanceGO.AddComponent<CharacterGraphicsRootController>();

            return instanceGO;
        }
    }
}
