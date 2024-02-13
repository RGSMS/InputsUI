using UnityEngine.InputSystem.Utilities;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace RGSMS
{
    #region Enums

    public enum EDevice
    {
        None = -1,

        Keyboard,
        PlayStation,
        Xbox,
        Switch
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
        RightTrigger,
        LeftTrigger,

        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow,

        DPad,

        LeftStick,
        RightStick,

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
            { EDevice.PlayStation,  "Playstation" },
            { EDevice.Keyboard,     "Keyboard" },
            { EDevice.Switch,       "Switch" },
            { EDevice.Xbox,         "Xbox" }
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

        private readonly Stack<InputActionMap> _inputsMaps = null;

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

            _inputsMaps = new Stack<InputActionMap>();

            _inputs = new Inputs();

#if UNITY_STANDALONE
            _playerInput = inputComponent;
            _playerInput.actions = _inputs.asset;

            InputSystem.onActionChange += CheckCurrentDevice;
#endif

            InputSystem.onDeviceChange += OnDeviceConnected;
            AddNewDeviceChangeEvent(SetCorrectDevice);

            PopulateDevicesIcons(deviceInputsIcons);
            PopulateActionMaps();

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

        #region Inputs Icons

        // Get the sprite icon according to the type of input passed as a parameter
        public Sprite GetInputIcon(EInput input) => _deviceInputIcons.GetSpriteByInput(input);
        public Sprite GetJoystickImage() => _deviceInputIcons.Joystick;

        private void PopulateDevicesIcons(DeviceInputIcons[] deviceInputsIcons)
        {
            for (int i = 0; i < deviceInputsIcons.Length; i++)
            {
                _inputIconsMap.Add(_deviceMap[deviceInputsIcons[i].Console], deviceInputsIcons[i]);
            }
        }

        private void SetCorrectDevice()
        {
            string deviceName = _deviceMap[EDevice.Xbox];

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

            string xbox = _deviceMap[EDevice.Xbox];
            return _inputIconsMap[xbox];
        }

        #endregion

        #region Device Change

        public void AddNewDeviceChangeEvent(Action newDeviceConnectionEvent) => _deviceConnectionEvent += newDeviceConnectionEvent;
        public void RemoveDeviceChangeEvent(Action deviceConnectionEvent) => _deviceConnectionEvent -= deviceConnectionEvent;

        public EDeviceType GetDeviceByControl() => _deviceBySchemeMap[_currentControlScheme];

        //Device change methods
        private void OnDeviceConnected(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                case InputDeviceChange.Disconnected:
                    _deviceConnectionEvent?.Invoke();
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

        #region Action Maps

        private void PopulateActionMaps()
        {
            _inputActionMaps = new();

            //populate with all maps
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

        public void SetCallbacks(EInputsMap map, List<InputConfig> actionDefiners)
        {
            if (!_mapInputStorage.ContainsKey(map))
            {
                _mapInputStorage.Add(map, new List<InputConfig>());
            }

            SetInputDefiners(map, _inputActionMaps[map], actionDefiners);
        }

        private void SetInputDefiners(EInputsMap map, InputActionMap actionMap, List<InputConfig> actionDefiners)
        {
            InputAction input;
            foreach (InputConfig definer in actionDefiners)
            {
                input = actionMap[definer.InputName];
                definer.FillInputAction(input);

                _mapInputStorage[map].Add(definer);
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
