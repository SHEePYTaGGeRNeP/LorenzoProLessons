using UnityEngine;
using UnityEngine.UI;

namespace Unit.MonoBehaviours
{
    public class HealthbarMono : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void OnHealthChangedInt(int curHp)
        {
            Debug.Log("int listener");
            this._text.text = $"{curHp} hp";
        }

        public void OnHealthChanged(HealthSystem.HealthChangeEventArgs eventArgs)
        {
            Debug.Log("HealthChangeEventArgs listener");
            this._text.text = $"{eventArgs.CurrentHitPoints} hp";
        }
        
    }
}