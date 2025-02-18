using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReactionTest : MonoBehaviour
{

    //private Rigidbody rb;
    private Collider col;
    private Renderer rend;
    [SerializeField] bool flipBehavior = false;

    private string colorTag;
    [SerializeField] ColorNeeded colorNeeded;
    [SerializeField] bool startAwake = true;
    public List<string> colorsHittingNow = new List<string>();

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        if(colorNeeded == ColorNeeded.None)
        {
            colorTag = "None";
        }

        if (colorNeeded == ColorNeeded.Blue)
        {
            colorTag = "Blue";
        }

        if (colorNeeded == ColorNeeded.Green)
        {
            colorTag = "Green";
        }
        if (colorNeeded == ColorNeeded.Red)
        {
            colorTag = "Red";
        }
        if (startAwake)
        {
            colorsHittingNow.Add(colorTag);
            Exist();
        }
        else
            DontExist();
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

    public void HandleCurrentColorHit()
    {
        if (!flipBehavior)
        {
            if (colorsHittingNow.Contains(colorTag) && colorsHittingNow.Count == 1)
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
            if (colorsHittingNow.Contains(colorTag) && colorsHittingNow.Count == 1)
            {
                DontExist();
            }
            else
            {
                Exist();
            }
        }
    }

    public enum ColorNeeded
    {
        None,
        Red,
        Green,
        Blue
    }

}
