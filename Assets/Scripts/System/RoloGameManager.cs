using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using RGSMS.Input;
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

        private event Action _update = null;
        private event Action<bool> _focus = null;

        private void Awake()
        {
            if(Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
            InitializeSystems();

            Application.focusChanged += OnChangeFocus;
        }

        private void Update() => _update?.Invoke();

        private void OnChangeFocus(bool focus) => _focus?.Invoke(focus);

        #region Systems Methods

        private void InitializeSystems()
        {
            _systemsInstance = new Dictionary<Type, object>();

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

        #endregion

        #region Callback Methods

        public void RemoveChangeFocusCallback(Action<bool> callback) => _focus -= callback;
        public void AddChangeFocusCallback(Action<bool> callback) => _focus += callback;

        public void RemoveUpdateCallback(Action callback) => _update -= callback;
        public void AddUpdateCallback(Action callback) => _update += callback;

        #endregion
    }
}
