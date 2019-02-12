using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/CreateItemAsset", order = 0)]
    public class ItemSO : ScriptableObject, IItem
    {
        public enum GearSlot { None, All, Head, Chest, Leg, Shoulder, Gloves, Boots, HandRight, HandLeft, HandBoth, RingLeft, RingRight, Amulet }

        [SerializeField]
        private int _id;
        public int Id => this._id;

        [SerializeField]
        private string _name;
        public string Name => this._name;

        [SerializeField]
        private GearSlot _slot;
        public GearSlot Slot => this._slot;

        [SerializeField]
        private bool _isStackable;
        public bool IsStackable => this._isStackable;

        [SerializeField]
        protected Item _item;
        public Item Item => this._item;

        public void Use()
        {
            throw new NotImplementedException();
        }
        
    }
}
