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
        public string Name { get => this._name; set { this._name = value; } }

        [SerializeField]
        private GearSlot _slot;
        public GearSlot Slot { get { return this._slot; } set { this._slot = value; } }

        [SerializeField]
        private bool _isStackable;
        public bool IsStackable => this._isStackable;


        [SerializeField]
        private ItemBehavior[] _behaviors;
        public void SetBehaviors(ItemBehavior[] behaviors)
        {
            this._behaviors = behaviors;
        }

        public void OnEquip(GameObject owner)
        {
            if (this._behaviors.IsNullOrEmpty())
                return;
            foreach (ItemBehavior behavior in _behaviors)
                behavior.OnEquip(owner);
        }
        public void OnUnequip(GameObject owner)
        {
            if (this._behaviors.IsNullOrEmpty())
                return;
            foreach (ItemBehavior behavior in _behaviors)
                behavior.OnUnequip(owner);
        }

    }
}
