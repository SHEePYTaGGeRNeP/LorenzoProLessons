using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public enum GearSlot { None, Head, Chest, Leg, Shoulder, Gloves, Boots, HandRight, HandLeft, HandBoth, RingLeft, RingRight, Amulet }

    [CreateAssetMenu(fileName = "Item", menuName = "Items/CreateItemAsset", order = 0)]
    public class ItemSO : ScriptableObject
    {
        [SerializeField]
        private int _id;
        public int Id => this._id;

        [SerializeField]
        private string _name;
        public string Name => this._name;

        [SerializeField]
        private GearSlot _slot;
        public GearSlot Slot
        {
            get { return this._slot; }
            set { this._slot = value; }
        }

        [SerializeField]
        private bool _isStackable;
        public bool IsStackable => this._isStackable;


        [Serializable]
        public class Behaviors
        {
            public BaseItemBehavior behavior;
            public float[] parameter;
        }
        [SerializeField]
        private Behaviors[] _behaviors;


        public void OnEquip(BaseItemBehavior.ItemBehaviorParameters parameters)
        {
            if (this._behaviors.IsNullOrEmpty())
                return;
            foreach (Behaviors behavior in _behaviors)
                behavior.behavior.OnEquip(parameters, behavior.parameter);
        }
        public void OnUnequip(BaseItemBehavior.ItemBehaviorParameters parameters)
        {
            if (this._behaviors.IsNullOrEmpty())
                return;
            foreach (Behaviors behavior in _behaviors)
                behavior.behavior.OnUnequip(parameters, behavior.parameter);
        }

    }
}
