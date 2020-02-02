using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace IngloriousBlacksmiths
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] UIManager m_UIManager = null;
        [SerializeField] InputManager m_InputManager = null;
        [SerializeField] SoundManager m_SoundManager = null;
 
        public UIManager UIManager
        {
            get { return m_UIManager; }
        }

        public InputManager InputManager
        {
            get { return m_InputManager; }
        }

        public SoundManager SoundManager
        {
            get { return m_SoundManager; }
        }


        // game timer stuff
        const int TOTAL_GAME_SECS = 300;
        IEnumerator BeginCountdownCO = null;

        //knight (score) counter
        int m_SavedKnights = 0;
        int m_DeadKnights = 0;

        bool isPaused = false;
        public bool IsPaused
        {
            get { return isPaused; }
        }

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

        private void Update()
        {
            m_InputManager?.ListenForPlayerInputs();
        }

        int seconds = TOTAL_GAME_SECS;

        // countdown loop
        IEnumerator BeginCountdown()
        {
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

        public void PauseGame(bool makePaused)
        {
            if(makePaused)
            {
                if (BeginCountdownCO != null)
                { 
                    StopCoroutine(BeginCountdownCO);
                    BeginCountdownCO = null;
                }

                Time.timeScale = 0;
            }
            else
            {
                if (BeginCountdownCO == null)
                {
                    BeginCountdownCO = BeginCountdown();
                    StartCoroutine(BeginCountdownCO);
                }

                Time.timeScale = 1;
            }

            isPaused = makePaused;
        }

        void EndGame()
        {

        }

    }
}
