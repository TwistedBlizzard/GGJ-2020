using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
    public class PincersTool : Tool
    {
        public override void InitTool()
        {
            m_ToolName = "Pincers";

            base.InitTool();
        }

        protected override void UseTool(Injuries armour)
        {
            base.UseTool(armour);
        }
    } 
}
