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

        private void Start()
        {
            CombatManager = new CombatManager(this._creatureMono1.Creature, this._creatureMono2.Creature);
            //ServiceLocator.AddService(CombatManager);
        }
    }
}