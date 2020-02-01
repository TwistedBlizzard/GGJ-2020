using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace IngloriousBlacksmiths
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] UIManager m_UIManager = null;
 
        // game timer stuff
        const int TOTAL_GAME_SECS = 300;
        IEnumerator BeginCountdownCO = null;

        //knight (score) counter
        int m_SavedKnights = 0;
        int m_DeadKnights = 0;

        public int SavedKnightCount
        {
            get { return m_SavedKnights; }
        }

        public int DeadKnightCount
        {
            get { return m_DeadKnights; }
        }

        private void Start()
        {
            BeginGame();
        }

        // countdown loop
        IEnumerator BeginCountdown()
        {
            int seconds = TOTAL_GAME_SECS;

            while(seconds != 0)
            {
                yield return new WaitForSeconds(1f);

                seconds--;

                m_UIManager?.SetTimerText(TimeSpan.FromSeconds(seconds));
            }
        }

        void BeginGame()
        {
            m_UIManager?.SetTimerText(TimeSpan.FromSeconds(TOTAL_GAME_SECS));

            if(BeginCountdownCO == null)
            {
                BeginCountdownCO = BeginCountdown();
                StartCoroutine(BeginCountdownCO);
            }
        }

        void SaveKnight()
        {
            ++m_SavedKnights;

            m_UIManager?.SetSavedKnightsText(m_SavedKnights);
        }

        void KillKnight()
        {
            ++m_DeadKnights;

            m_UIManager?.SetDeadKnightsText(m_SavedKnights);
        }

        void EndGame()
        {

        }

    }
}
