using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs
{
    public struct RotatorJob : IJobParallelForTransform
    {
        public float rotateSpeed;
        public float deltaTime;
    
        public void Execute(int index, TransformAccess transform)
        {            
            Vector3 newRotation = new Vector3(transform.rotation.x, 
                transform.rotation.y + (this.rotateSpeed  * this.deltaTime),
                transform.rotation.z);
            transform.localRotation = Quaternion.Euler(newRotation);

        }
    }
}