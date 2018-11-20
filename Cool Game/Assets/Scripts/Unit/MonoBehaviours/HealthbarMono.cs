using UnityEngine;
using UnityEngine.UI;

namespace Unit.MonoBehaviours
{
    public class HealthbarMono : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Image _image;

        public void OnHealthChanged(HealthSystem.HealthChangeEventArgs eventArgs)
        {
            this._text.text = $"Health: {eventArgs.CurrentHitPoints} hp";
            this._image.fillAmount = (float)eventArgs.CurrentHitPoints / eventArgs.MaxHitPoints;
            this._image.color =
                new Color(Mathf.Lerp(0f, 1f, 1- this._image.fillAmount),
                    Mathf.Lerp(0f, 1f, this._image.fillAmount), 0);
        }
        
    }
}