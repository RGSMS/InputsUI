using UnityEngine.InputSystem;
using UnityEngine;

namespace RGSMS.Input
{
    [System.Serializable]
    public abstract class InputConfig
    {
        [SerializeField]
        private string _inputName = null;
        public string InputName => _inputName;

        [SerializeField]
        private EInput _inputType = EInput.None;
        public EInput InputType => _inputType;

        protected InputManager _inputManager = null;
        protected InputAction _input = null;

        public void SetInputManager(InputManager inputManager) => _inputManager = inputManager;
        public virtual void FillInputAction(InputAction input) => _input = input;
        public abstract void ProcessAction(InputAction.CallbackContext context);
        public abstract void ClearInputAction(InputAction input);
        public abstract void IsPressedAction();
    }
}
