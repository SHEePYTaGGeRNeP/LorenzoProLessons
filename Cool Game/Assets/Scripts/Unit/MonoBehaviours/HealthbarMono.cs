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
        }
        
    }
}