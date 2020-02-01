using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IngloriousBlacksmiths
{
    [RequireComponent(typeof(Image))]
    public class Injuries : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        GameManager m_GameManager = null;
        RectTransform m_Rect = null;

        Image image;

        public Sprite foreignObject;
        public Sprite brokenArmour;
        public Sprite both;
        public Sprite fixedArmour;

        Vector2 m_Offset = Vector2.zero;
        Vector3 m_StartPosition = Vector3.zero;
        GameObject m_OverlappingObject = null;


        bool dragging = false;

        bool hasForeignObject;
        public bool HasForeignObject
        {
            get
            {
                return hasForeignObject;
            }
        }
        bool hasBrokenArmour;
        public bool HasBrokenArmour
        {
            get
            {
                return hasBrokenArmour;
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (TryGetComponent<Image>(out image))
            {
                //Image Found
            }
            else
            {
                //Something's Gone Very Wrong
                Debug.LogError("No Image Found");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitInjury(GameManager gm)
        {
            m_GameManager = gm;
            
            if(!TryGetComponent<RectTransform>(out m_Rect))
            {
                Debug.LogError("Injury has no rect transform!");
            }
        }

        public void SetForeignObject(bool foreign)
        {
            hasForeignObject = foreign;
            UpdateImage();
        }

        public void SetBrokenArmour(bool broken)
        {
            hasBrokenArmour = broken;
            UpdateImage();
        }

        void UpdateImage()
        {
            if (hasForeignObject && hasBrokenArmour)
            {
                image.sprite = both;
            }
            else if (hasForeignObject)
            {
                image.sprite = foreignObject;
            }
            else if (hasBrokenArmour)
            {
                image.sprite = brokenArmour;
            }
            else
            {
                image.sprite = fixedArmour;
            }
        }

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
                    // do thing
                }

                dragging = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!m_GameManager.IsPaused)
            {
                if (dragging)
                {
                    if (collision.tag == "Anvil")
                    {
                        m_OverlappingObject = collision.gameObject;
                        OverlapHighlight(collision.gameObject, true);
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

    }
}