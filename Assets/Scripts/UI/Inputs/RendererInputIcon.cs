using UnityEngine;

namespace RGSMS.UI
{
    public class RendererInputIcon : InputIcon
    {
        [SerializeField]
        private SpriteRenderer _renderer = null;

        public override void HandleData()
        {
            Sprite icon = _inputManager.GetInputIcon(Data);
            _renderer.sprite = icon;
        }
    }
}
