using UnityEngine.InputSystem;
using UnityEngine;

namespace RGSMS.Input
{
    [System.Serializable]
    public class Vector2InputConfig : InputConfig
    {
        [SerializeField]
        private Vector2Event _vector2Callback = null;

        private readonly Vector2 _zero = Vector2.zero;

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
                _vector2Callback.Invoke(_input.ReadValue<Vector2>());
            }
        }

        public override void ProcessAction(InputAction.CallbackContext context) => _vector2Callback.Invoke(context.ReadValue<Vector2>());
        public void ResetAction(InputAction.CallbackContext _) => _vector2Callback.Invoke(_zero);
    }
}
