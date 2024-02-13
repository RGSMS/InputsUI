using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

namespace RGSMS
{
    [System.Serializable]
    public class VectorEvent : UnityEvent<float> { }

    [System.Serializable]
    public class VectorInputConfig : InputConfig
    {
        [SerializeField]
        private VectorEvent _vectorCallback = null;

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

        public override void ProcessAction(InputAction.CallbackContext context) => _vectorCallback.Invoke(context.ReadValue<float>());
        public void ResetAction(InputAction.CallbackContext _) => _vectorCallback.Invoke(0.0f);
    }
}
