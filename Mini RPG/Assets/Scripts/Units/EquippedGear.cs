using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Items;
using Units;
using UnityEngine;
namespace Assets.Scripts.Units
{
    public class EquippedGear
    {
        private readonly Dictionary<GearSlot, ItemSO> _gear = new Dictionary<GearSlot, ItemSO>();
        private GameObject _owner;
        public EquippedGear(GameObject owner)
        { this._owner = owner; }
        public void Equip(ItemSO item)
        {
            ItemSO equippedItem = this.GetItemFromSlot(item.Slot);
            if (equippedItem != null)
                this.Unequip(equippedItem);
            this._gear[item.Slot] = item;
            item.OnEquip(this._owner);
        }
        public void Unequip(ItemSO item)
        {
            if (!this.IsEquipped(item))
                throw new ArgumentException("Item is not equipped");
            this._gear.Remove(item.Slot);
            item.OnUnequip(this._owner);
        }
        public ItemSO GetItemFromSlot(GearSlot slot)
        {
            if (this._gear.ContainsKey(slot))
                return this._gear[slot];
            return null;
        }
        public bool IsEquipped(ItemSO item)
        {
            if (!this._gear.ContainsKey(item.Slot))
                return false;
            return this._gear[item.Slot] == item;
        }
    }
}
