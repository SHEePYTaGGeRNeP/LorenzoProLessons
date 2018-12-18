using System;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class CustomSoundEffect : CustomEffect
    {
        [SerializeField]
        protected ulong delay = 0;

        protected AudioSource audioSource;

        protected virtual void Awake()
        {
            this.audioSource = this.GetComponent<AudioSource>();
            if (this.playOnAwake)
                this.Play();
        }

        protected virtual void Start()
        {
            if (this.playOnStart)
                this.Play();
        }

        public override float GetLength()
        {
            return this.audioSource.clip.length;
        }

        public override void Play()
        {
            this.audioSource.Play(this.delay);
            switch (this.destroyComponentAfterFinished)
            {
                case DestroyComponent.DestroyComponent:
                    Destroy(this, this.audioSource.clip.length + this.delay);
                    break;
                case DestroyComponent.DestroyGameObject:
                    Destroy(this.gameObject, this.audioSource.clip.length + this.delay);
                    break;
                case DestroyComponent.No:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}