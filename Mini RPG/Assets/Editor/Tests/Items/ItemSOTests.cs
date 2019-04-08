using System;

using Assets.Scripts.Items;
using Assets.Scripts.Units;
using NUnit.Framework;
using Units;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Editor.Tests.Items
{
    class ItemSOTests
    {
        private const string _ITEM_ASSET_PATH = "Assets/Editor/Tests/Data";

        [Test]
        public void Can_Create()
        {
            ItemSO data = ScriptableObject.CreateInstance<ItemSO>();
            //data.Name = "";
            const string name = "_itemTest";
            //AssetDatabase.CreateAsset(data, $"{_ITEM_ASSET_PATH }/{name}.asset");
            //AssetDatabase.DeleteAsset($"{_ITEM_ASSET_PATH }/{name}.asset");
            Assert.IsNotNull(data);
        }

        [Test]
        public void Can_Equip_Item()
        {
            EquippedGear eg = new EquippedGear();
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Slot = GearSlot.Head;
            eg.Equip(item);
            Assert.AreEqual(item, eg.GetItemFromSlot(item.Slot));
            Assert.IsTrue(eg.IsEquipped(item));
        }
        [Test]
        public void Can_UnEquip_EquippedItem()
        {
            EquippedGear eg = new EquippedGear();
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Slot = GearSlot.Head;
            eg.Equip(item);
            Assert.AreEqual(item, eg.GetItemFromSlot(item.Slot));
            Assert.IsTrue(eg.IsEquipped(item));
            eg.Unequip(item);
            Assert.IsNull(eg.GetItemFromSlot(item.Slot));
            Assert.IsFalse(eg.IsEquipped(item));
        }
        [Test]
        public void EquippingASecondItem_Unequips_FirstItem()
        {
            EquippedGear eg = new EquippedGear();
            ItemSO item1 = ScriptableObject.CreateInstance<ItemSO>();
            item1.Slot = GearSlot.Head;
            eg.Equip(item1);
            Assert.AreEqual(item1, eg.GetItemFromSlot(item1.Slot));
            Assert.IsTrue(eg.IsEquipped(item1));
            ItemSO item2 = ScriptableObject.CreateInstance<ItemSO>();
            item2.Slot = GearSlot.Head;
            eg.Equip(item2);
            Assert.AreEqual(item2, eg.GetItemFromSlot(item2.Slot));
            Assert.IsTrue(eg.IsEquipped(item2));
            Assert.IsFalse(eg.IsEquipped(item1));
        }
        [Test]
        public void GetItemFromSlot_NotEquipped_ReturnsNull()
        {
            EquippedGear gear = new EquippedGear();
            Assert.IsNull(gear.GetItemFromSlot(GearSlot.Head));
        }

        [Test]
        public void Unequipping_NonEquippedItem_ThrowsException()
        {
            EquippedGear eg = new EquippedGear();
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Slot = GearSlot.Head;
            Assert.Throws<ArgumentException>(() => eg.Unequip(item));
        }

        [Test]
        public void Equipping_HpItem_ChangesHealth()
        {
            Unit u = new GameObject().AddComponent<Unit>();
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            var behavior = ScriptableObject.CreateInstance<IncreaseHpItemBehavior>();
            const int hpIncrease = 10;
            behavior.parameters = new ItemBehavior.ItemBehaviorParameters() { values = new[] { (float)hpIncrease } };
            item.Slot = GearSlot.Head;
            item.SetBehaviors(new[] { behavior });
            int currentMaxHp = u.MaxHp;
            // Somehow increase hp
            u.EquippedGear.Equip(item);
            Assert.AreEqual(currentMaxHp + hpIncrease, u.MaxHp);
            u.EquippedGear.Unequip(item);
            Assert.AreEqual(currentMaxHp, u.MaxHp);
        }
    }
}
