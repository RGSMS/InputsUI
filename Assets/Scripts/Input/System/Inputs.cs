//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Settings/Inputs/Inputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace RGSMS
{
    public partial class @Inputs: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Inputs()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Joystick"",
            ""id"": ""155f64df-1c4b-47f3-ac45-cd665564cbcc"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""0a8c3c01-6e16-479e-97be-ef2b4c24c607"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""17cdee97-b65b-4109-b95d-b99018bcd300"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""80d5d7db-d72e-4479-80be-aa0707039484"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""ffd09f44-c678-4594-a99c-ad8482736a9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f4eefe46-cd8c-41c6-b6e7-730ae9866e46"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joysticks"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b319d0ee-708b-4196-98a2-d4745a173afd"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a53afc6-4a32-4099-8916-011a4dd79e79"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""123c9ed8-ff75-45c8-9ec2-632579365c07"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Joysticks"",
            ""bindingGroup"": ""Joysticks"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<SwitchProControllerHID>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Joystick
            m_Joystick = asset.FindActionMap("Joystick", throwIfNotFound: true);
            m_Joystick_Up = m_Joystick.FindAction("Up", throwIfNotFound: true);
            m_Joystick_Down = m_Joystick.FindAction("Down", throwIfNotFound: true);
            m_Joystick_Left = m_Joystick.FindAction("Left", throwIfNotFound: true);
            m_Joystick_Right = m_Joystick.FindAction("Right", throwIfNotFound: true);
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Joystick
        private readonly InputActionMap m_Joystick;
        private List<IJoystickActions> m_JoystickActionsCallbackInterfaces = new List<IJoystickActions>();
        private readonly InputAction m_Joystick_Up;
        private readonly InputAction m_Joystick_Down;
        private readonly InputAction m_Joystick_Left;
        private readonly InputAction m_Joystick_Right;
        public struct JoystickActions
        {
            private @Inputs m_Wrapper;
            public JoystickActions(@Inputs wrapper) { m_Wrapper = wrapper; }
            public InputAction @Up => m_Wrapper.m_Joystick_Up;
            public InputAction @Down => m_Wrapper.m_Joystick_Down;
            public InputAction @Left => m_Wrapper.m_Joystick_Left;
            public InputAction @Right => m_Wrapper.m_Joystick_Right;
            public InputActionMap Get() { return m_Wrapper.m_Joystick; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(JoystickActions set) { return set.Get(); }
            public void AddCallbacks(IJoystickActions instance)
            {
                if (instance == null || m_Wrapper.m_JoystickActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_JoystickActionsCallbackInterfaces.Add(instance);
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
            }

            private void UnregisterCallbacks(IJoystickActions instance)
            {
                @Up.started -= instance.OnUp;
                @Up.performed -= instance.OnUp;
                @Up.canceled -= instance.OnUp;
                @Down.started -= instance.OnDown;
                @Down.performed -= instance.OnDown;
                @Down.canceled -= instance.OnDown;
                @Left.started -= instance.OnLeft;
                @Left.performed -= instance.OnLeft;
                @Left.canceled -= instance.OnLeft;
                @Right.started -= instance.OnRight;
                @Right.performed -= instance.OnRight;
                @Right.canceled -= instance.OnRight;
            }

            public void RemoveCallbacks(IJoystickActions instance)
            {
                if (m_Wrapper.m_JoystickActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IJoystickActions instance)
            {
                foreach (var item in m_Wrapper.m_JoystickActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_JoystickActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public JoystickActions @Joystick => new JoystickActions(this);
        private int m_JoysticksSchemeIndex = -1;
        public InputControlScheme JoysticksScheme
        {
            get
            {
                if (m_JoysticksSchemeIndex == -1) m_JoysticksSchemeIndex = asset.FindControlSchemeIndex("Joysticks");
                return asset.controlSchemes[m_JoysticksSchemeIndex];
            }
        }
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_MouseSchemeIndex = -1;
        public InputControlScheme MouseScheme
        {
            get
            {
                if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
                return asset.controlSchemes[m_MouseSchemeIndex];
            }
        }
        public interface IJoystickActions
        {
            void OnUp(InputAction.CallbackContext context);
            void OnDown(InputAction.CallbackContext context);
            void OnLeft(InputAction.CallbackContext context);
            void OnRight(InputAction.CallbackContext context);
        }
    }
}
