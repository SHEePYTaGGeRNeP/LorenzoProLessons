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

        private BaseItemBehavior.ItemBehaviorParameters itemBehaviorParameters;

        public EquippedGear(Unit u)
        {
            this.itemBehaviorParameters = new BaseItemBehavior.ItemBehaviorParameters()
            {
                Unit = u
            };
        }
        public EquippedGear(Unit u, HealthSystem hs)
        {
            this.itemBehaviorParameters = new BaseItemBehavior.ItemBehaviorParameters()
            {
                Unit = u,
                HealthSystem = hs
            };
        }
        public EquippedGear()
        { this.itemBehaviorParameters = new BaseItemBehavior.ItemBehaviorParameters(); }
        public void Equip(ItemSO item)
        {
            if (this.GetItemFromSlot(item.Slot))
                this.Unequip(item);
            this._gear[item.Slot] = item;
            item.OnEquip(this.itemBehaviorParameters);
        }
        public void Unequip(ItemSO item)
        {
            if (!this.IsEquipped(item))
                throw new ArgumentException("Item is not equipped");
            this._gear.Remove(item.Slot);
            item.OnUnequip(this.itemBehaviorParameters);
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
