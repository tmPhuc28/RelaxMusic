using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static RadioSetting;

public class RadioMN : MonoBehaviour
{
    public AudioSource audioSource;
    public RadioSetting radioSetting;
    public int currentTrackIndex = 0;
    public bool isPlaying = false;
    public void PlayAudioClip()
    {
        if(radioSetting.playlistPlaying.Count > 0)
        {
            PlayClip(currentTrackIndex);
        }
        else
        {
            if (!audioSource.isPlaying && !isPlaying)
            {
                Debug.Log("T");
                radioSetting.GetCurrentPlayList();
                PlayClip(currentTrackIndex);
            }

        }
    }
    void Update()
    {
        AutoPlayClip();
    }

    public void PlayClip(int index)
    {
        currentTrackIndex = index;
        GetPlayList(radioSetting.playlistPlaying[currentTrackIndex]);
        isPlaying = true;
        audioSource.Play();

    }
    public void GetPlayList(Playlist playlist)
    {
        audioSource.clip = playlist.songData.songClip;
        radioSetting.GetNameSong(playlist.songData.songName, playlist.songData.author);
    }
    public void NextPlayClip()
    {
        if(audioSource.isPlaying && isPlaying)
        {
            PlayClip(NextClip());
        }
        else
        {
            NextClip();
        }
    }
    public int NextClip()
    {
        return currentTrackIndex = (currentTrackIndex + 1) % radioSetting.playlistPlaying.Count;
    }
    public void PreviousPlayClip()
    {
        if (audioSource.isPlaying && isPlaying)
        {
            PlayClip(PreviousClip());
        }
        else
        {
            PreviousClip();
        }
    }
    public int PreviousClip()
    {
        return currentTrackIndex = (currentTrackIndex - 1 + radioSetting.playlistPlaying.Count) % radioSetting.playlistPlaying.Count;
    }
    public void AutoPlayClip()
    {
        if (radioSetting.playlistPlaying.Count > 0)
        {
            if (!audioSource.isPlaying && isPlaying)
            {

                PlayClip(NextClip());
            }
        } 
    }
    public void PauseAudioClip()
    {
        isPlaying = false;
        audioSource.Pause();
    }

    public void GetVolumeValue(float volume)
    {
        audioSource.volume = volume;
    }
    public void GetTimeAudio(Slider progressSlider)
    {
        audioSource.time = progressSlider.value * audioSource.clip.length;
    }
}
