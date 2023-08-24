using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundtrackRandomiser : MonoBehaviour
{
    private int __CurrentPlayingSong;
    double __TrackTimer = 0;
    public List<AudioClip> __SoundTrack;
    public AudioSource __SoundTrackSource;

    public void Start()
    {
        __CurrentPlayingSong = Random.Range(0, __SoundTrack.Count);
        __SoundTrackSource.clip = __SoundTrack[__CurrentPlayingSong];
        __SoundTrackSource.Play();
    }

    public void Update()
    {

        if (__SoundTrackSource.isPlaying)
        {
            __TrackTimer += 1 * Time.deltaTime;
        }

        if (!__SoundTrackSource.isPlaying && __TrackTimer >= __SoundTrackSource.clip.length)
        {
            List<AudioClip> _TemporarySoundTrack = __SoundTrack.Select(song => song).ToList();
            _TemporarySoundTrack.RemoveAt(__CurrentPlayingSong);
            __CurrentPlayingSong = Random.Range(0, _TemporarySoundTrack.Count);
            __SoundTrackSource.clip = _TemporarySoundTrack[__CurrentPlayingSong];
            __SoundTrackSource.Play();
            __TrackTimer = 0;
        }
    }
}
