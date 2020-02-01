using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace IngloriousBlacksmiths
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_TimerText = null;
        [SerializeField] TextMeshProUGUI m_DeadKnightsText = null;
        [SerializeField] TextMeshProUGUI m_SavedKnightsText = null;

        public void SetTimerText(TimeSpan elapsed)
        {
            if (m_TimerText != null)
            {
                m_TimerText.text = elapsed.ToString(@"mm\:ss");
            }
        }

        public void SetDeadKnightsText(int deadKnights)
        {
            if (m_DeadKnightsText != null)
            {
                m_DeadKnightsText.text = deadKnights.ToString();
            }
        }

        public void SetSavedKnightsText(int savedKnights)
        {
            if (m_SavedKnightsText != null)
            {
                m_SavedKnightsText.text = savedKnights.ToString();
            }
        }
    }
}
