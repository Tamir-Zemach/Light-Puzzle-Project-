using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReactionTest : MonoBehaviour
{

    [SerializeField, Tooltip("By default object exists when light in the right color hits it. when ticked, object disappears when light in the right color hits it.")]
    bool flipBehavior = false;
    [SerializeField] ColorNeededToExist colorNeeded;
   

    private Collider col;
    private Renderer rend;


    public List<LanternColor> colorsHittingNow = new List<LanternColor>(); //TODO: naghuty attributes, read only
    private LanternColor[] colorTag;




    private void Awake()
    {
        col = GetComponent<Collider>();
        rend = gameObject.GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        switch (colorNeeded)
        {

            case ColorNeededToExist.None:
                colorTag = new LanternColor[0];
                break;

            case ColorNeededToExist.Red:
                colorTag = new LanternColor[] { LanternColor.Red };
                break;

            case ColorNeededToExist.Green:
                colorTag = new LanternColor[] { LanternColor.Green };
                break;

            case ColorNeededToExist.Blue:
                colorTag = new LanternColor[] { LanternColor.Blue };
                break;

            case ColorNeededToExist.Purple:
                colorTag = new LanternColor[] { LanternColor.Red, LanternColor.Blue };
                break;

            case ColorNeededToExist.Cyan:
                colorTag = new LanternColor[] { LanternColor.Blue, LanternColor.Green };
                break;

            case ColorNeededToExist.White:
                colorTag = new LanternColor[] { LanternColor.Red, LanternColor.Green, LanternColor.Blue };
                break;


        }
    }

    void Update()
    {
        HandleCurrentColorHit();
    }

    public void Exist()
    {
        rend.enabled = true;
        col.isTrigger = false;

    }

    public void DontExist()
    {
        rend.enabled = false;
        col.isTrigger = true;

    }

    private bool CompareListToArray(List<LanternColor> list, LanternColor[] lanternColorArray)
    {


        if (list.Count != lanternColorArray.Length)
        {
            return false;
        }

        foreach (LanternColor color in lanternColorArray)
        {
            if (!list.Contains(color)) return false;
        }

        return true;
    }



    public void HandleCurrentColorHit()
    {
        if (colorsHittingNow == null)
        {
            Debug.LogError("colorsHittingnow is Null");
        }
        if (!flipBehavior)
        {
            //compare if all items in color tag array are in colors hitting now list.
            //if ALL items from array ARE in the list, AND the list is the SAME LENGTH of the array
            //EXIST

            //if list contains something that isnt in array, false.
            // if list is not the length array false



            if (CompareListToArray(colorsHittingNow, colorTag))
            {
                Exist();
            }
            else
            {
                DontExist();
            }
        }
        else
        {
            if (CompareListToArray(colorsHittingNow, colorTag))
            {
                DontExist();
            }
            else
            {
                Exist();
            }
        }
    }

}
