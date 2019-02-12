using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Items.ItemSO;

namespace Assets.Scripts.Items
{
    [Serializable]
    public class Item: IItem
    {
        [SerializeField]
        private int _test;
        public int Test => this._test;

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

        public override string ToString() => "Item class";

        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}
