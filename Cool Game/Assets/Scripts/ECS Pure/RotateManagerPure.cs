using Unity.Collections;
using UnityEngine;
using Unity.Entities;

namespace ECS_Pure
{
    public class RotateManagerPure : MonoBehaviour
    {
        private EntityManager _manager;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private float _speed;


        private void Start()
        {
            this._manager = World.Active.GetOrCreateManager<EntityManager>();
            this.Spawn(3);
        }

        private void Update()
        {
            if (Input.GetKeyDown("space"))
                this.Spawn(1);
        }

        private void Spawn(int amount)
        {
            NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
            this._manager.Instantiate(this._prefab, entities);


            for (int i = 0; i < amount; i++)
            {
                float3 pos = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                this._manager.SetComponentData(entities[i], new Position {Value = pos});
                this._manager.SetComponentData(entities[i], new RotatorPure {speed = this._speed});
            }
            entities.Dispose();
        }
    }
}