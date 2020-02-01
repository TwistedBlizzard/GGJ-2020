using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]

public class Injuries : MonoBehaviour
{
    Image image;

    public Sprite foreignObject;
    public Sprite brokenArmour;
    public Sprite both;
    public Sprite fixedArmour;

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
        if (TryGetComponent<Image>(out image)) {
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

}