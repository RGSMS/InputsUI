using UnityEngine.InputSystem;
using UnityEngine;

namespace RGSMS
{
    public interface IHold
    {
        void StartHold();
        void EndHold();
        void Hold();
    }

    [System.Serializable]
    public abstract class InputConfig
    {
        [SerializeField]
        private string _inputName = null;
        public string InputName => _inputName;

        [SerializeField]
        private EInput _inputType = EInput.None;
        public EInput InputType => _inputType;

        public abstract void ProcessAction(InputAction.CallbackContext context);
        public abstract void ClearInputAction(InputAction input);
        public abstract void FillInputAction(InputAction input);
    }
}
