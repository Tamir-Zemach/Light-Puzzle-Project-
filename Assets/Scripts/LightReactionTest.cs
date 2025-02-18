using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReactionTest : MonoBehaviour
{

    //private Rigidbody rb;
    private Collider col;
    private Renderer rend;

    [SerializeField, Tooltip("By default object exists when light in the right color hits it. when ticked, object disappears when light in the right color hits it.")]
    bool flipBehavior = false;

    private string[] colorTag;
    [SerializeField] ColorNeededToExist colorNeeded;
    [SerializeField] bool existOnAwake = true;
    public List<string> colorsHittingNow = new List<string>();

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        switch (colorNeeded)
        {

            case ColorNeededToExist.None:
                colorTag = new string[0];
                break;

            case ColorNeededToExist.Red:
                colorTag = new string[] { "Red" };
                break;

            case ColorNeededToExist.Green:
                colorTag = new string[] { "Green" };
                break;

            case ColorNeededToExist.Blue:
                colorTag = new string[] { "Blue" };
                break;

            case ColorNeededToExist.Purple:
                colorTag = new string[] { "Red", "Blue" };
                break;

            case ColorNeededToExist.Cyan:
                colorTag = new string[] { "Blue", "Green" };
                break;

            case ColorNeededToExist.White:
                colorTag = new string[] { "Red", "Green", "Blue" };
                break;


        }

        print("start" + colorsHittingNow);
        print("start" + colorTag);

        if (existOnAwake)
        {
            if (!flipBehavior)
            {
                colorsHittingNow.AddRange(colorTag);
            }

            Exist();
        }
        else
        {
            DontExist();
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
        //rb.constraints = RigidbodyConstraints.None;
    }

    public void DontExist()
    {
        rend.enabled = false;
        col.isTrigger = true;
        //rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private bool CompareListToArray(List<string> list, string[] stringArray)
    {
        //print(list);
        //print(stringArray);

        if (list.Count != stringArray.Length)
        {
            return false;
        }

        foreach (string str in stringArray)
        {
            if (!list.Contains(str)) return false;
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
