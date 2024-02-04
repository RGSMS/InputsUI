using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

namespace RGSMS
{
    public enum EInputDevice
    {
        None = 0,

        Keyboard = 1,
        Mouse,

        Generic = 10,
        Stadia,

        SteamDeck = 20,
        SteamController,

        Playstation3 = 30,
        Playstation4,
        Playstation5,

        XBox360 = 40,
        XBoxOne,
        XBoxSeries,

        Switch = 50,
        //Joy-Cons are only supported on Switch
        SwitchJoycons,
        SwitchJoyconsRight,
        SwitchJoyconsLeft,

        Playdate = 60
    }

    public class RoloInputSystem
    {
        private readonly Inputs _inputs = null;

        private readonly Dictionary<string, EInputDevice> _deviceNamesMap = new()
        {
            { "Keyboard", EInputDevice.Keyboard }, //Correct
            { "Mouse", EInputDevice.Mouse }, //Correct

            { "Generic", EInputDevice.Generic },

            { "SteamDeck", EInputDevice.SteamDeck },
            { "SteamController", EInputDevice.SteamController },

            { "Playstation4", EInputDevice.Playstation4 },
            { "Playstation5", EInputDevice.Playstation5 },

            { "XBox360", EInputDevice.XBox360 },
            { "Xbox Controller", EInputDevice.XBoxOne }, //Correct
            { "XBoxSeries", EInputDevice.XBoxSeries },

            { "Switch", EInputDevice.Switch },
            { "SwitchJoycon", EInputDevice.SwitchJoycons },

            { "Playdate", EInputDevice.Playdate },
        };

        public RoloInputSystem()
        {
            _inputs = new Inputs();

            Debug.Log(Gamepad.all.Count);

            if (Mouse.current != null)
            {
                Debug.Log(Mouse.current.path);
                Debug.Log(Mouse.current.name);
                Debug.Log(Mouse.current.displayName);
                Debug.Log(Mouse.current.shortDisplayName);
            }

            if (Keyboard.current != null)
            {
                Debug.Log(Keyboard.current.path);
                Debug.Log(Keyboard.current.name);
                Debug.Log(Keyboard.current.displayName);
                Debug.Log(Keyboard.current.shortDisplayName);
            }

            if (Gamepad.current != null)
            {
                Debug.Log("------------------------------");

                Debug.Log(Gamepad.current.layout);
                Debug.Log(Gamepad.current.path);
                Debug.Log(Gamepad.current.name);
                Debug.Log(Gamepad.current.displayName);
                Debug.Log(Gamepad.current.shortDisplayName);
            }
        }
    }
}
