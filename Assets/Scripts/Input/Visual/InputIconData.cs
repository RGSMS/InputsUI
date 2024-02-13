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
}
