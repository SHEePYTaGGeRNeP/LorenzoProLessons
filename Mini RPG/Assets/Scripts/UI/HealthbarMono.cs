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
    public class HealthbarMono : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Image _color;
        [SerializeField]
        private Image _fill;
        [SerializeField]
        private Gradient _gradientColor;
        
        public void OnHealthChanged(HealthChangeEventArgs e)
        {
            this._text.text = $"{e.CurrentHitPoints}/{e.MaxHitPoints} hp";
            this._fill.fillAmount = (float)e.CurrentHitPoints / e.MaxHitPoints;
            this._color.color = _gradientColor.Evaluate(1 - this._fill.fillAmount);
        }
    }
}
