using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace IngloriousBlacksmiths
{
    [RequireComponent(typeof(RectTransform), typeof(BoxCollider2D))]
    public class Tool : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {

        [SerializeField] string m_InteractionTag = "Armour";

        protected Vector3 m_StartPosition = Vector3.zero;
        protected RectTransform m_Rect = null;
        protected BoxCollider2D m_Collider = null;

        protected string m_ToolName = null;

        // on exit trigger fires on start up. Flag is used to make sure the exit behaviour only happens on a true trigger exit.
        bool previouslyOverlapping = false;

        public string ToolName
        {
            get { return m_ToolName; }
        }

        protected GameObject m_OverlappingObject = null;

        // this stops the tool image from snapping to the middle of the cursor mid drag.
        Vector2 m_Offset = Vector2.zero;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (m_Rect != null && eventData != null)
            {
                m_Offset = new Vector2(m_Rect.anchoredPosition.x - eventData.position.x, m_Rect.anchoredPosition.y - eventData.position.y);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            // drag tool to follow cursor
            if (m_Rect != null && eventData != null)
            {
                m_Rect.anchoredPosition = eventData.position + m_Offset;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (m_Rect != null)
            {
                m_Rect.anchoredPosition = m_StartPosition;
            }

            m_Offset = Vector2.zero;

            if (m_OverlappingObject != null)
            {
                UseTool();
            }
        }

        public virtual void InitTool()
        {
            if (!TryGetComponent<RectTransform>(out m_Rect))
            {
                Debug.LogError($"Tool ({m_ToolName}) doesn't have a valid RectTransform.");
            }
            else
            {
                m_StartPosition = m_Rect.anchoredPosition;
            }

            if (!TryGetComponent<BoxCollider2D>(out m_Collider))
            {
                Debug.LogError($"Tool ({m_ToolName}) is missing a box collider!");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == m_InteractionTag)
            {
                m_OverlappingObject = collision.gameObject;

                previouslyOverlapping = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (previouslyOverlapping)
            {
                m_OverlappingObject = null;

                previouslyOverlapping = false;
            }
        }

        protected virtual void UseTool()
        {
        }
    }
}
