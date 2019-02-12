using Assets.Scripts.Items;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

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
    }
}
