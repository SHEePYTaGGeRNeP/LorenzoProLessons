using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs
{
    public class RotateManager : MonoBehaviour
    {
        private TransformAccessArray _transforms;
        private RotatorJob _rotatorJob;
        private JobHandle _rotatorHandle;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private float _speed;

        private void OnDisable()
        {
            this._rotatorHandle.Complete();
            this._transforms.Dispose();
        }

        private void Start()
        {
            this._transforms = new TransformAccessArray(0, -1);
            this.Spawn(3);            
        }

        private void Update()
        {
            this._rotatorHandle.Complete();

            if (Input.GetKeyDown("space"))
                this.Spawn(1);

            this._rotatorJob = new RotatorJob()
            {
                deltaTime = Time.deltaTime,
                rotateSpeed = this._speed
            };

            this._rotatorHandle = this._rotatorJob.Schedule(this._transforms);

            JobHandle.ScheduleBatchedJobs();

        }

        private void Spawn(int amount)
        {
            this._rotatorHandle.Complete();

            this._transforms.capacity = this._transforms.length + amount;

            for (int i = 0; i < amount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                var obj = Instantiate(this._prefab, pos, Quaternion.identity);

                this._transforms.Add(obj.transform);
            }

        }
    }
}