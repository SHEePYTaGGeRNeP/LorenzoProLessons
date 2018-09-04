using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace ECS_Pure
{
    public class RotatorSystemPure : JobComponentSystem
    {
        struct RotateJob : IJobProcessComponentData<TransformAccess, RotatorPure>
        {
            public float deltaTime;

            public void Execute(ref TransformAccess transform, [ReadOnly] ref RotatorPure rotator)
            {
                Vector3 newRotation = new Vector3(transform.rotation.x, 
                    transform.rotation.y + (rotator.speed * this.deltaTime),
                    transform.rotation.z);
                transform.rotation = Quaternion.Euler(newRotation);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            RotateJob rotateJob = new RotateJob()
            {
                deltaTime = Time.deltaTime
            };
            JobHandle rotateHandle = rotateJob.Schedule(this, 1, inputDeps);

            return rotateHandle;

        }
    }
}