using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace ECS_Pure
{
    public class RotationSystemPure : JobComponentSystem
    {
//        struct RotateJob : IJobProcessComponentData<Transform, RotationPure>
//        {
//            public void Execute(ref Transform transform, [ReadOnly] ref RotationPure rotation)
//            {
//                transform.Rotate(rotation.x, rotation.y, rotation.z);
//            }
//        }
//
//        protected override JobHandle OnUpdate(JobHandle inputDeps)
//        {
//            RotateJob rotateJob = new RotateJob();
//            JobHandle rotateHandle = rotateJob.Schedule(this, 1, inputDeps);
//
//            return rotateHandle;
//
//        }
        
    }
}