using UnityEngine;
using RGSMS.Input;

namespace RGSMS
{
    public class TempCode : MonoBehaviour
    {
        [SerializeField]
        private InputsContainer _inputs = null;

        private InputManager _inputManager = null;

        private void Start()
        {
            _inputManager = RoloGameManager.Instance.GetInstance<InputManager>();

            _inputManager.SetCallbacks(EInputsMap.None, _inputs.GetInputs());
            _inputManager.SetNewActionMap(EInputsMap.None);
        }

        public void WasReleased() => Debug.Log("Was Released");
        public void WasPressed() => Debug.Log("Was Pressed");
        public void IsPressed() => Debug.Log("Is Pressed");

        public void WasReleasedBack() => Debug.Log("Was Released");
        public void WasPressedBack() => Debug.Log("Was Pressed");
        public void IsPressedBack() => Debug.Log("Is Pressed");
    }
}
