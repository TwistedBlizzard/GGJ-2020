using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
	public class HammerTool : Tool
	{
        public override void InitTool()
        {
            m_ToolName = "Hammer";

            base.InitTool();
        }

        protected override void UseTool()
        {
            base.UseTool();

            m_GameManager.SoundManager.PlaySound(audioSource, "ToolUse_01");
        }
    } 
}
