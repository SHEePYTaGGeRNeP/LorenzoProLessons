using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;
using Assets.Scripts.AI.Sensors;
using UnityEngine.Assertions;
namespace Assets.Scripts.AI
{
    class AIParameterSetter : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private PlayerTouchSensor3D _playerTouchSensor3D;

        public Unit Target { get; private set; }

        public Vector3 OriginalPosition { get; private set; }

        private void Awake()
        {
            Assert.IsNotNull(this._playerTouchSensor3D);
            this.OriginalPosition = this.transform.position;
        }
        
        private void Update()
        {
            this._animator.SetBool(AISimpleParameters.NAME_IN_ORIGINAL_SPOT, Vector3.Distance(this.transform.position, this.OriginalPosition) < 1f);
            bool playerInRange = this._playerTouchSensor3D.Player != null;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_CHASE, playerInRange);
            if (!playerInRange)
            {
                this.Target = null;
                return;
            }
            this.Target = this._playerTouchSensor3D.Player.GetComponentInParent<Unit>();
            float range = Vector3.Distance(this.transform.position, this._playerTouchSensor3D.Player.transform.position);
            bool inRange = range <= AISimpleParameters.ATTACK_RANGE;
            this._animator.SetBool(AISimpleParameters.NAME_IN_RANGE_FOR_ATTACK, inRange);
        }

    }
}
