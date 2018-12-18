﻿using System;
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
            this.Execute(self, opponent);
            return true;
        }

        protected virtual void Execute(Creature self, Creature opponent)
        {
            CustomSoundEffect sfx = GameObject.Instantiate(this._soundPrefab);
            sfx.GetComponent<AudioSource>().clip = this.SoundFx;
            sfx.Play();
        }

        public virtual bool IsAllowedToUse(Creature self, Creature opponent)
        {
            // TODO: when we have cooldowns, add this
            return true;
        }
    }
}