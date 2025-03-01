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
    private ParticleSystem particles;

    public List<LanternColor> colorsHittingNow = new List<LanternColor>(); //TODO: naghuty attributes, read only
    private LanternColor[] colorTag;




    private void Awake()
    {
        col = GetComponent<Collider>();
        rend = gameObject.GetComponentInChildren<Renderer>();
        particles = GetComponentInChildren<ParticleSystem>();
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

            case ColorNeededToExist.Yellow:
                colorTag = new LanternColor[] { LanternColor.Yellow };
                break;

            case ColorNeededToExist.Blue:
                colorTag = new LanternColor[] { LanternColor.Blue };
                break;

            case ColorNeededToExist.Purple:
                colorTag = new LanternColor[] { LanternColor.Red, LanternColor.Blue };
                break;

            case ColorNeededToExist.Green:
                colorTag = new LanternColor[] { LanternColor.Blue, LanternColor.Yellow };
                break;

            case ColorNeededToExist.Orange:
                colorTag = new LanternColor[] { LanternColor.Red, LanternColor.Yellow };
                break;

            case ColorNeededToExist.White:
                colorTag = new LanternColor[] { LanternColor.Red, LanternColor.Yellow, LanternColor.Blue };
                break;


        }
    }

    void Update()
    {
        HandleCurrentColorHit();
        HandleParticleColor();
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

    private void HandleParticleColor()
    {
        var main = particles.main;

        //maybe better to have a function to detect what color hitting now, and then save
        //the color that is hitting and use it in particle system and in existing
        switch (colorsHittingNow)
        {
            case var _ when colorsHittingNow.Count == 0:
                particles.Stop();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Red) && colorsHittingNow.Count == 1:              
                main.startColor = Color.red;
                if(!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Blue) && colorsHittingNow.Count == 1:
                main.startColor = Color.blue;
                if (!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Yellow) && colorsHittingNow.Count == 1:
                main.startColor = Color.yellow;
                if (!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Yellow) && colorsHittingNow.Contains(LanternColor.Red) && colorsHittingNow.Count == 2:
                print("Orange");
                main.startColor = new Color(1f, 0.6745098f, 0.1098039f, 1f); //orange
                if (!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Blue) && colorsHittingNow.Contains(LanternColor.Red) && colorsHittingNow.Count == 2:
                print("Puprle");
                main.startColor = new Color(0.7490196f, 0.2509804f, 0.7490196f, 1f ); // purple
                if (!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Yellow) && colorsHittingNow.Contains(LanternColor.Blue) && colorsHittingNow.Count == 2:
                main.startColor = Color.green;
                if (!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when  colorsHittingNow.Count == 3: //only way it can be 3 is if all colors are hitting
                main.startColor = Color.white;
                if (!particles.isEmitting)
                    particles.Play();
                break;
        }
    }

}
