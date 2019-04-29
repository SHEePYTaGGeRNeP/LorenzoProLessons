using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;
namespace Assets.Scripts
{
    class DamageRegion : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 100)]
        private int _damage = 1;
        [SerializeField]
        [Range(0.01f, 5)]
        private float _cooldown = 0.2f;

        private float _lastDamageTime;

        private void OnCollisionEnter(Collision collision) => this.CheckDoDamage(collision.collider);
        private void OnCollisionStay(Collision collision) => this.CheckDoDamage(collision.collider);
        private void OnTriggerEnter(Collider other) => this.CheckDoDamage(other);
        private void OnTriggerStay(Collider other) => this.CheckDoDamage(other);

        private void CheckDoDamage(Collider other)
        {
            Player p = other.GetComponent<Player>();
            if (p == null || Time.time - _lastDamageTime < this._cooldown)
                return;
            _lastDamageTime = Time.time;
            Unit u = p.GetComponent<Unit>();
            u.Damage(this._damage);
        }
    }
}
