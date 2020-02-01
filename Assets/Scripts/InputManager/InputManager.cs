using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
	public class InputManager : MonoBehaviour
	{
		[SerializeField] GameManager m_GameManager = null;
		[SerializeField] CloseDialog m_CloseDialog = null;

		public void ListenForPlayerInputs()
		{
			if (!m_GameManager.IsPaused)
			{
				if (Input.GetKeyUp(KeyCode.Escape))
				{
					m_GameManager?.PauseGame(true);
					m_CloseDialog?.EnableQuitDialog(true);
				}
			}
		}
	} 
}
