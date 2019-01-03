using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Effects;

namespace Unit.Abilities
{    
    public abstract class Ability : ScriptableObject
    {
        [SerializeField]
        private string _name;
        public string Name => this._name;

        [SerializeField]
        private AudioClip _soundFx;
        public AudioClip SoundFx => this._soundFx;

        public int Level { get; set; } = 1;

        [SerializeField]
        private CustomSoundEffect _soundPrefab;

        public bool Use(Creature self, Creature opponent)
        {
            if (!this.IsAllowedToUse(self, opponent))
                return false;
            if (this._soundPrefab == null || this.SoundFx == null)
                LogHelper.LogWarning(typeof(Ability), $"{this.Name} has no sound prefab or FX");
            else
            {
                CustomSoundEffect sfx = GameObject.Instantiate(this._soundPrefab);
                sfx.GetComponent<AudioSource>().clip = this.SoundFx;
                sfx.Play();
            }
            this.Execute(self, opponent);
            return true;
        }

        protected abstract void Execute(Creature self, Creature opponent);

        public virtual bool IsAllowedToUse(Creature self, Creature opponent)
        {
            // TODO: when we have cooldowns, add this
            return true;
        }

        public override string ToString() => $"Level {this.Level} {this.Name} ";
    }
}