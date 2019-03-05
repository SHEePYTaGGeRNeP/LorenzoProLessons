using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI.Sensors
{
    [RequireComponent(typeof(Collider))]
    public abstract class TouchSensor3D : Sensor
    {
        [SerializeField]
        protected bool triggerSense = true;

        [SerializeField]
        protected bool collisionSense;

        [SerializeField]
        protected Collider touchCollider;

        [SerializeField]
        private Transform _parentObject;

        [Header("Debug")]
        [SerializeField]
        protected int touchCount;

        [SerializeField]
        private List<Collider> _touchingColliders;

        public bool IsTouching { get { return this.touchCount > 0; } }

        protected override void Awake()
        {
            base.Awake();
            this.touchCount = 0;
            if (this.touchCollider == null)
                this.touchCollider = this.GetComponent<Collider>();
            Assert.IsNotNull(this._parentObject, "Please set parent");
        }

        protected override void UpdateSense()
        {
            this._touchingColliders.RemoveAll(x => x == null);
            this.touchCount = this._touchingColliders.Count;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!this.triggerSense || !this.detectionMask.IsLayerInLayerMask(other.gameObject.layer))
                return;
            this.touchCount++;
            this._touchingColliders.Add(other);
            this.OnTouch(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!this.triggerSense || !this.detectionMask.IsLayerInLayerMask(other.gameObject.layer))
                return;
            this.touchCount--;
            this._touchingColliders.Remove(other);
        }

        protected virtual void OnCollisionEnter(Collision col)
        {
            if (!this.collisionSense || !this.detectionMask.IsLayerInLayerMask(col.gameObject.layer))
                return;
            this.touchCount++;
            this._touchingColliders.Add(col.collider);
            this.OnTouch(col.collider);
        }

        protected virtual void OnCollisionExit(Collision col)
        {
            if (!this.collisionSense || !this.detectionMask.IsLayerInLayerMask(col.gameObject.layer))
                return;
            this.touchCount--;
            this._touchingColliders.Remove(col.collider);
        }

        protected abstract void OnTouch(Collider col);


        protected override void DebugDrawGizmos()
        {
            if (Application.isPlaying)
                Gizmos.color = this.IsTouching ? Color.red : Color.green;
            else
                Gizmos.color = Color.grey;
            // there's no forward check
            var box = this.touchCollider as BoxCollider;
            if (box != null)
                Gizmos.DrawWireCube(this.transform.position + box.center, box.size);
            else
            {
                var sphere = this.touchCollider as SphereCollider;
                if (sphere != null)
                    Gizmos.DrawWireSphere(this.transform.position + sphere.center, sphere.radius);
                else
                {
                    var capsule = this.touchCollider as CapsuleCollider;
                    if (capsule == null) return;
                    for (float position = -capsule.height / 2f + capsule.radius;
                        position < (capsule.height / 2f);
                        position += (int)capsule.radius)
                    {
                        switch (capsule.direction)
                        {
                            case 0: //x
                                Gizmos.DrawWireSphere(
                                    this.transform.position + capsule.center + (this.transform.right * position),
                                    capsule.radius);
                                break;
                            case 1: //y
                                Gizmos.DrawWireSphere(
                                    this.transform.position + capsule.center + (this.transform.up * position),
                                    capsule.radius);
                                break;
                            default: //z
                                Gizmos.DrawWireSphere(
                                    this.transform.position + capsule.center + (this.transform.forward * position),
                                    capsule.radius);
                                break;
                        }
                    }
                }
            }
        }


        protected override void DebugDrawImportantGizmos()
        {
            if (this.IsTouching)
                this.DebugDrawGizmos();
        }
    }
}
