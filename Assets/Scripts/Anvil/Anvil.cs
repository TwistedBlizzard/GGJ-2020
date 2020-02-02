using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
    [RequireComponent(typeof(AudioSource))]
    public class Anvil : MonoBehaviour
    {
        [SerializeField] GameManager m_GameManager = null;
        AudioSource m_Source = null;

        private void Awake()
        {
            if(!TryGetComponent<AudioSource>(out m_Source))
            {
                Debug.LogError("Anvil has no audio source!");
            }
        }

        public void UseAnvil(Tool toolUsed)
        {

        }
    }

}