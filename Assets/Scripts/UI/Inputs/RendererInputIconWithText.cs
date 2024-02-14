using UnityEngine;
using TMPro;

namespace RGSMS.UI
{
    public class RendererInputIconWithText : RendererInputIcon
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;

        [SerializeField]
        private string _uiSuffix = null;

        public void SetUISuffix(string suffix) => _uiSuffix = suffix;

        public override void HandleData()
        {
            base.HandleData();

            string text = _inputManager.GetInputLocKey(Data);
            if (!string.IsNullOrEmpty(_uiSuffix))
            {
                text = string.Concat(text, _uiSuffix);
            }

            //falta localizar o texto
            _text.SetText(text);
        }
    }
}
