namespace MGPC.Game.Extension.CharacterControl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
    using Pathfinding;
    using Unity.VisualScripting;
    using UnityEngine;

    public class CharacterAIBehaviourBridge : Lightbug.CharacterControllerPro.Implementation.CharacterAIBehaviour
    {
        //
        public GameObject ownedGO;
        public List<GameObject> extendedBehaviorGOs;

        //
        // public float moveSpeed = 1.0f;

        //
        private readonly List<BehaviorDesigner.Runtime.BehaviorTree> _behaviorTrees =
            new List<BehaviorDesigner.Runtime.BehaviorTree>();

        private readonly List<FlowMachine> _flowMachines = new List<FlowMachine>();
        private readonly List<StateMachine> _stateMachines = new List<StateMachine>();

        private readonly List<Vector3> _foundPathPoints = new List<Vector3>();

        private readonly List<GameObject> _behaviorTreeOwnedGOs = new List<GameObject>();
        private readonly List<GameObject> _boltMachineOwnedGOs = new List<GameObject>();

        //
        public bool hasPath;
        private Seeker _seeker;
        public bool _canMove;

        private float _nextPathRequestTimer;
        private float _accPathRequestTimer;

        public Vector3 _targetPosition;

        public override void EnterBehaviour(float dt)
        {
            Debug.Log($"CharacterAIBehaviourBridge - EnterBehaviour");
            _behaviorTrees.ForEach(x =>
            {
                // Debug.Log($"name: {x.BehaviorName}");
                x.EnableBehavior();
                x.Start();
            });
            //
            var variablesComp = ownedGO.GetComponent<Variables>();
            // var gameController = variablesComp.declarations.Get("gameController");
            // var gameController = GameObject.FindGameObjectsWithTag("GameController");
            hasPath = variablesComp.declarations.Get<bool>("hasPath");

            //
            _canMove = true;


        }

        public override void ExitBehaviour(float dt)
        {
            Debug.Log($"CharacterAIBehaviourBridge - ExitBehaviour");
            _behaviorTrees.ForEach(x =>
            {
                x.DisableBehavior();
            });
            _canMove = false;
        }

        public override void UpdateBehaviour(float dt)
        {
            // throw new System.NotImplementedException();
            if (hasPath && _canMove)
            {
                _accPathRequestTimer += dt;
                if (_accPathRequestTimer >= _nextPathRequestTimer)
                {
                    //
                    RequestToFindPath(ownedGO.transform.position, _targetPosition);


                    _accPathRequestTimer = 0;
                    _nextPathRequestTimer = UnityEngine.Random.Range(0.15f, 0.25f);
                    // _nextPathRequestTimer = UnityEngine.Random.Range(5.15f, 10.25f);
                }
            }
        }

        // private void Start()
        protected override void Awake()
        {
            base.Awake();
            _seeker = GetComponent<Seeker>();
            if (_seeker != null)
            {
                // _seeker.
            }

            var behaviorTreeOwnedMap = new Dictionary<GameObject, int>();
            var boltMachineOwnedMap = new Dictionary<GameObject, int>();
            extendedBehaviorGOs.ForEach(ebgo =>
            {
                var behaviorTreeComponents = ebgo.GetComponents<BehaviorDesigner.Runtime.BehaviorTree>();
                var flowMachineComponents = ebgo.GetComponents<FlowMachine>();
                var stateMachineComponents = ebgo.GetComponents<StateMachine>();

                if (behaviorTreeComponents.Any())
                {
                    Debug.Log($"has {behaviorTreeComponents.Length} behavior tree comps");
                    _behaviorTrees.AddRange(behaviorTreeComponents);

                    _behaviorTrees.ForEach(x =>
                    {
                        var contained = behaviorTreeOwnedMap.ContainsKey(x.gameObject);
                        if (!contained)
                        {
                            behaviorTreeOwnedMap.Add(x.gameObject, 1);
                        }
                    });
                }

                if (flowMachineComponents.Any())
                {
                    _flowMachines.AddRange(flowMachineComponents);

                    _flowMachines.ForEach(x =>
                    {
                        var contained = boltMachineOwnedMap.ContainsKey(x.gameObject);
                        if (!contained)
                        {
                            boltMachineOwnedMap.Add(x.gameObject, 1);
                        }
                    });
                }

                if (stateMachineComponents.Any())
                {
                    _stateMachines.AddRange(_stateMachines);

                    _stateMachines.ForEach(x =>
                    {
                        var contained = boltMachineOwnedMap.ContainsKey(x.gameObject);
                        if (!contained)
                        {
                            boltMachineOwnedMap.Add(x.gameObject, 1);
                        }
                    });
                }
            });

            if (behaviorTreeOwnedMap.Any())
            {
                _behaviorTreeOwnedGOs.AddRange(behaviorTreeOwnedMap.Keys.ToList());
            }

            if (boltMachineOwnedMap.Any())
            {
                _boltMachineOwnedGOs.AddRange(boltMachineOwnedMap.Keys.ToList());
            }

            _nextPathRequestTimer = UnityEngine.Random.Range(0.15f, 0.25f);
        }

        public void RequestToFindPath(Vector3 start, Vector3 end)
        {
            // Debug.Log($"RequestToFindPath: start: {start} end: {end}");
            if (_seeker == null) return;

            _seeker.StartPath(start, end, path =>
            {
                if (path.error)
                {
                    // Show path error message
                }
                else
                {
                    // Debug.Log("Found path points");

                    _foundPathPoints.Clear();
                    _foundPathPoints.AddRange(path.vectorPath);

                    _boltMachineOwnedGOs.ForEach(bmogo =>
                    {
                        // Debug.Log($"Trigger event for {bmogo}");
                        CustomEvent.Trigger(bmogo, "Searched Path", _foundPathPoints, _canMove);
                    });
                }
            });
        }

        public void SetMoveDirection(Vector3 direction)
        {
            // direction *= moveSpeed;
            SetMovementAction(direction);
        }

        public void SetTargetPosition(Vector3 pos)
        {
            _targetPosition = pos;
        }

        public void SetCanMove(bool inValue)
        {
            _canMove = inValue;
        }

        public void SetHasPath(bool inValue)
        {
            hasPath = inValue;
        }
    }
}
