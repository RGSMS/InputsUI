using UnityEngine;

namespace RGSMS.UI
{
    public enum EMoment : int
    {
        Awake = 0,
        Start,
        Enable
    }

    [DisallowMultipleComponent]
    public abstract class InputIcon : BaseInstantiableView<EInput, InputIcon>
    {
        #region Variables

        [SerializeField]
        private EInput _overrideInput = EInput.None;
        [SerializeField]
        private EMoment _overrideMoment = EMoment.Enable;

        protected InputManager _inputManager = null;

        #endregion

        #region Unity Methods

        private void Awake() => HandleIcon(EMoment.Awake);
        private void Start() => HandleIcon(EMoment.Start);
        private void OnEnable() => HandleIcon(EMoment.Enable);

        #endregion

        #region Override Methods

        public override void Destroy() { }
        protected override void Initialize()
        {
            //falta pegar a isntancia do InputManager pra utilizar sempre que necessario
        }

        #endregion

        #region Other Methods

        private void HandleIcon(EMoment moment)
        {
            if (_overrideInput != EInput.None &&
                _overrideMoment == moment)
            {
                SetData(_overrideInput);
            }
        }

        #endregion
    }
}
