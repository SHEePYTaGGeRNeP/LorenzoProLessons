using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    public class DistortMusic : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _audioMixer;

        [SerializeField]
        private AnimationCurve _distortionCurve;
        [SerializeField]
        private AnimationCurve _pitchCurve;

        private const string _DISTORTION_NAME = "MusicDistortion";
        private const string _PITCH_NAME = "MusicPitch";

        public void OnHealthUpdate(HealthChangeEventArgs e)
        {
            float perc = Mathf.Clamp01(1 - (float)e.CurrentHitPoints / e.MaxHitPoints);
            this._audioMixer.SetFloat(_DISTORTION_NAME, _distortionCurve.Evaluate(perc));
            this._audioMixer.SetFloat(_PITCH_NAME, _pitchCurve.Evaluate(perc));
        }
    }
}