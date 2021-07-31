namespace MGPC.Game.Extension.CharacterControl
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Utilities;
    using Lightbug.CharacterControllerPro.Implementation;


    public class InputSystemHandler : InputHandler
    {
        private MGPC.Game.Extension.InputControl.Generated.Controls _playerControls;

        private void Awake()
        {
            _playerControls = new MGPC.Game.Extension.InputControl.Generated.Controls();
        }

        private void OnEnable() => _playerControls.Enable();

        private void OnDisable() => _playerControls.Disable();

        public override bool GetBool(string actionName)
        {
            var output = false;
            try
            {
                // output = Input.GetButton(actionName);
            }
            catch ( System.Exception )
            {
                Debug.LogWarning(actionName);
            }

            return output;
        }

        public override float GetFloat(string actionName)
        {
            float output = default( float );
            try
            {
                // output = Input.GetAxis(actionName);
            }
            catch (System.Exception)
            {
                Debug.LogWarning(actionName);
            }

            return output;
        }

        public override Vector2 GetVector2(string actionName)
        {
            // Vector2Action vector2Action;
            // var found = _vector2Actions.TryGetValue( actionName , out vector2Action );
            // if(!found)
            // {
            //     vector2Action = new Vector2Action(
            //         string.Concat(actionName , " X"),
            //         string.Concat(actionName , " Y")
            //     );
            //
            //     _vector2Actions.Add(actionName , vector2Action);
            // }

            var output = Vector2.zero;

            try
            {
                output = _playerControls.Base.Move.ReadValue<Vector2>();
            }
            catch ( System.Exception )
            {
                // PrintInputWarning( vector2Action.x , vector2Action.y );
                // Debug.LogWarning($"{vector2Action.x}, {vector2Action.y}");
                Debug.LogWarning(actionName);
            }

            return output;
        }

        // [SerializeField] InputActionAsset inputActionsAsset = null;
        //
        // [SerializeField] bool filterByActionMap = false;
        //
        // [SerializeField] string gameplayActionMap = "Gameplay";
        //
        // [SerializeField] bool filterByControlScheme = false;
        //
        // [SerializeField] string controlSchemeName = "Keyboard Mouse";
        //
        //
        // Dictionary<string, InputAction> inputActionsDictionary = new Dictionary<string, InputAction>();
        //
        // protected virtual void Awake()
        // {
        //
        //     if (inputActionsAsset == null)
        //     {
        //         Debug.Log("No input actions asset found!");
        //         return;
        //     }
        //
        //     inputActionsAsset.Enable();
        //
        //     if (filterByControlScheme)
        //     {
        //         string bindingGroup = inputActionsAsset.controlSchemes.First(x => x.name == controlSchemeName)
        //             .bindingGroup;
        //         inputActionsAsset.bindingMask = InputBinding.MaskByGroup(bindingGroup);
        //     }
        //
        //     ReadOnlyArray<InputAction> rawInputActions = new ReadOnlyArray<InputAction>();
        //
        //     if (filterByActionMap)
        //     {
        //         rawInputActions = inputActionsAsset.FindActionMap(gameplayActionMap).actions;
        //
        //         for (int i = 0; i < rawInputActions.Count; i++)
        //             inputActionsDictionary.Add(rawInputActions[i].name, rawInputActions[i]);
        //
        //     }
        //     else
        //     {
        //         for (int i = 0; i < inputActionsAsset.actionMaps.Count; i++)
        //         {
        //             InputActionMap actionMap = inputActionsAsset.actionMaps[i];
        //
        //             for (int j = 0; j < actionMap.actions.Count; j++)
        //             {
        //                 InputAction action = actionMap.actions[j];
        //                 inputActionsDictionary.Add(action.name, action);
        //             }
        //
        //         }
        //
        //
        //     }
        //
        //
        //     for (int i = 0; i < rawInputActions.Count; i++)
        //     {
        //         inputActionsDictionary.Add(rawInputActions[i].name, rawInputActions[i]);
        //     }
        //
        // }
        //
        // public override bool GetBool(string actionName)
        // {
        //     InputAction inputAction;
        //
        //     if (!inputActionsDictionary.TryGetValue(actionName, out inputAction))
        //         return false;
        //
        //     return inputActionsDictionary[actionName].ReadValue<float>() >=
        //            InputSystem.settings.defaultButtonPressPoint;
        // }
        //
        // public override float GetFloat(string actionName)
        // {
        //     InputAction inputAction;
        //
        //     if (!inputActionsDictionary.TryGetValue(actionName, out inputAction))
        //         return 0f;
        //
        //     return inputAction.ReadValue<float>();
        // }
        //
        //
        //
        // public override Vector2 GetVector2(string actionName)
        // {
        //     InputAction inputAction;
        //
        //     Debug.Log($"{actionName}");
        //
        //     if (!inputActionsDictionary.TryGetValue(actionName, out inputAction))
        //         return Vector2.zero;
        //
        //
        //     return inputActionsDictionary[actionName].ReadValue<Vector2>();
        // }
    }
}
