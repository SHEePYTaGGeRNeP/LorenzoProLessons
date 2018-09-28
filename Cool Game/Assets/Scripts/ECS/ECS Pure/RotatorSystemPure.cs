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
        struct RotateJob : IJobProcessComponentData<RotationPure, RotatorPure>
        {
            public float deltaTime;

            public void Execute(ref RotationPure rotation, [ReadOnly] ref RotatorPure rotator)
            {
                rotation.y += rotator.speed * this.deltaTime;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            RotateJob rotateJob = new RotateJob()
            {
                deltaTime = Time.deltaTime
            };
            JobHandle rotateHandle = rotateJob.Schedule(this, 4, inputDeps);

            return rotateHandle;

        }
    }
}