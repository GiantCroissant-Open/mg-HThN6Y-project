namespace MGPC.Game.EditorPart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEditor.Animations;
    using UnityEditor.VersionControl;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public partial class OrganizeScenario
    {
        [MenuItem("Assets/MGPC/Scenario/Organize")]
        // Start is called before the first frame update
        public static void Organize()
        {
            // Pre setup

            //
            CreateScenarios();

            // Post setup
        }

        private static IEnumerable<Generated.Scenario> GetScenarios()
        {
            var baseAssetPath = Path.Combine("_", "1 - Game", "MG - HThN6Y", "Preprocessed Assets");
            var designAssetPath = Path.Combine(baseAssetPath, "design");
            var scenarioAssetPath = Path.Combine(designAssetPath, "scenarios");

            var sourceDirectory = Path.Combine(Application.dataPath, scenarioAssetPath);

            var jsonFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");
            var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt");

            var dataFiles = jsonFiles.Concat(txtFiles);

            var scenarios = dataFiles
                .Select(dataFile =>
                {
                    var jsonText = File.ReadAllText(dataFile);

                    var newsStub = Generated.Scenario.FromJson(jsonText);

                    return newsStub;
                });

            return scenarios;
        }

        private static IEnumerable<GameObject> CreateScenarios()
        {
            var gameAssetPath = Path.Combine("_", "1 - Game");
            var scenarioBasePath = Path.Combine("Assets", gameAssetPath, $"_Generated - Scenarios");
            var absoluteScenarioBasePath = Path.Combine(Application.dataPath, gameAssetPath, $"_Generated - Scenarios");
            var absoluteScenarioBasePathExisted = Directory.Exists(absoluteScenarioBasePath);

            if (absoluteScenarioBasePathExisted)
            {
                FileUtil.DeleteFileOrDirectory(scenarioBasePath);
            }

            Directory.CreateDirectory(absoluteScenarioBasePath);

            //
            var baseAssetPath = Path.Combine("_", "1 - Game", "MG - HThN6Y");


            var dataAssetPath = Path.Combine("Assets", baseAssetPath, "Data Assets");

            var flowMachineAssetPath = Path.Combine(dataAssetPath, "ScG - Scenario.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(flowMachineAssetPath);

            var scenarios = GetScenarios();
            var scenarioGOs = new List<GameObject>();
            scenarios.ToList().ForEach(scenario =>
            {
                var specificScenarioBasePath = Path.Combine(scenarioBasePath, $"{scenario.Title}");
                var absoluteSpecificScenarioBasePath = Path.Combine(absoluteScenarioBasePath, $"{scenario.Title}");

                Directory.CreateDirectory(absoluteSpecificScenarioBasePath);


                // Create scenario
                var scenarioGO = new GameObject(scenario.Title);
                var variablesComp = scenarioGO.AddComponent<Variables>();
                var fmComp = scenarioGO.AddComponent<FlowMachine>();

                scenarioGOs.Add(scenarioGO);

                variablesComp.declarations.Set("title", scenario.Title);

                variablesComp.declarations.Set("currentWaveIndex", 0);


                // var fmType = typeof(FlowMachine);
                // var graphProperty = fmType.GetProperty("nest");
                // Debug.Log(flowMachineAsset);
                // Debug.Log(flowMachineAsset.graph);
                // Debug.Log(graphProperty);
                // graphProperty.SetValue(fmComp, flowMachineAsset.graph);

                // fmComp.graphData = flowMachineAsset.graph.CreateData();
                fmComp.nest.SwitchToMacro(flowMachineAsset);

                // fmComp.nest.nester = flowMachineAsset.graph;

                // fmComp.GetType().GetProperties().ToList().ForEach(x => Debug.Log(x.Name));
                // fmComp.GetType().GetProperty("nest").SetValue(fmComp, flowMachineAsset.graph);
                // fmComp.GetType().GetProperty("graph").SetValue(fmComp, flowMachineAsset.graph);
                // fmComp.GetType().GetProperty("nest").SetValue(fmComp, flowMachineAsset.graph, null);
                // fmComp.graphData = new FlowGraphData(flowMachineAsset.DefaultGraph());

                // fmComp.nest = flowMachineAsset.graph
                // fmComp.graphData = flowMachineAsset.graph;
                // fmComp.graph = flowMachineAsset.graph;

                //
                var waves = CreateWaves(
                    specificScenarioBasePath,
                    absoluteSpecificScenarioBasePath,
                    scenario, scenarioGO);

                variablesComp.declarations.Set("waves", waves.ToList());
            });

            return scenarioGOs;
        }


        private static IEnumerable<GameObject> CreateWaves(
            string specificScenarioBasePath,
            string absoluteSpecificScenarioBasePath,
            Generated.Scenario scenario,
            GameObject scenarioGO)
        {
            var baseAssetPath = Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y");
            var dataAssetPath = Path.Combine(baseAssetPath, "Data Assets");

            var flowMachineAssetPath = Path.Combine(dataAssetPath, "ScG - Wave.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(flowMachineAssetPath);

            //
            var waveBaseAssetPath = Path.Combine(specificScenarioBasePath, "Waves");
            var absoluteWaveBasePath = Path.Combine(absoluteSpecificScenarioBasePath, "Waves");
            Debug.Log(absoluteWaveBasePath);
            if (!Directory.Exists(absoluteWaveBasePath))
            {
                Directory.CreateDirectory(absoluteWaveBasePath);
            }

            //
            var waveGOs = new List<GameObject>();
            scenario.Waves.ForEach(wave =>
            {
                var waveGO = new GameObject(wave.Title);
                waveGO.transform.SetParent(scenarioGO.transform);


                var pdComp = waveGO.AddComponent<PlayableDirector>();

                // pdComp.bind

                var variablesComp = waveGO.AddComponent<Variables>();
                var fmComp = waveGO.AddComponent<FlowMachine>();

                //
                variablesComp.declarations.Set("owner", scenarioGO);

                //
                fmComp.nest.SwitchToMacro(flowMachineAsset);

                var srtvsComp = waveGO.AddComponent<MGPC.Game.Extension.Common.SignalReceiverToVS>();
                // srtvsComp.eventName = "Scenario Marker";
                srtvsComp.vsGameObjects = new List<GameObject>();
                srtvsComp.vsGameObjects.Add(waveGO);

                //
                variablesComp.declarations.Set("maxWaveCount", 0);

                waveGO.SetActive(false);
                waveGOs.Add(waveGO);

                var playableAssetPath = Path.Combine(waveBaseAssetPath, $"{wave.Title}.playable");
                var timelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();
                Debug.Log(timelineAsset);

                AssetDatabase.CreateAsset(timelineAsset, playableAssetPath);
                AssetDatabase.Refresh();

                var signalTrack = timelineAsset.CreateTrack<SignalTrack>();
                signalTrack.name = $"{wave.Title}";

                foreach (PlayableBinding output in timelineAsset.outputs)
                {
                    if (output.streamName == $"{wave.Title}")
                    {
                        pdComp.SetGenericBinding(output.sourceObject, waveGO);
                    }
                }

                var endTime = wave.ActivatedAt + wave.Duration;
                var scenarioMarker = signalTrack.CreateMarker<MGPC.Game.Extension.Common.TimelineMarker>(endTime);
                scenarioMarker.actionId = 1942;

                // timelineAsset.CreateTrack<SignalTrack>();

                pdComp.playableAsset = timelineAsset;

                var pathGroups = CreatePathGroups(
                    waveBaseAssetPath,
                    absoluteWaveBasePath,
                    wave, scenarioGO, timelineAsset);
                variablesComp.declarations.Set("pathGroups", pathGroups.ToList());

                pathGroups.ToList().ForEach(pathGroup =>
                {
                    foreach (PlayableBinding output in timelineAsset.outputs)
                    {
                        if (output.streamName == $"Signal - {pathGroup.name}")
                        {
                            pdComp.SetGenericBinding(output.sourceObject, waveGO);
                        }
                    }
                });
            });

            return waveGOs;
        }

        private static IEnumerable<GameObject> CreatePathGroups(
            string waveBaseAssetPath,
            string absoluteWaveBasePath,
            Generated.Wave wave,
            GameObject scenarioGO,
            TimelineAsset timelineAsset)
        {
            var baseAssetPath = Path.Combine("Assets", "_", "1 - Game", "MG - HThN6Y");
            var dataAssetPath = Path.Combine(baseAssetPath, "Data Assets");

            var flowMachineAssetPath = Path.Combine(dataAssetPath, "ScG - Path Group.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(flowMachineAssetPath);

            //
            var pathGroupGOs = new List<GameObject>();

            wave.PathGroups.ForEach(pathGroup =>
            {
                var signalTrack = timelineAsset.CreateTrack<SignalTrack>();

                // timelineAsset.CreateMarkerTrack();

                signalTrack.name = $"Signal - {pathGroup.Id}";

                var activatedAt = wave.ActivatedAt + pathGroup.ActivatedAt;
                // timelineAsset.markerTrack.CreateMarker<MGPC.Game.Extension.ScenarioMarker>(activatedAt);
                // var scenarioMarker = timelineAsset.markerTrack.CreateMarker<MGPC.Game.Extension.ScenarioMarker>(activatedAt);
                var scenarioMarker = signalTrack.CreateMarker<MGPC.Game.Extension.Common.TimelineMarker>(activatedAt);
                scenarioMarker.actionId = 2379;

                scenarioMarker.stringParams = new List<string>();
                scenarioMarker.stringParams.Add($"{pathGroup.Id}");

                var pathGroupGO = new GameObject($"{pathGroup.Id}");
                pathGroupGO.transform.SetParent(scenarioGO.transform);

                var pdComp = pathGroupGO.AddComponent<PlayableDirector>();

                var pathGroupPlayableAssetPath = Path.Combine(waveBaseAssetPath, $"{pathGroup.Id}.playable");
                var pathGroupTimelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();

                AssetDatabase.CreateAsset(pathGroupTimelineAsset, pathGroupPlayableAssetPath);
                AssetDatabase.Refresh();

                pdComp.playableAsset = pathGroupTimelineAsset;

                var variablesComp = pathGroupGO.AddComponent<Variables>();

                variablesComp.declarations.Set("currentIndex", 0);
                // Default to the gameobject itself
                variablesComp.declarations.Set("splineFollowerPrefab", pathGroupGO);

                var fmComp = pathGroupGO.AddComponent<FlowMachine>();
                fmComp.nest.SwitchToMacro(flowMachineAsset);

                pathGroupGO.SetActive(false);
                pathGroupGOs.Add(pathGroupGO);

                var paths = CreatePaths(pathGroup, scenarioGO, pathGroupGO);

                variablesComp.declarations.Set("pathList", paths);
            });

            return pathGroupGOs;
        }

        private static IEnumerable<GameObject> CreatePaths(
            Generated.PathGroup pathGroup,
            GameObject scenarioGO,
            GameObject pathGroupGO)
        {
            //
            var pathGOs = new List<GameObject>();

            pathGroup.Paths.ForEach(path =>
            {
                var pathGO = new GameObject($"{path.TemplateId} - {path.Id}");

                pathGO.transform.SetParent(scenarioGO.transform);
                pathGO.transform.localPosition = Vector3.zero;
                pathGO.transform.localRotation = Quaternion.identity;

                pathGOs.Add(pathGO);
            });

            return pathGOs;
        }
    }
}
