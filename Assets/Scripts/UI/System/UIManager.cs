using System.Collections.Generic;
using System;

namespace RGSMS.UI
{
    public sealed class UIManager
    {
        private readonly Dictionary<EUIType, UIState> _uisMap = null;
        private readonly Stack<EUIType> _uisStack = null;

        private event Action<EUIType> _onChangeUI = null;

        private static UIManager _instance = null;
        public static UIManager Instance
        {
            get
            {
                _instance ??= new UIManager();
                return _instance;
            }

            private set { }
        }

        public UIManager()
        {
            _uisMap = new Dictionary<EUIType, UIState>();
            _uisStack = new Stack<EUIType>();
        }

        public void RemoveChangeUICallback(Action<EUIType> callback) => _onChangeUI -= callback;
        public void AddChangeUICallback(Action<EUIType> callback) => _onChangeUI += callback;

        public void AddNewUIs (UIState[] uis)
        {
            foreach (UIState uiState in uis)
            {
                _uisMap.Add(uiState.Type, uiState);
            }
        }

        public void DestroyUIs(UIState[] uis)
        {
            CloseUIs();

            foreach(UIState ui in uis)
            {
                _uisMap.Remove(ui.Type);
                foreach (BaseUIView view in ui.Views)
                {
                    view.Destroy();
                }
            }
        }

        public void ChangeUI (EUIType nextUIType)
        {
            if (nextUIType == GetCurrentUIType())
            {
                return;
            }

            EUIType currentStateType = EUIType.None;
            if (_uisStack.Count > 0)
            {
                currentStateType = _uisStack.Peek();
            }

            _uisStack.Push(nextUIType);

            UpdateViews(currentStateType, nextUIType);
        }

        public void BackToPreviousUI ()
        {
            if(_uisStack.Count > 0)
            {
                EUIType currentUI = _uisStack.Pop();
                EUIType uiToReturnTo = EUIType.None;

                if (_uisStack.Count > 0)
                {
                    uiToReturnTo = _uisStack.Peek();
                }

                UpdateViews(currentUI, uiToReturnTo);
            }
        }

        private void UpdateViews(EUIType uiToDisable, EUIType uiToEnable)
        {
            UIState newState = _uisMap[uiToEnable];

            if (uiToDisable != EUIType.None)
            {
                UIState lastState = _uisMap[uiToDisable];

                BaseUIView[] activeViews = lastState.Views;
                foreach(BaseUIView view in activeViews)
                {
                    if (!ViewExistInState(view, newState))
                    {
                        view.Close();
                    }
                }
            }

            _onChangeUI?.Invoke(uiToEnable);

            BaseUIView[] nextActiveViews = newState.Views;
            foreach (BaseUIView view in nextActiveViews)
            {
                if (!view.IsOn)
                {
                    view.Open();
                }
            }
        }
        
        private bool ViewExistInState(BaseUIView view, UIState uiState)
        {
            BaseUIView[] nextStateViews = uiState.Views;
            foreach (BaseUIView nextView in nextStateViews)
            {
                if (view == nextView)
                {
                    return true;
                }
            }

            return false;
        }

        public EUIType GetCurrentUIType ()
        {
            if(_uisStack.Count > 0)
            {
                return _uisStack.Peek();
            }

            return EUIType.None;
        }

        private void CloseUIs ()
        {
            if (_uisStack.Count == 0)
            {
                return;
            }

            UIState currentState = _uisMap[_uisStack.Peek()];
            BaseUIView[] stateViews = currentState.Views;
            foreach(BaseUIView view in stateViews)
            {
                view.Close();
            }

            _uisStack.Clear();
        }
    }
}
