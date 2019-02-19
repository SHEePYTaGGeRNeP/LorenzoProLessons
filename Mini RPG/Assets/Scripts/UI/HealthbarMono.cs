using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Text))]
    public class HealthbarMono : MonoBehaviour
    {
        private Text _text;
        private void Awake()
        {
            this._text = this.GetComponent<Text>();
        }
        public void OnHealthChanged(HealthSystem.HealthChangeEventArgs e)
        {
            this._text.text = $"{e.CurrentHitPoints}/{e.MaxHitPoints} hp";
        }
    }
}
