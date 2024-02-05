namespace RGSMS.UI
{
    public enum EUIType : int
    {
        None = 0,
    }

    [System.Serializable]
    public struct UIState
    {
        public EUIType Type;
        public BaseUIView[] Views;
    }
}
