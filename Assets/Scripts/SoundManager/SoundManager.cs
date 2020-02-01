using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace IngloriousBlacksmiths
{

    public class SoundManager : MonoBehaviour
    {
        [System.Serializable]
        class SoundEntry
        {
            [SerializeField] string name = "";
            [SerializeField] AudioClip audioClip = null;

            public string Name
            {
                get { return name; }
            }

            public AudioClip AudioClip
            {
                get { return audioClip; }
            }

            public SoundEntry(string name, AudioClip audioClip)
            {
                this.name = name;
                this.audioClip = audioClip;
            }
        }

        [SerializeField] SoundEntry[] soundCollection = null;

        public void PlaySound(AudioSource audioSource, string name, bool loop = false)
        {
            AudioClip clip = GetSoundFromCollection(name);

            if(clip != null)
            {
                audioSource.clip = clip;
                audioSource.loop = loop;
                audioSource.Play();
            }
        }

        AudioClip GetSoundFromCollection(string name)
        {
            foreach (SoundEntry entry in soundCollection)
            {
                if(entry.Name == name)
                {
                    return entry.AudioClip;
                }
            }

            Debug.LogError($"Sound ({name}) does not exist in this collection!");

            return null;
        }
    }
}