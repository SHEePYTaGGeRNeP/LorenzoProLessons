using System;
using Units;
using UnityEngine.Events;

namespace Assets.Scripts
{
    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthChangeEventArgs> { }
}
