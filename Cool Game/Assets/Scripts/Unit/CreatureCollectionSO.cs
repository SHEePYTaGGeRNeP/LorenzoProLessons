using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "Creatures", menuName = "Creatures/CreateCreatureCollection")]
    public class CreatureCollectionSO : ScriptableObject
    {
        [SerializeField]
        private CreatureSO[] _creatures;
        public IEnumerable<CreatureSO> Creatures { get { return this._creatures.AsEnumerable(); } }
    }
}