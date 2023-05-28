// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Core/Input/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Core
{
    public class @Input : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Input()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""cc4445bd-9fbd-498e-b02b-72f5d01c153e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8dd24720-2e01-4966-acc4-05f07ee858f7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""5c656236-acb1-43ac-bd42-83dff289eda2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""d592fef6-aba7-449f-8fc4-652fd3a56f04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""43d8de7d-0ca0-4a48-a381-cf5874828d97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""E"",
                    ""type"": ""Button"",
                    ""id"": ""6f78a27d-6c83-494c-92e8-fefdcc0c0840"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F"",
                    ""type"": ""Button"",
                    ""id"": ""39f34401-b6ce-4dd4-af27-18974301b493"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LMB"",
                    ""type"": ""Button"",
                    ""id"": ""db7a88e7-1127-4d5e-9c3d-ddfe459c1ba7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RMB"",
                    ""type"": ""Button"",
                    ""id"": ""2f3c8d23-69d8-4ee9-926f-9bf54ab8f01d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Direction"",
                    ""id"": ""feb15c36-49c2-488b-ab06-a250ac004693"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""efd7a12d-eab9-49ec-99b3-8a4a98130e42"",
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
                    ""id"": ""8cef8730-29a0-4991-bf49-c4cb090605b1"",
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
                    ""id"": ""ced24300-ba50-4293-89a6-4d176e2a2092"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c17bc79c-d9a5-45eb-a136-16d180360a3d"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""455b776e-dbde-4491-8ff1-8edb3053c179"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c7f251a-e793-4606-bcfe-c2ce71e893b1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c84f984-b936-46da-8620-11bd36383169"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""088ade4c-b3f3-4f21-91d0-bcc371805650"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LMB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a12d4f60-eef0-4fd0-bc98-e8b5b7c90ed0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RMB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
            m_Gameplay_Escape = m_Gameplay.FindAction("Escape", throwIfNotFound: true);
            m_Gameplay_Enter = m_Gameplay.FindAction("Enter", throwIfNotFound: true);
            m_Gameplay_Space = m_Gameplay.FindAction("Space", throwIfNotFound: true);
            m_Gameplay_E = m_Gameplay.FindAction("E", throwIfNotFound: true);
            m_Gameplay_F = m_Gameplay.FindAction("F", throwIfNotFound: true);
            m_Gameplay_LMB = m_Gameplay.FindAction("LMB", throwIfNotFound: true);
            m_Gameplay_RMB = m_Gameplay.FindAction("RMB", throwIfNotFound: true);
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

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Move;
        private readonly InputAction m_Gameplay_Escape;
        private readonly InputAction m_Gameplay_Enter;
        private readonly InputAction m_Gameplay_Space;
        private readonly InputAction m_Gameplay_E;
        private readonly InputAction m_Gameplay_F;
        private readonly InputAction m_Gameplay_LMB;
        private readonly InputAction m_Gameplay_RMB;
        public struct GameplayActions
        {
            private @Input m_Wrapper;
            public GameplayActions(@Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Gameplay_Move;
            public InputAction @Escape => m_Wrapper.m_Gameplay_Escape;
            public InputAction @Enter => m_Wrapper.m_Gameplay_Enter;
            public InputAction @Space => m_Wrapper.m_Gameplay_Space;
            public InputAction @E => m_Wrapper.m_Gameplay_E;
            public InputAction @F => m_Wrapper.m_Gameplay_F;
            public InputAction @LMB => m_Wrapper.m_Gameplay_LMB;
            public InputAction @RMB => m_Wrapper.m_Gameplay_RMB;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                    @Escape.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Enter.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnter;
                    @Enter.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnter;
                    @Enter.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnter;
                    @Space.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpace;
                    @Space.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpace;
                    @Space.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpace;
                    @E.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnE;
                    @E.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnE;
                    @E.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnE;
                    @F.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnF;
                    @F.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnF;
                    @F.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnF;
                    @LMB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLMB;
                    @LMB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLMB;
                    @LMB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLMB;
                    @RMB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRMB;
                    @RMB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRMB;
                    @RMB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRMB;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                    @Enter.started += instance.OnEnter;
                    @Enter.performed += instance.OnEnter;
                    @Enter.canceled += instance.OnEnter;
                    @Space.started += instance.OnSpace;
                    @Space.performed += instance.OnSpace;
                    @Space.canceled += instance.OnSpace;
                    @E.started += instance.OnE;
                    @E.performed += instance.OnE;
                    @E.canceled += instance.OnE;
                    @F.started += instance.OnF;
                    @F.performed += instance.OnF;
                    @F.canceled += instance.OnF;
                    @LMB.started += instance.OnLMB;
                    @LMB.performed += instance.OnLMB;
                    @LMB.canceled += instance.OnLMB;
                    @RMB.started += instance.OnRMB;
                    @RMB.performed += instance.OnRMB;
                    @RMB.canceled += instance.OnRMB;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
            void OnEnter(InputAction.CallbackContext context);
            void OnSpace(InputAction.CallbackContext context);
            void OnE(InputAction.CallbackContext context);
            void OnF(InputAction.CallbackContext context);
            void OnLMB(InputAction.CallbackContext context);
            void OnRMB(InputAction.CallbackContext context);
        }
    }
}
