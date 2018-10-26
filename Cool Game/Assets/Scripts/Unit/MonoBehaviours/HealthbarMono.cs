using UnityEngine;
using UnityEngine.UI;

namespace Unit.MonoBehaviours
{
    public class HealthbarMono : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void OnHealthChanged(HealthSystem.HealthChangeEventArgs eventArgs)
        {
            this._text.text = $"Health: {eventArgs.CurrentHitPoints} hp";
        }
        
    }
}