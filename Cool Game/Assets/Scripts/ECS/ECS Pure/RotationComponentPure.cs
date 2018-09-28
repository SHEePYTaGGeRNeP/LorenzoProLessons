using System;
using Unity.Entities;

namespace ECS_Pure
{
    [Serializable]
    public struct RotationPure : IComponentData
    {
        public float x;
        public float y;
        public float z;
    }

    // allow to be added to inspector / gameobjects
    //public class RotationComponentPure : ComponentDataWrapper<RotationPure> {}
}