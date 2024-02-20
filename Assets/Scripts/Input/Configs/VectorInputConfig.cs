using UnityEngine.InputSystem;
using UnityEngine;

namespace RGSMS.Input
{
    [System.Serializable]
    public class VectorInputConfig : InputConfig
    {
        [SerializeField]
        private VectorEvent _vectorCallback = null;

        public override void FillInputAction(InputAction input)
        {
            base.FillInputAction(input);

            input.started += ProcessAction;
            input.started += WasPressed;

            input.canceled += WasReleased;
            input.canceled += ResetAction;
        }

        public override void ClearInputAction(InputAction input)
        {
            input.started -= ProcessAction;
            input.started -= WasPressed;

            input.canceled -= WasReleased;
            input.canceled -= ResetAction;
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

        public override void IsPressedAction()
        {
            if (_input.IsPressed() && !_input.WasPressedThisFrame())
            {
                _vectorCallback.Invoke(_input.ReadValue<float>());
            }
        }

        public override void ProcessAction(InputAction.CallbackContext context) => _vectorCallback.Invoke(context.ReadValue<float>());
        public void ResetAction(InputAction.CallbackContext _) => _vectorCallback.Invoke(0.0f);
    }
}
