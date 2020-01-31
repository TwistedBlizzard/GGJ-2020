using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OnScreenDebug : MonoBehaviour
{
    TextMeshProUGUI debugText = null;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<TextMeshProUGUI>(out debugText))
        {
            Debug.LogError("Failed to get debug text.");
        }
    }
    
    public void PrintDebugMessage(string msg)
    {
        if (debugText != null)
            debugText.text = msg;
    }
}
