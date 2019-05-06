using Assets.Scripts.Helpers.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.Behaviors
{
    [CreateAssetMenu(fileName = "IncreaseMovementSpeedBehavior", menuName = "Items/Behaviors/CreateIncreaseMovementSpeedBehavior", order = 0)]
    public class IncreaseMovementSpeedBehavior : ItemBehavior
    {
        [SerializeField]
        private float _movementSpeed = 1f;
        public float MovementSpeed { get => this._movementSpeed; set { this._movementSpeed = value; } }
        public override void OnEquip()
        {
            SimpleCharacterControl hs = Toolbox.Instance.GetToolboxComponent<SimpleCharacterControl>();
            hs.IncreaseMovementSpeed(this._movementSpeed);
        }
        public override void OnUnequip()
        {
            SimpleCharacterControl hs = Toolbox.Instance.GetToolboxComponent<SimpleCharacterControl>();
            hs.DecreaseMovementSpeed(this._movementSpeed);
        }
    }
}
