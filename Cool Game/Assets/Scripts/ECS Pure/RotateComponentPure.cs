using Unity.Entities;

namespace ECS_Pure
{
    public struct RotatorPure : IComponentData
    {
        public float speed;
    }

    // allow to be added to inspector / gameobjects
    public class RotatorComponentPure : ComponentDataWrapper<RotatorPure>{}
}