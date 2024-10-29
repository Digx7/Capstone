using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicSystem : GenericSingleton<MusicSystem>
{
    public List<Song> playlist;
    private AudioSource source;
    private int playlistIdex = 0;
    private bool isPlaying = false;
    private bool isPaused = false;
    private bool nextSongIsQued = false;

    public void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        if(playlist.Count == 0)
        {
            Debug.LogWarning("The Music System has no songs in its playlist");
            return;
        }
        Play();
    }

    private bool PlayListIsEmpty()
    {
        if(playlist.Count == 0)
        {
            Debug.LogWarning("The Music System has no songs in its playlist");
            return true;
        }
        return false;
    }

    private void NextSong()
    {
        if(PlayListIsEmpty()) return;

        if(isPlaying) Pause();
        
        playlistIdex++;
        if(playlistIdex > playlist.Count) playlistIdex = 0;

        source.clip = playlist[playlistIdex].clip;
        Play();
    }

    private void Skip()
    {
        if(PlayListIsEmpty()) return;

        if(isPlaying)
        {
            Stop();
            NextSong();
        }
    }

    private void Stop()
    {
        if(PlayListIsEmpty()) return;
        if(isPlaying)
        {
            source.Stop();
            isPlaying = false;

            if(nextSongIsQued) StopCoroutine("WaitingUntilNextSong");
        }
    }

    private void Pause()
    {
        if(PlayListIsEmpty()) return;
        
        if(isPlaying)
        {
            source.Pause();
            isPlaying = false;
            isPaused = true;

            if(nextSongIsQued) StopCoroutine("WaitingUntilNextSong");
        }
    }

    private void Play()
    {
        if(PlayListIsEmpty()) return;
        if(isPlaying) return;

        if(source.clip == null)
        {
            source.clip = playlist[playlistIdex].clip;
        }

        float timeUntilNextSong = 0.0f;

        if(!isPaused)
        {
            source.Play();
            timeUntilNextSong = playlist[playlistIdex].clip.length;
        }
        else
        {
            source.UnPause();
            isPaused = false;
            timeUntilNextSong = playlist[playlistIdex].clip.length - source.time;
        }
        
        isPlaying = true;

        

        if(nextSongIsQued) StopCoroutine("WaitingUntilNextSong");
        StartCoroutine(WaitingUntilNextSong(timeUntilNextSong));
    }

    private IEnumerator WaitingUntilNextSong(float timeToWait)
    {
        nextSongIsQued = true;
        yield return new WaitForSeconds(timeToWait);
        nextSongIsQued = false;
        NextSong();
    }
}
