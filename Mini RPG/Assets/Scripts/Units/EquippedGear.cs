using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Items;
using Units;

namespace Assets.Scripts.Units
{
    public class EquippedGear
    {
        private readonly Dictionary<GearSlot, ItemSO> _gear = new Dictionary<GearSlot, ItemSO>();

       // private readonly BaseItemBehavior.ItemBehaviorParameters _itemBehaviorParameters;

        private Unit _unit;
        private HealthSystem _hs;


        public EquippedGear(Unit u, HealthSystem hs)
        {
            this._unit = u;
            this._hs = hs;
        }
        public EquippedGear()
        {}
        public void Equip(ItemSO item)
        {
            ItemSO equippedItem = this.GetItemFromSlot(item.Slot);
            if (equippedItem != null)
                this.Unequip(equippedItem);
            this._gear[item.Slot] = item;
            item.OnEquip();
        }
        public void Unequip(ItemSO item)
        {
            if (!this.IsEquipped(item))
                throw new ArgumentException("Item is not equipped");
            this._gear.Remove(item.Slot);
            item.OnUnequip();
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
