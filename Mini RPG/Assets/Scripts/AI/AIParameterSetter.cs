using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;
namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Collider))]
    class AIParameterSetter : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        public Unit Target { get; private set; }

        public Vector3 OriginalPosition { get; private set; }

        private void Awake()
        {
            this.OriginalPosition = this.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            Unit u = other.GetComponent<Unit>();
            if (other.GetComponent<Player>() == null || u == null)
                return;
            Target = u;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_CHASE, true);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform != Target.transform)
                return;
            this.Target = null;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_CHASE, false);
        }
        private void Update()
        {
            this._animator.SetBool(AISimpleParameters.NAME_IN_ORIGINAL_SPOT, Vector3.Distance(this.transform.position, this.OriginalPosition) < 1f);
            if (this.Target == null)
                return;
            float range = Vector3.Distance(this.transform.position, Target.transform.position);
            bool inRange = range <= AISimpleParameters.ATTACK_RANGE;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_ATTACK, inRange);
        }

    }
}
