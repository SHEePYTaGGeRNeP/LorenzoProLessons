using UnityEngine;
using UnityEngine.UI;

namespace Unit.MonoBehaviours
{
    public class HealthbarMono : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void OnHealthChanged(int curHp)
        {
            Debug.Log("int listener");
            this._text.text = $"{curHp} hp";
        }

        public void OnHealthChanges(HealthSystem.HealthChangeEventArgs eventArgs)
        {
            Debug.Log("HealthChangeEventArgs listener");
            this._text.text = $"{eventArgs.CurrentHitPoints} hp";
        }
        
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                return;
        }

    }
}