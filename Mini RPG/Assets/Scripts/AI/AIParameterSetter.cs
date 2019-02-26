using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Collider))]
    class AIParameterSetter : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        private Transform _target;

        public Vector3 OriginalPosition { get; private set; }

        private void Awake()
        {
            this.OriginalPosition = this.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null)
                return;
            _target = other.transform;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_CHASE, true);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform != _target)
                return;
            this._target = null;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_CHASE, false);
        }
        private void Update()
        {
            this._animator.SetBool(AISimpleParameters.NAME_IN_ORIGINAL_SPOT, this.transform.position == this.OriginalPosition);
            if (this._target == null)
                return;
            float range = Vector3.Distance(this.transform.position, _target.position);
            bool inRange = range <= AISimpleParameters.ATTACK_RANGE;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_ATTACK, inRange);
        }

    }
}
