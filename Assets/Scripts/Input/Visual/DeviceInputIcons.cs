using UnityEngine;

namespace RGSMS
{
    [System.Serializable]
    public struct InputIconData
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string Name;
#endif

        public EInput Input;
        public Sprite Icon;
    }

    [CreateAssetMenu(fileName = "Device Inputs Icons", menuName = "OTAIMON/Input/Device Icons")]
    public class DeviceInputIcons : ScriptableObject
    {
        [SerializeField]
        private EDevice _console = EDevice.None;
        public EDevice Console => _console;

        [SerializeField]
        private InputIconData[] _inputsIcons = null;

        public Sprite GetSpriteByInput(EInput input)
        {
            foreach(InputIconData inputIcon in _inputsIcons)
            {
                if (inputIcon.Input == input)
                {
                    return inputIcon.Icon;
                }
            }

            return null;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            for(int i = 0; i < _inputsIcons.Length; i++)
            {
                _inputsIcons[i].Name = _inputsIcons[i].Input.ToString();
            }
        }
#endif
    }
}
