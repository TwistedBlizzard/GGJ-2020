using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IngloriousBlacksmiths
{
    [RequireComponent(typeof(RectTransform), typeof(BoxCollider2D))]
    public class Tool : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] string m_InteractionTag = "Armour";
        [SerializeField] protected GameManager m_GameManager = null;

        protected Vector3 m_StartPosition = Vector3.zero;
        protected RectTransform m_Rect = null;
        protected BoxCollider2D m_Collider = null;

        protected string m_ToolName = null;

        bool dragging = false;

        public string ToolName
        {
            get { return m_ToolName; }
        }

        protected GameObject m_OverlappingObject = null;

        // this stops the tool image from snapping to the middle of the cursor mid drag.
        Vector2 m_Offset = Vector2.zero;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!m_GameManager.IsPaused)
            {
                if (m_Rect != null && eventData != null)
                {
                    m_Offset = new Vector2(m_Rect.anchoredPosition.x - eventData.position.x, m_Rect.anchoredPosition.y - eventData.position.y);
                }

                dragging = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!m_GameManager.IsPaused)
            {
                // drag tool to follow cursor
                if (m_Rect != null && eventData != null)
                {
                    m_Rect.anchoredPosition = eventData.position + m_Offset;
                } 
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!m_GameManager.IsPaused)
            {
                if (m_Rect != null)
                {
                    m_Rect.anchoredPosition = m_StartPosition;
                }

                m_Offset = Vector2.zero;

                if (m_OverlappingObject != null)
                {
                    if(m_OverlappingObject.TryGetComponent<Injuries>(out Injuries armour))
                    {
                        UseTool(armour);
                    }
                }

                dragging = false; 
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
            if (!m_GameManager.IsPaused)
            {
                if (dragging)
                {
                    if (collision.tag == m_InteractionTag)
                    {
                        if (m_GameManager.Anvil.StoredArmour != null && collision.gameObject == m_GameManager.Anvil.StoredArmour.gameObject)
                        {
                            m_OverlappingObject = collision.gameObject;
                            OverlapHighlight(collision.gameObject, true);
                        }
                    }
                } 
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!m_GameManager.IsPaused)
            {
                //if (previouslyOverlapping)
                //{
                m_OverlappingObject = null;
                OverlapHighlight(collision.gameObject, false);
                //} 
            }
        }

        void OverlapHighlight(GameObject highlightObject, bool makeHighlighted)
        {
            if (highlightObject != null)
            {
                Outline outlineObj = highlightObject.GetComponentInChildren<Outline>();

                if (outlineObj != null)
                {
                    outlineObj.enabled = makeHighlighted;
                }
            }
        }

        protected virtual void UseTool(Injuries armour)
        {
        }

    }
}
