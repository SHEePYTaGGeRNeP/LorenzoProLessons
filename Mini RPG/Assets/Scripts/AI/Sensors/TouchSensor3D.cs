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
        [Space(20)]
        [SerializeField]
        protected bool triggerSense = true;

        [SerializeField]
        protected bool collisionSense;

        [SerializeField]
        private Transform _parentObject;

        [Header("Debug TouchSensor3D")]
        [SerializeField]
        private int touchCount;

        [SerializeField]
        private List<Collider> _touchingColliders;

        private Collider _touchCollider;
        public bool IsTouching { get { return this.touchCount > 0; } }

        protected override void Awake()
        {
            base.Awake();
            this._touchCollider = this.GetComponent<Collider>();
            Assert.IsNotNull(this._parentObject, "Please set parent");
        }

        protected override void UpdateSense()
        {
            this._touchingColliders.RemoveAll(x => x == null);
            this.touchCount = this._touchingColliders.Count;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!this.triggerSense || !this.detectionMask.IsLayerInLayerMask(other.gameObject.layer)
                || !this.ShouldAddOrRemove(other))
                return;
            this.touchCount++;
            this._touchingColliders.Add(other);
            this.OnTouch(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!this.triggerSense || !this.detectionMask.IsLayerInLayerMask(other.gameObject.layer)
                || !this.ShouldAddOrRemove(other))
                return;
            this.touchCount--;
            this._touchingColliders.Remove(other);
            this.OnTouchExit(other);
        }

        protected virtual void OnCollisionEnter(Collision col)
        {
            if (!this.collisionSense || !this.detectionMask.IsLayerInLayerMask(col.gameObject.layer)
                || !this.ShouldAddOrRemove(col.collider))
                return;
            this.touchCount++;
            this._touchingColliders.Add(col.collider);
            this.OnTouch(col.collider);
        }

        protected virtual void OnCollisionExit(Collision col)
        {
            if (!this.collisionSense || !this.detectionMask.IsLayerInLayerMask(col.gameObject.layer)
                || !this.ShouldAddOrRemove(col.collider))
                return;
            this.touchCount--;
            this._touchingColliders.Remove(col.collider);
            this.OnTouchExit(col.collider);
        }

        protected abstract void OnTouch(Collider col);
        protected abstract void OnTouchExit(Collider col);

        protected virtual bool ShouldAddOrRemove(Collider col) => true;

        protected override void DebugDrawGizmos()
        {
            Color c = Color.grey;
            if (Application.isPlaying)
                c = this.IsTouching ? Color.red : Color.green;
            Utils.DrawColliderGizmo(this._touchCollider, c);
        }

        protected override void DebugDrawImportantGizmos()
        {
            if (this.IsTouching)
                this.DebugDrawGizmos();
        }
    }
}
