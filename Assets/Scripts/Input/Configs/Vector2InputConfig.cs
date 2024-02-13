using UnityEngine.InputSystem;
using UnityEngine;

namespace RGSMS
{
    [System.Serializable]
    public class Vector2InputConfig : InputConfig
    {
        [SerializeField]
        private Vector2Event _vector2Callback = null;

        private readonly Vector2 _zero = Vector2.zero;

        public override void FillInputAction(InputAction input)
        {
            input.performed += ProcessAction;
            input.canceled += ResetAction;
        }

        public override void ClearInputAction(InputAction input)
        {
            input.performed -= ProcessAction;
            input.canceled -= ResetAction;
        }

        public override void ProcessAction(InputAction.CallbackContext context) => _vector2Callback.Invoke(context.ReadValue<Vector2>());
        public void ResetAction(InputAction.CallbackContext _) => _vector2Callback.Invoke(_zero);
    }
}
