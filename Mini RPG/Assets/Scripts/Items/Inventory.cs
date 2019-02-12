using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Items
{
    public class InventoryItem
    {
        public ItemSO Item { get; }
        public int Amount { get; set; } = 1;
        public InventoryItem(ItemSO item)
        {
            this.Item = item;
        }
    }
    class Inventory
    {
        private readonly List<InventoryItem> _inventory = new List<InventoryItem>(0);
        
        public bool IsEmpty => this._inventory.Count == 0;


        public void AddItem(ItemSO item)
        {
            if (item.IsStackable && this.ContainsItem(item.Id))
                this.GetItemById(item.Id).Amount++;
            else
                this._inventory.Add(new InventoryItem(item));
            //this.OnInventoryCollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <summary>
        /// Removes and item from the inventory.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="all">TRUE = Removes all instances/stacks of this item. FALSE = one is removed.</param>
        public void RemoveItem(int itemId, bool all)
        {
            if (!this.ContainsItem(itemId))
                throw new ArgumentException("Item does not exist in inventory", nameof(itemId));
            InventoryItem item = this.GetItemById(itemId);
            if (all || item.Amount == 1)
                this._inventory.Remove(item);
            else
                item.Amount--;
            //this.OnInventoryCollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        public InventoryItem GetItemById(int itemId)
        {
            InventoryItem item = this._inventory.FirstOrDefault(x => x.Item.Id == itemId);
            if (item == null)
                throw new ArgumentException("Item does not exist in inventory", nameof(itemId));
            return item;
        }

        public bool ContainsItem(int itemId)
        {
            return this._inventory.Any(x => x.Item.Id == itemId);
        }

    }
}
