using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class GlobalSFXSystem : GenericSingleton<GlobalSFXSystem>
{
    private AudioSource source;
    
    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    
    public void Request(SFXRequest newRequest)
    {
        if(IsInvalidRequest(newRequest)) return;
        
        if(source.isPlaying)
        {
            source.Stop();
        }

        source.clip = newRequest.clip;
        source.outputAudioMixerGroup = newRequest.mixerGroup;

        source.Play();
    }

    private bool IsInvalidRequest(SFXRequest request)
    {
        if(request.clip == null)
        {
            Debug.LogWarning("GlobalSFXSystem was sent a request for a null audio clip.  Make sure that all requests have a audio clip");
            return true;
        }
        else if(request.mixerGroup == null)
        {
            Debug.LogWarning("GlobalSFXSystem was sent a request with a null mixer group.  Make sure that all requests have a mixer group");
            return true;
        }
        else return false;
    }
}
