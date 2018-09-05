using Unity.Collections;
using UnityEngine;
using Unity.Entities;

namespace ECS_Pure
{
    public class RotatorManagerPure : MonoBehaviour
    {
        private EntityManager _manager;

        [SerializeField]
        private Entity _prefabEntity;

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
            this._manager.Instantiate(this._prefabEntity, entities);


            for (int i = 0; i < amount; i++)
            {
                this._manager.SetComponentData(entities[i], new RotatorPure {speed = this._speed});
            }
            entities.Dispose();
        }
    }
}