using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
    [RequireComponent(typeof(AudioSource))]
    public class Forge : MonoBehaviour
    {
        [SerializeField] GameManager m_GameManager = null;
        AudioSource m_Source = null;

        private void Awake()
        {
            if (!TryGetComponent<AudioSource>(out m_Source))
            {
                Debug.LogError("Forge has no AudioSource");
            }
        }

        private void Start()
        {
            m_GameManager?.SoundManager.PlaySound(m_Source, "Ambience_01", true);
        }
    } 
}
