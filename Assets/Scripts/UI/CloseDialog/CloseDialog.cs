using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
    public class CloseDialog : MonoBehaviour
    {
        [SerializeField] GameManager m_GameManager = null;

        public void OnClickYesButton()
        {
            if (Application.isEditor)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        }

        public void OnClickNoButton()
        {
            EnableQuitDialog(false);

            m_GameManager?.PauseGame(false);
        }

        public void EnableQuitDialog(bool makeEnabled)
        {
            gameObject.SetActive(makeEnabled);
        }
    } 
}
