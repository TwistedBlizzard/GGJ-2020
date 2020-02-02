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
        [SerializeField] RectTransform m_ArmourRestSpot = null;
        Injuries m_StoredArmour = null;
        public RectTransform ArmourRestSpot
        {
            get { return m_ArmourRestSpot; }
        }

        public Injuries StoredArmour
        {
            get { return m_StoredArmour; }
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
        }

        public void RemoveArmour()
        {
            m_StoredArmour = null;
        }
    }

}