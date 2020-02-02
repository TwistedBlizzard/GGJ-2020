using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IngloriousBlacksmiths
{
    [RequireComponent(typeof(AudioSource), typeof(Outline))]
    public class Anvil : MonoBehaviour
    {
        [SerializeField] GameManager m_GameManager = null;
        AudioSource m_Source = null;
        [SerializeField] RectTransform m_ArmourRestSpot = null;
        Injuries m_StoredArmour = null;
        [SerializeField] Outline m_Outline = null;

        public RectTransform ArmourRestSpot
        {
            get { return m_ArmourRestSpot; }
        }

        public Injuries StoredArmour
        {
            get { return m_StoredArmour; }
        }

        public AudioSource AudioSource
        {
            get { return m_Source; }
        }

        private void Awake()
        {
            if(!TryGetComponent<AudioSource>(out m_Source))
            {
                Debug.LogError("Anvil has no audio source!");
            }
        }

        public void StoreArmour(Injuries storedArmour)
        {
            m_StoredArmour = storedArmour;

            m_Outline.enabled = false;

            m_GameManager.SoundManager.PlaySound(m_Source, "ArmorOn_01");
        }

        public void RemoveArmour()
        {
            m_StoredArmour = null;

            m_GameManager.SoundManager.PlaySound(m_Source, "ArmorOff_01");
        }
    }

}