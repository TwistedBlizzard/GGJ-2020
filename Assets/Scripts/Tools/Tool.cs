using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Tool : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    protected RectTransform m_Rect = null;
    protected string m_ToolName = null;

    // this stops the tool image from snapping to the middle of the cursor mid drag.
    Vector2 m_Offset = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_Rect != null && eventData != null)
        {
            m_Offset = new Vector2(m_Rect.position.x - eventData.position.x, m_Rect.position.y - eventData.position.y);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // drag tool to follow cursor
        if (m_Rect != null && eventData != null)
        {
            m_Rect.localPosition = eventData.position + m_Offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_Offset = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<RectTransform>(out m_Rect))
        {
            Debug.LogError($"Tool ({m_ToolName}) doesn't have a valid RectTransform.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
