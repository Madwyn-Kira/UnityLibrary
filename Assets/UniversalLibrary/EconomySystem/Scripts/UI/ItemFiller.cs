using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Ui
{
    public class ItemFiller : MonoBehaviour
    {
        public Resource resource;

        public TMP_Text text;
        public TMP_Text countText;

        public Image Sprite;

        public void Init(Resource res)
        {
            resource = res;
            text.text = res.Type.ToString();
            Sprite.sprite = res.sprite;
            ChangeCountInUi();

            resource.OnValueChanged += ChangeCountInUi;
        }

        public void ChangeCountInUi()
        {
            countText.text = resource.GetValue().ToString();
        }

        private void OnDestroy()
        {
            resource.OnValueChanged -= ChangeCountInUi;
        }
    }
}