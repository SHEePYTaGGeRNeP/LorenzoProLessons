using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine.Events;

namespace Assets.Scripts
{
    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthSystem.HealthChangeEventArgs> { }
}
