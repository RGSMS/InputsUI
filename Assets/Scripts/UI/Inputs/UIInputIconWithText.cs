using UnityEngine;
using TMPro;

namespace RGSMS.UI
{
    public class UIInputIconWithText : UIInputIcon
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;

        public override void HandleData()
        {
            base.HandleData();

            string text = _inputManager.GetInputLocKey(Data);
            //falta localizar o texto
            _text.SetText(text);
        }
    }
}
