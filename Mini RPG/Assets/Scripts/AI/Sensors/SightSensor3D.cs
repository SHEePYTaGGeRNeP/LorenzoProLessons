using Helpers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
namespace Assets.Scripts.AI.Sensors
{
    public class SightSensor3D : Sensor
    {
        [Space(20)]
        [SerializeField]
        private Transform _parentObject;

        [Range(1, 360)]
        public float fov = 120f;

        public float viewDistance = 100;

        [Range(1, 2001)]
        public int nrOfRays = 9;

        [SerializeField]
        protected QueryTriggerInteraction queryTriggerInteraction;

        private readonly RaycastHit[] _rayHits = new RaycastHit[10];

        [Header("Debug")]
        [SerializeField]
        [Range(0, 1f)]
        protected float importantIfLessThan = 0.8f;

        [SerializeField]
        private bool _drawLineDistanceHit = true;

        [SerializeField]
        private Color[] _raycastColors;

        [SerializeField]
        private Gradient _colorGradient;

        /// <summary>
        /// First index is forward then left, right, left, right
        /// less than 0 means no collision.
        /// </summary>
        public CustomTuple<float, Collider>[] RaycastDistances { get; private set; }

        /// <summary>
        /// Index and [distance, collider]
        /// </summary>
        public List<KeyValuePair<int, CustomTuple<float, Collider>>> RaycastsThatHit
        {
            get
            {
                if (this.RaycastDistances.IsNullOrEmpty())
                    return new List<KeyValuePair<int, CustomTuple<float, Collider>>>();
                List<KeyValuePair<int, CustomTuple<float, Collider>>> result =
                    new List<KeyValuePair<int, CustomTuple<float, Collider>>>();
                for (int i = 0; i < this.RaycastDistances.Length; i++)
                {
                    if (this.RaycastDistances[i].Item1 < 0)
                        continue;
                    result.Add(new KeyValuePair<int, CustomTuple<float, Collider>>(i,
                        this.RaycastDistances[i]));
                }
                return result;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(this._parentObject, "Please set parent");   
        }

        protected override void UpdateSense()
        {
            if (this.RaycastDistances.IsNullOrEmpty() || this.RaycastDistances.Length != this.nrOfRays)
            {
                this.RaycastDistances = new CustomTuple<float, Collider >[this.nrOfRays];
                for (var i = 0; i < this.RaycastDistances.Length; i++)
                {
                    this.RaycastDistances[i] = CustomTuple.Create<float, Collider>(-1f, null);
                }
                this._raycastColors = new Color[this.nrOfRays];
            }
            Utils.FovAction(this.transform.up, this.transform.forward,
                this.nrOfRays, this.fov, this.RaycastAction);
        }

        private void RaycastAction(Vector3 direction, int index)
        {
            int count = Physics.RaycastNonAlloc(this.transform.position, direction,
                this._rayHits, this.viewDistance, this.combinedMask, this.queryTriggerInteraction);
            this.RaycastDistances[index].Item1 = -1;
            this.RaycastDistances[index].Item2 = null;
            for (int i = 0; i < count; i++)
            {
                if (this._rayHits[i].collider.transform
                    .IsTransformInMyParents(this._parentObject))
                    continue;
                if (!this.RaycastDistances[index].Item1.IsAbout(-1) &&
                    this.RaycastDistances[index].Item1 <= this._rayHits[i].distance) continue;
                this.RaycastDistances[index].Item1 = this._rayHits[i].distance;
                this.RaycastDistances[index].Item2 = this._rayHits[i].collider;
            }
            if (this.RaycastDistances[index].Item2 == null ||
                !this.blockedMask.IsLayerInLayerMask(this.RaycastDistances[index].Item2.gameObject.layer)) return;
            // if we hit a blocked layer we should set it to nothing hit.
            this.RaycastDistances[index].Item1 = -1;
            this.RaycastDistances[index].Item2 = null;

        }

        private void OnDisable()
        {
            this.RaycastDistances = null;
        }

        protected override void DebugDrawGizmos()
        {
            if (this.RaycastDistances.IsNullOrEmpty())
                this.DrawGizmosNotPlaying();
            else
                this.DrawGizmosWhilePlaying();
        }

        private float[] _copy;

        private void DrawGizmosWhilePlaying()
        {
            // if we change nrOfRays we get exceptions if we don't copy it.
            this._copy = new float[this.RaycastDistances.Length];
            Array.Copy(this.RaycastDistances.Select(x => x.Item1).ToArray(), this._copy, this.RaycastDistances.Length);
            Utils.FovAction(this.transform.up, this.transform.forward,
                this.nrOfRays, this.fov, this.DrawColoredLines);
        }

        private void DrawColoredLines(Vector3 direction, int index)
        {
            float perc = this._copy[index] < 0 ? 1 : this._copy[index] / this.viewDistance;
            Color c;
            if (perc.IsMoreThanOrAbout(1))
                c = Color.white;
            else
                c = this._colorGradient.Evaluate(perc);
            Gizmos.color = c;
            this._raycastColors[index] = c;
            if (perc > this.importantIfLessThan && this.debugOnlyOnImportantValues)
                return;
            float distance = this.viewDistance;
            if (this._drawLineDistanceHit && this._copy[index] > 0)
                distance = this._copy[index];
            Gizmos.DrawLine(this.transform.position, this.transform.position + direction * distance);
        }

        private void DrawGizmosNotPlaying()
        {
            if (this.debugOnlyOnImportantValues) return;
            Gizmos.color = Color.gray;
            Utils.FovAction(this.transform.up, this.transform.forward, this.nrOfRays, this.fov,
                (direction, i) => Gizmos.DrawLine(this.transform.position,
                    this.transform.position + direction * this.viewDistance));
        }

        protected override void DebugDrawImportantGizmos()
        {
            if (!this.RaycastDistances.IsNullOrEmpty() && this.RaycastDistances.Any(x => x.Item1 > 0))
                this.DrawGizmosWhilePlaying();
        }
    }
}
