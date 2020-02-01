using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IngloriousBlacksmiths
{
    public class ToolsManager : MonoBehaviour
    {
        [SerializeField] Tool[] m_ToolsList = null;

        // Start is called before the first frame update
        void Start()
        {
            if (m_ToolsList != null)
            {
                foreach (Tool tool in m_ToolsList)
                {
                    tool.InitTool();
                }
            }
        }

        public Tool GetTool(string toolName)
        {
            Tool retTool = null;

            if (m_ToolsList != null)
            {
                foreach (Tool tool in m_ToolsList)
                {
                    if(tool.ToolName == toolName)
                    {
                        retTool = tool;
                        break;
                    }
                }
            }

            if(retTool == null)
            {
                Debug.LogError($"Could not find tool {toolName}");
            }

            return retTool;
        }

        public void EnableTool(string toolName, bool makeEnabled)
        {
            Tool tool = GetTool(toolName);

            if(tool != null)
            {
                tool.gameObject.SetActive(makeEnabled);
            }
        }
    }
}
