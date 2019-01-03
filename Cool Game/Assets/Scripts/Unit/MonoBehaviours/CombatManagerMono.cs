using Helpers.Classes;
using UnityEngine;

namespace Unit.MonoBehaviours
{
    public class CombatManagerMono : MonoBehaviour
    {
        public static CombatManager CombatManager { get; private set; }

        [SerializeField]
        private CreatureMono _creatureMono1;

        [SerializeField]
        private CreatureMono _creatureMono2;

        public Creature Creature1 => this._creatureMono1?.Creature;
        public Creature Creature2 => this._creatureMono2?.Creature;

        private void Start()
        {
            CombatManager = new CombatManager(this._creatureMono1.Creature, this._creatureMono2.Creature);
            //ServiceLocator.AddService(CombatManager);
        }
    }
}