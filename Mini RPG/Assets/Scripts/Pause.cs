using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Pause : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot _unpausedSnapshot;
    [SerializeField]
    private AudioMixerSnapshot _pausedSnapshot;

    private bool _isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Escape))
            return;
        this._isPaused = !this._isPaused;
        AudioMixerSnapshot snapshotTo = this._isPaused ? this._pausedSnapshot : this._unpausedSnapshot;
        snapshotTo.TransitionTo(0f);
        Time.timeScale = this._isPaused ? 0 : 1;
    }


}
