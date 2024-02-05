using UnityEngine;

namespace RGSMS.UI
{
    [DisallowMultipleComponent]
    public sealed class SceneUIs : MonoBehaviour
    {
        private UIManager _uiManager = null;

        [SerializeField]
        private UIState[] _uis = null;
        public UIState[] UIs => _uis;

        private void OnDestroy() => _uiManager.DestroyUIs(_uis);
        private void Awake()
        {
            _uiManager = UIManager.Instance;
            _uiManager.AddNewUIs(_uis);
        }
    }
}
