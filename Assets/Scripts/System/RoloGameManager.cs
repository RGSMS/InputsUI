using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using RGSMS.UI;
using System;

namespace RGSMS
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInput))]
    public class RoloGameManager : MonoBehaviour
    {
        public static RoloGameManager Instance { get; private set; } = null;

        private Dictionary<Type, object> _systemsInstance = null;

        [SerializeField]
        private PlayerInput _playerInput = null;

        [SerializeField]
        private DeviceInputIcons[] _devicesIcons = null;

        private void Awake()
        {
            if(Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            AddInstance(new UIManager());
            AddInstance(new InputManager(_playerInput, _devicesIcons));
        }

        public void AddInstance<T>(T newInstance) where T : class => _systemsInstance.Add(typeof(T), newInstance);

        public void RemoveInstance<T>() where T : class
        {
            if (_systemsInstance.ContainsKey(typeof(T)))
            {
                _systemsInstance.Remove(typeof(T));
            }
        }

        public T GetInstance<T>() where T : class
        {
            if (_systemsInstance.TryGetValue(typeof(T), out object reference))
            {
                return (T)reference;
            }

            return null;
        }
    }
}
