using UnityEngine.UI;
using UnityEngine;

namespace RGSMS.UI
{
    public class UIInputIcon : InputIcon
    {
        [SerializeField]
        private Image _image = null;

        public override void HandleData()
        {
            Sprite icon = _inputManager.GetInputIcon(Data);
            _image.sprite = icon;
        }
    }
}
