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
            Assert.IsTrue(true);
        }

        [Test]
        public void Can_Equip_NonEquippedItem()
        {
            EquippedGear eg = new EquippedGear();
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Slot = ItemSO.GearSlot.Head;
            Assert.IsTrue(eg.Equip(item));
            // throw exception instead of return value?
        }

        [Test]
        public void Cannot_Unequip_NonEquippedItem()
        {
            EquippedGear eg = new EquippedGear();
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Slot = ItemSO.GearSlot.Head;
            eg.Equip(item);
            Assert.IsFalse(eg.Equip(item));
        }
        [UnityTest]
        public void Equipping_HpItem_IncreasesHealth()
        {
            Unit u = new GameObject().AddComponent<Unit>();
            EquippedGear eg = new EquippedGear(u);
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.Slot = ItemSO.GearSlot.Head;
            const int hpIncrease = 10;
            int currentMaxHp = u.MaxHp;
            // Somehow increase hp
            eg.Equip(item);
            Assert.AreEqual(currentMaxHp + hpIncrease, u.MaxHp);
        }
    }
}
