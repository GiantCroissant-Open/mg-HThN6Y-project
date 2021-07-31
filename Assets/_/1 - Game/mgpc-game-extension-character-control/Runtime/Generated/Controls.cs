// GENERATED AUTOMATICALLY FROM 'Assets/_/1 - Game/mgpc-game-extension-character-control/Data Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MGPC.Game.Extension.InputControl.Generated
{
    public class @Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Base"",
            ""id"": ""53a6c58e-a35b-4bad-88e3-4d809bb3ac8e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""849df38d-4d4b-4e8b-8455-5b5e22ce9271"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special Fire"",
                    ""type"": ""Button"",
                    ""id"": ""e5a03b85-d876-4f25-a2b6-ea4a2d83b81a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Value"",
                    ""id"": ""02d94c71-89ff-47be-8b92-6fa116fd3b59"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AWSD"",
                    ""id"": ""44b51bbc-6f39-4b33-a518-e737677dd41c"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""173b5f06-ded7-486d-8e61-f5a15919a099"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0fefbf99-0fa5-4972-ac52-9adddeee0a7d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""81feba35-fbc9-4f52-9e03-b3b57ae92ce8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""66495e2f-caf5-414a-a4d3-f503b1403a25"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""391d7595-87a3-4904-9dfb-08db9af3698c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1d0ae15-756b-4ea0-8554-18436269ac48"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""042d3cf2-5737-4f35-8157-7c6bded5668b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eba0750a-e7a9-4f0e-8cc4-86e8b40090a8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Base
            m_Base = asset.FindActionMap("Base", throwIfNotFound: true);
            m_Base_Move = m_Base.FindAction("Move", throwIfNotFound: true);
            m_Base_SpecialFire = m_Base.FindAction("Special Fire", throwIfNotFound: true);
            m_Base_Fire = m_Base.FindAction("Fire", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Base
        private readonly InputActionMap m_Base;
        private IBaseActions m_BaseActionsCallbackInterface;
        private readonly InputAction m_Base_Move;
        private readonly InputAction m_Base_SpecialFire;
        private readonly InputAction m_Base_Fire;
        public struct BaseActions
        {
            private @Controls m_Wrapper;
            public BaseActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Base_Move;
            public InputAction @SpecialFire => m_Wrapper.m_Base_SpecialFire;
            public InputAction @Fire => m_Wrapper.m_Base_Fire;
            public InputActionMap Get() { return m_Wrapper.m_Base; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BaseActions set) { return set.Get(); }
            public void SetCallbacks(IBaseActions instance)
            {
                if (m_Wrapper.m_BaseActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                    @SpecialFire.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnSpecialFire;
                    @SpecialFire.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnSpecialFire;
                    @SpecialFire.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnSpecialFire;
                    @Fire.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnFire;
                    @Fire.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnFire;
                    @Fire.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnFire;
                }
                m_Wrapper.m_BaseActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @SpecialFire.started += instance.OnSpecialFire;
                    @SpecialFire.performed += instance.OnSpecialFire;
                    @SpecialFire.canceled += instance.OnSpecialFire;
                    @Fire.started += instance.OnFire;
                    @Fire.performed += instance.OnFire;
                    @Fire.canceled += instance.OnFire;
                }
            }
        }
        public BaseActions @Base => new BaseActions(this);
        public interface IBaseActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnSpecialFire(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
        }
    }
}
