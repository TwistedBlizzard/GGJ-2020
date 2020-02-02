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

        protected override void UseTool(Injuries armour)
        {
            base.UseTool(armour);

            m_GameManager.SoundManager.PlaySound(m_GameManager.Anvil.AudioSource, "ToolUse_01");
        }
    } 
}
