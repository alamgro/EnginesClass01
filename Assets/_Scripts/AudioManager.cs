using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> _audioChannels;
    [SerializeField]
    private Object _audioChannelPrefab;
    
    public List<AudioSource> AudioChannels => _audioChannels;

    private int FindEmptyChannelIndex() => _audioChannels.FindIndex(channel => channel.clip == null);

    private void OnEnable()
    {
        for (int i = 0; i < _audioChannels.Count; i++)
        {
            var channelGO = Instantiate(_audioChannelPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
            if(channelGO != null)
                _audioChannels[i] = channelGO.GetComponent<AudioSource>();
        }
    }

    private async void ClearAudio(AudioSource audioSource)
    {
        await Task.Delay(TimeSpan.FromSeconds(audioSource.clip.length));
        audioSource.clip = null;
        audioSource.outputAudioMixerGroup = null;
    }

    public void PlayAudio(AudioClip audioClip, AudioMixerGroup mixerGroup)
    {
        int emptyChannelIndex = FindEmptyChannelIndex();
        var audioSource = _audioChannels[emptyChannelIndex];
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.Play();

        ClearAudio(audioSource);
    }
}
