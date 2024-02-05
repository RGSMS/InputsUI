using UnityEngine;

namespace RGSMS.UI
{
    [DisallowMultipleComponent]
    public abstract class BaseUIView : MonoBehaviour
    {
        protected UIManager _uiManager = null;

        public bool IsOn => gameObject.activeSelf;
        private bool _initialized = false;

        public virtual void Initialize()
        {
            _uiManager =  UIManager.Instance;
            _initialized = true;
        }

        protected void SetCurrentActivation(bool enable) => gameObject.SetActive(enable);
        public virtual void Close () => SetCurrentActivation(false);
        public abstract void Destroy();

        public virtual void Open()
        {
            if (!_initialized)
            {
                Initialize();
            }

            SetCurrentActivation(true);
        }
    }
}
