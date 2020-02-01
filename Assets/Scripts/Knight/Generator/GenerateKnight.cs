using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IngloriousBlacksmiths
{
    /**/
    public class GenerateKnight : MonoBehaviour
    {
        [SerializeField]
        private RectTransform[] baseBodyParts = null;
        [SerializeField]
        private RectTransform[] spots = null;

        [SerializeField]
        GameManager m_GameManager = null;

        private float ticker = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (baseBodyParts.Length != spots.Length)
            {
                Debug.LogError("Please Set Base Body Parts and Spots to the Same Length!");
            }

            Generate();
        }

        // Update is called once per frame
        void Update()
        {
            //ticker += Time.deltaTime;

            if (ticker > 10)
            {
                Generate();
                ticker = 0;
            }
        }

        void Generate()
        {
            List<RectTransform> bodyParts = new List<RectTransform>();

            for (int i = 0; i < baseBodyParts.Length; ++i)
            {
                RectTransform bodyPart = Instantiate(baseBodyParts[i], Vector3.zero, Quaternion.identity, spots[i]) as RectTransform;
                bodyPart.anchoredPosition = Vector3.zero;

                if (bodyPart.gameObject.TryGetComponent<Injuries>(out Injuries inj))
                {
                    inj.InitInjury(m_GameManager);
                }

                bodyParts.Add(bodyPart);
            }
            int health = Random.Range(0, 4);

            int maladies;

            switch (health)
            {
                case 1:
                    maladies = 1;
                    break;
                case 2:
                    maladies = 2;
                    break;
                case 3:
                    maladies = 3;
                    break;
                default:
                    maladies = Random.Range(4, bodyParts.Count * 2);
                    break;
            }

            for (int i = 0; i < maladies; ++i)
            {
                int index = Random.Range(0, bodyParts.Count);

                Transform effectedBodyPart = bodyParts[index];

                Injuries injuries;

                if (!effectedBodyPart.TryGetComponent<Injuries>(out injuries))
                {
                    --i;
                    continue;
                }

                if (injuries.HasBrokenArmour && injuries.HasForeignObject)
                {
                    --i;
                    continue;
                }
                else if (injuries.HasBrokenArmour)
                {
                    injuries.SetForeignObject(true);
                }
                else if (injuries.HasForeignObject)
                {
                    injuries.SetBrokenArmour(true);
                }
                else
                {
                    int coinFlip = Random.Range(0, 2);

                    if (coinFlip == 1)
                    {
                        injuries.SetBrokenArmour(true);
                    }
                    else
                    {
                        injuries.SetForeignObject(true);
                    }
                }
            }
        }
    }
}