using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs
{
    public struct MagicJob : IJobParallelForTransform
    {
        public float rotateSpeed;
        public float deltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            Vector3 newRotation = new Vector3(transform.rotation.x,
                transform.rotation.y + (this.rotateSpeed * this.deltaTime),
                transform.rotation.z);
            transform.rotation = Quaternion.Euler(newRotation);
            transform.position = transform.position.AddZ( this.deltaTime);
        }
    }
}