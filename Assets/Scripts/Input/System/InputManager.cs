using UnityEngine.InputSystem.Utilities;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace RGSMS.Input
{
    #region Enums

    public enum EDevice
    {
        None = -1,

        Keyboard,

        EightBitDo,

        SwitchJoyCons,
        SwitchProController,

        Xbox360,
        XboxOne,
        XboxSeries,

        PlayStation2,
        PlayStation3,
        PlayStation4,
        PlayStation5,
    }

    public enum EDeviceType
    {
        None = -1,

        Mouse,
        Keyboard,
        Gamepad
    }

    public enum EInput
    {
        None = -1,

        Cross = 0,
        Circle,
        Square,
        Triangle,

        Start,
        Select,

        LeftShoulder,
        RightShoulder,

        LeftTrigger,
        RightTrigger,

        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow,

        DPad,

        LeftStick,
        RightStick,

        LeftStickUp,
        LeftStickDown,
        LeftStickLeft,
        LeftStickRight,

        RightStickUp,
        RightStickDown,
        RightStickLeft,
        RightStickRight,

        LeftStickButton,
        RightStickButton
    }

    public enum EInputsMap
    {
        None = -1,
    }

    #endregion

    public sealed class InputManager
    {
        #region Variables

        private readonly Dictionary<EInput, string> _inputsLocMap = new()
        {
            { EInput.Cross,             "input_cross" },
            { EInput.Circle,            "input_circle" },
            { EInput.Square,            "input_square" },
            { EInput.Triangle,          "input_triangle" },

            { EInput.Start,             "input_start" },
            { EInput.Select,            "input_select" },

            { EInput.LeftShoulder,      "input_left_shoulder" },
            { EInput.RightShoulder,     "input_right_shoulder" },

            { EInput.LeftTrigger,       "input_left_trigger"  },
            { EInput.RightTrigger,      "input_right_trigger" },

            { EInput.UpArrow,           "input_up_arrow" },
            { EInput.DownArrow,         "input_down_arrow" },
            { EInput.LeftArrow,         "input_left_arrow" },
            { EInput.RightArrow,        "input_right_arrow" },

            { EInput.DPad,              "input_dpad" },

            { EInput.LeftStick,         "input_left_stick"  },
            { EInput.RightStick,        "input_right_stick" },

            { EInput.LeftStickUp,       "input_left_stick_up" },
            { EInput.LeftStickDown,     "input_left_stick_down" },
            { EInput.LeftStickLeft,     "input_left_stick_left" },
            { EInput.LeftStickRight,    "input_left_stick_right" },

            { EInput.RightStickUp,      "input_right_stick_up" },
            { EInput.RightStickDown,    "input_right_stick_down" },
            { EInput.RightStickLeft,    "input_right_stick_left" },
            { EInput.RightStickRight,   "input_right_stick_right" },

            { EInput.LeftStickButton,   "input_left_stick_button" },
            { EInput.RightStickButton,  "input_right_stick_button" }
        };

        private readonly Dictionary<string, string> _gamepadMap = new()
        {
            { "DualShock3GamepadHID",       "Playstation" },
            { "DualShock4GamepadHID",       "Playstation" },
            { "DualShock4GampadiOS",        "Playstation" },

            { "SwitchProControllerHID",     "Switch" },

            { "XInputControllerWindows",    "Xbox" },
            { "XboxOneGampadMacOSWireless", "Xbox" },
            { "XboxGamepadMacOS",           "Xbox" },
            { "XboxOneGampadiOS",           "Xbox" },
        };

        private readonly Dictionary<EDevice, string> _deviceMap = new()
        {
            { EDevice.PlayStation4,         "Playstation" },
            { EDevice.Keyboard,             "Keyboard" },
            { EDevice.SwitchProController,  "Switch" },
            { EDevice.XboxOne,              "Xbox" }
        };

        private readonly Dictionary<string, EDeviceType> _deviceBySchemeMap = new()
        {
            { "Keyboard",   EDeviceType.Keyboard },
            { "Gamepad",    EDeviceType.Gamepad },
            { "Mouse",      EDeviceType.Mouse }
        };

        private readonly Dictionary<EInputsMap, List<InputConfig>> _mapInputStorage = null;
        private readonly Dictionary<string, DeviceInputIcons> _inputIconsMap = null;
        private Dictionary<EInputsMap, InputActionMap> _inputActionMaps = null;

        private readonly HashSet<Action> _isPressedCallbacks = null;

        private readonly Stack<InputActionMap> _inputsMaps = null;

        private event Action _deviceDisconnectionEvent = null;
        private event Action _deviceConnectionEvent = null;

        private DeviceInputIcons _deviceInputIcons = null;

        private string _currentControlScheme = null;

#if UNITY_STANDALONE
        private readonly PlayerInput _playerInput = null;
#endif

        private readonly Inputs _inputs = null;

        #endregion

        #region Other Methods

        public InputManager(PlayerInput inputComponent, DeviceInputIcons[] deviceInputsIcons)
        {
            _mapInputStorage = new Dictionary<EInputsMap, List<InputConfig>>();
            _inputIconsMap = new Dictionary<string, DeviceInputIcons>();

            _isPressedCallbacks = new HashSet<Action>();

            _inputsMaps = new Stack<InputActionMap>();

            _inputs = new Inputs();

#if UNITY_STANDALONE
            _playerInput = inputComponent;
            _playerInput.actions = _inputs.asset;

            InputSystem.onActionChange += CheckCurrentDevice;
#endif

            InputSystem.onDeviceChange += OnDeviceConnected;
            AddNewDeviceConnectionEvent(SetCorrectDevice);

            PopulateDevicesIcons(deviceInputsIcons);
            PopulateActionMaps();

            RoloGameManager.Instance.AddUpdateCallback(Update);

            Application.focusChanged += FocusCallback;
        }

        private void FocusCallback(bool onfocus)
        {
            if(_inputsMaps.TryPeek(out InputActionMap input))
            {
                if(onfocus)
                {
                    input.Enable();
                }
                else
                {
                    input.Disable();
                }
            }
        }

        public void Dispose()
        {
            ClearAllInputs();
            _inputs.Dispose();

            Application.focusChanged -= FocusCallback;

            InputSystem.onDeviceChange -= OnDeviceConnected;
#if UNITY_STANDALONE
            InputSystem.onActionChange -= CheckCurrentDevice;
#endif
        }

#endregion

        #region Device Methods

        // Get the sprite icon according to the type of input passed as a parameter
        public Sprite GetInputIcon(EInput input) => _deviceInputIcons.GetSpriteByInput(input);
        public Sprite GetJoystickImage() => _deviceInputIcons.Joystick;

        public string GetInputLocKey(EInput input) => _inputsLocMap[input];

        private void PopulateDevicesIcons(DeviceInputIcons[] deviceInputsIcons)
        {
            for (int i = 0; i < deviceInputsIcons.Length; i++)
            {
                _inputIconsMap.Add(_deviceMap[deviceInputsIcons[i].Console], deviceInputsIcons[i]);
            }
        }

        private void SetCorrectDevice()
        {
            string deviceName = _deviceMap[EDevice.XboxOne];

#if UNITY_SWITCH
            deviceName = _deviceMap[EDevice.Switch];
#elif UNITY_PS4 || UNITY_PS5
            deviceName = _deviceMap[EDevice.Playstation];
#elif UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT
            deviceName = _deviceMap[EDevice.Xbox];
#else
            // Get the device name used by the player
            if (Gamepad.current != null)
            {
                //Checking if the new gamepad is the same. Like having connected through bluetooth and cable at the same time.
                string gamepadName = Gamepad.current.name;
                bool result = char.IsDigit(Gamepad.current.name[^1]);
                if (result)
                {
                    gamepadName = gamepadName.Remove(gamepadName.Length - 1);
                }

                _gamepadMap.TryGetValue(gamepadName, out deviceName);
            }
            else if (Keyboard.current != null)
            {
                deviceName = Keyboard.current.name;
            }
#endif

            _deviceInputIcons = GetDeviceByName(deviceName);
        }

        private DeviceInputIcons GetDeviceByName(string device)
        {
            if (_inputIconsMap.TryGetValue(device, out DeviceInputIcons deviceInputIcons))
            {
                return deviceInputIcons;
            }

            string xbox = _deviceMap[EDevice.XboxOne];
            return _inputIconsMap[xbox];
        }

        #endregion

        #region Device Change

        public void AddNewDeviceDisconnectionEvent(Action newDeviceDisconnectionEvent) => _deviceDisconnectionEvent += newDeviceDisconnectionEvent;
        public void RemoveDeviceDisconnectionEvent(Action deviceDisconnectionEvent) => _deviceDisconnectionEvent -= deviceDisconnectionEvent;

        public void AddNewDeviceConnectionEvent(Action newDeviceConnectionEvent) => _deviceConnectionEvent += newDeviceConnectionEvent;
        public void RemoveDeviceConnectionEvent(Action deviceConnectionEvent) => _deviceConnectionEvent -= deviceConnectionEvent;

        public EDeviceType GetDeviceByControl() => _deviceBySchemeMap[_currentControlScheme];

        //Device change methods
        private void OnDeviceConnected(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Reconnected:
                case InputDeviceChange.Added:
                    _deviceConnectionEvent?.Invoke();
                    break;

                case InputDeviceChange.Disconnected:
                case InputDeviceChange.Removed:
                    _deviceDisconnectionEvent?.Invoke();
                    break;
            }
        }

#if UNITY_STANDALONE

        private void CheckCurrentDevice(object obj, InputActionChange change)
        {
            if (_currentControlScheme == _playerInput.currentControlScheme)
            {
                return;
            }

            _currentControlScheme = _playerInput.currentControlScheme;
        }

#endif

        #endregion

        #region Action Methods

        private void PopulateActionMaps()
        {
            _inputActionMaps = new()
            {
                { EInputsMap.None, _inputs.Joystick }
            };
        }

        public void SetNewActionMap(EInputsMap eActionMap)
        {
            if (_inputsMaps.Count > 0)
            {
                _inputsMaps.Peek().Disable();
            }

            _inputsMaps.Push(_inputActionMaps[eActionMap]);
            _inputsMaps.Peek().Enable();
        }

        public void BackToPreviousActionMap()
        {
            if (_inputsMaps.Count > 0)
            {
                InputActionMap previous = _inputsMaps.Pop();
                previous.Disable();
            }

            //If there's one remaining, activate it
            if (_inputsMaps.Count > 0)
            {
                _inputsMaps.Peek().Enable();
            }
        }

        public void ClearAllInputs()
        {
            InputActionMap input;
            while (_inputsMaps.Count > 0)
            {
                input = _inputsMaps.Pop();
                input.Disable();
                input.Dispose();
            }
        }

        private void Update()
        {
            foreach(Action callback in _isPressedCallbacks)
            {
                callback.Invoke();
            }
        }

        public void SetCallbacks(EInputsMap map, List<InputConfig> actionDefiners)
        {
            if (!_mapInputStorage.ContainsKey(map))
            {
                _mapInputStorage.Add(map, new List<InputConfig>());
            }

            SetInputConfigs(map, _inputActionMaps[map], actionDefiners);
        }

        private void SetInputConfigs(EInputsMap map, InputActionMap actionMap, List<InputConfig> actionConfigs)
        {
            InputAction input;
            foreach (InputConfig config in actionConfigs)
            {
                input = actionMap[config.InputName];

                config.SetInputManager(this);
                config.FillInputAction(input);

                _mapInputStorage[map].Add(config);
            }
        }

        public void ClearCallbacks(EInputsMap map) => ClearMap(map, _inputActionMaps[map]);

        public void ClearCallbacks(EInputsMap map, List<InputConfig> actionDefiners)
        {
            InputActionMap actionMap = _inputActionMaps[map];
            InputAction input;

            foreach (InputConfig definer in actionDefiners)
            {
                input = actionMap[definer.InputName];
                definer.ClearInputAction(input);

                _mapInputStorage[map].Remove(definer);
            }
        }

        private void ClearMap(EInputsMap map, InputActionMap inputActionMap)
        {
            if (_mapInputStorage.TryGetValue(map, out List<InputConfig> definers))
            {
                InputConfig definer;
                InputAction input;
                for (int i = 0; i < definers.Count; i++)
                {
                    definer = definers[i];
                    input = inputActionMap[definer.InputName];

                    definer.ClearInputAction(input);
                }
                _mapInputStorage[map].Clear();
            }
        }

        public void AddNewInputToHoldMap(Action callback)
        {
            _isPressedCallbacks.Add(callback);
        }

        public void RemoveInputFromHoldMap(Action callback)
        {
            _isPressedCallbacks.Remove(callback);
        }

#endregion

        #region Any Button

        public void AddCallOnceAction(Action callback) => InputSystem.onAnyButtonPress.CallOnce(ctrl => callback.Invoke());

        #endregion

        #region Rumble

        public void SetRumbleSpeed(float left, float right) => Gamepad.current?.SetMotorSpeeds(left, right);
        public void StopRumble() => Gamepad.current?.SetMotorSpeeds(0.0f, 0.0f);

        #endregion
    }
}
