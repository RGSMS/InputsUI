using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

namespace RGSMS
{
    public enum EInputAction
    {
        Pressed = 0,
        Released,
        Hold
    }

    [System.Serializable]
    public class ButtonInputConfig : InputConfig
    {
        [SerializeField]
        private EInputAction _inputAction = EInputAction.Pressed;
        public EInputAction InputAction => _inputAction;

        [SerializeField]
        private UnityEvent _callback = null;

        public override void FillInputAction(InputAction input)
        {
            switch(_inputAction)
            {
                case EInputAction.Pressed:
                    input.started += ProcessAction;
                    break;

                case EInputAction.Released:
                    input.canceled += ProcessAction;
                    break;
            }
        }

        public override void ClearInputAction(InputAction input)
        {
            switch (_inputAction)
            {
                case EInputAction.Pressed:
                    input.started -= ProcessAction;
                    break;

                case EInputAction.Released:
                    input.canceled -= ProcessAction;
                    break;
            }
        }

        public override void ProcessAction(InputAction.CallbackContext context) => _callback.Invoke();
    }
}
