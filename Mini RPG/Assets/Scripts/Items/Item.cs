using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [Serializable]
    public class Item
    {
        [SerializeField]
        private int _test;
        public int Test => this._test;

        public override string ToString() =>             "Item base";
    }
    [Serializable]
    public class ItemB : Item
    {
        [SerializeField]
        private string _test2;
        public string Test2 => this._test2;
        public override string ToString() => "Item B";
    }
}
