using System.Collections.Generic;
using UnityEngine;

namespace RGSMS.UI
{
    [DisallowMultipleComponent]
    public abstract class BaseInstantiableView<TData, TView> : MonoBehaviour where TView : BaseInstantiableView<TData, TView>
    {
        public TData Data { get; protected set; } = (default);
        public Transform Parent { get; private set; } = null;

        private bool _initialized = false;

        public virtual void Close() => gameObject.SetActive(false);
        public virtual void Open() => gameObject.SetActive(true);

        protected abstract void Initialize();
        public abstract void HandleData();
        public abstract void Destroy();

        public virtual void SetData(TData data)
        {
            Data = data;

            if(!_initialized)
            {
                _initialized = true;
                Initialize();
            }

            HandleData();
        }

        public void InstantiateViews(IList<TData> dataList, List<TView> viewList, bool setActive = true)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                RecycleSingle(dataList[i], viewList, i, setActive);
            }
            DisableUnusedViews(dataList, viewList);
        }

        public TView InstantiateSingle(TData data, List<TView> viewList, bool setActive = true)
        {
            if(Parent == null)
            {
                Parent = transform.parent;
            }

            GameObject viewGameObject = Instantiate(gameObject, Parent);

            TView newChildView = viewGameObject.GetComponent<TView>();
            SetViewActivation(data, newChildView, setActive);
            viewList.Add(newChildView);

            return newChildView;
        }

        private void RecycleSingle(TData data, List<TView> viewList, int dataIndex, bool setActive = true)
        {
            if (dataIndex >= viewList.Count)
            {
                InstantiateSingle(data, viewList, setActive);
            }
            else
            {
                SetViewActivation(data, viewList[dataIndex], setActive);
            }
        }

        public TView RecycleFirstDisabled(TData data, List<TView> viewList, bool setActive = true)
        {
            foreach(TView view in viewList)
            {
                if (!view.gameObject.activeSelf)
                {
                    SetViewActivation(data, view, setActive);
                    return view;
                }
            }

            return InstantiateSingle(data, viewList, setActive);
        }

        public TView EnableFirstDisabled (List<TView> viewList)
        {
            foreach (TView view in viewList)
            {
                if (!view.gameObject.activeSelf)
                {
                    view.Open();
                    return view;
                }
            }

            return null;
        }

        public void DisableUnusedViews(IList<TData> dataList, List<TView> viewList)
        {
            if (dataList.Count < viewList.Count)
            {
                for (int i = dataList.Count; i < viewList.Count; i++)
                {
                    viewList[i].Close();
                }
            }
        }

        public int GetEnabledViewsAmount(List<TView> views)
        {
            int amount = 0;
            foreach (TView view in views)
            {
                if (view.gameObject.activeSelf)
                {
                    amount++;
                }
            }

            return amount;
        }

        public bool HaveEnabledViews(List<TView> views)
        {
            foreach (TView view in views)
            {
                if (view.gameObject.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }

        public void DisableViews(IList<TView> viewList)
        {
            foreach(TView view in viewList)
            {
                view.Close();
            }
        }

        public void DisableView(TView viewList) => viewList.Close();

        private void SetViewActivation(TData data, TView view, bool setActive)
        {
            view.SetData(data);

            if (setActive && !view.gameObject.activeSelf)
            {
                view.Open();
            }
            else if (!setActive && view.gameObject.activeSelf)
            {
                view.Close();
            }
        }
    }
}
