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
        GameObject m_AnvilOverlapObj = null;

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
                if (m_AnvilOverlapObj != null)
                {
                    DropArmourOnAnvil();
                }
                else
                {
                    if (m_Rect != null)
                    {
                        m_Rect.anchoredPosition = m_StartPosition;
                    }

                    if (m_GameManager.Anvil.StoredArmour != null)
                        m_GameManager.Anvil.RemoveArmour();
                }

                m_Offset = Vector2.zero;

                dragging = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!m_GameManager.IsPaused)
            {
                if (dragging)
                {
                    // only trigger enter on anvil objects.
                    if (collision.tag == "Anvil")
                    {
                        m_AnvilOverlapObj = collision.gameObject;
                        OverlapHighlight(collision.gameObject, true);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!m_GameManager.IsPaused)
            {
                // Only trigger exit on anvil object if we have one.
                if (collision.gameObject == m_AnvilOverlapObj)
                {
                    m_AnvilOverlapObj = null;

                    OverlapHighlight(collision.gameObject, false);
                }
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

        void DropArmourOnAnvil()
        {
            if(m_AnvilOverlapObj != null)
            {
                if(m_AnvilOverlapObj.TryGetComponent<Anvil>(out Anvil anvil))
                {
                    if (m_Rect != null)
                    {
                        m_Rect.position = anvil.ArmourRestSpot.position;
                        anvil.StoreArmour(this);
                    }
                }
                else
                {
                    Debug.LogError($"Cannot find Anvil script on overlapped object ({m_AnvilOverlapObj.name})");
                }
            }
        }

    }
}