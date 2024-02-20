using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

namespace RGSMS.Input
{
    public enum EInputAction
    {
        WasPressed = 0,
        WasReleased,
        IsPressed
    }

    [System.Serializable]
    public class ButtonInputConfig : InputConfig
    {
        [SerializeField]
        private EInputAction _inputAction = EInputAction.WasPressed;
        public EInputAction InputAction => _inputAction;

        [SerializeField]
        private UnityEvent _callback = null;

        public override void FillInputAction(InputAction input)
        {
            base.FillInputAction(input);

            switch(_inputAction)
            {
                case EInputAction.WasPressed:
                    input.started += ProcessAction;
                    break;

                case EInputAction.IsPressed:
                    input.started += WasPressed;
                    input.canceled += WasReleased;
                    break;

                case EInputAction.WasReleased:
                    input.canceled += ProcessAction;
                    break;
            }
        }

        public override void ClearInputAction(InputAction input)
        {
            switch (_inputAction)
            {
                case EInputAction.WasPressed:
                    input.started -= ProcessAction;
                    break;

                case EInputAction.IsPressed:
                    input.started -= WasPressed;
                    input.canceled -= WasReleased;
                    break;

                case EInputAction.WasReleased:
                    input.canceled -= ProcessAction;
                    break;
            }
        }

        private void WasPressed(InputAction.CallbackContext context)
        {
            if (_input.WasPressedThisFrame())
            {
                _inputManager.AddNewInputToHoldMap(IsPressedAction);
            }
        }

        private void WasReleased(InputAction.CallbackContext context)
        {
            _inputManager.RemoveInputFromHoldMap(IsPressedAction);
        }

        public override void ProcessAction(InputAction.CallbackContext context) => _callback.Invoke();
        public override void IsPressedAction()
        {
            if(!_input.WasPressedThisFrame())
            {
                _callback.Invoke();
            }
        }
    }
}
