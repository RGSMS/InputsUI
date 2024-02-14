using UnityEngine;

namespace RGSMS.Input
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
