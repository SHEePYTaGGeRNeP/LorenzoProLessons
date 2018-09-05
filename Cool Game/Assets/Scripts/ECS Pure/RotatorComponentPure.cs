using Unity.Entities;
using System;
namespace ECS_Pure
{
    [Serializable]
    public struct RotatorPure : IComponentData
    {
        public float speed;
    }

    // allow to be added to inspector / gameobjects
    //public class RotatorComponentPure : ComponentDataWrapper<RotatorPure>{}
}