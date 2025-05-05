using DissolveExample;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReactionTest : MonoBehaviour
{
    [SerializeField] ColorNeededToExist colorNeeded;
    


    private Collider col;
    private ParticleSystem particles;
    private Dissolve _dissolveScript;

    private List<LanternColor> colorsHittingNow = new List<LanternColor>(); 
    private LanternColor[] colorTag;

    private bool _isExisting;
    public bool _objectInCollider;

    private void Awake()
    {
        _dissolveScript = gameObject.GetComponent<Dissolve>();
        col = GetComponent<BoxCollider>();
        particles = GetComponentInChildren<ParticleSystem>();
        HandleParticleSystemSize(col);
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
        CheckIfShouldExist();
        HandleParticleColor();
    }

    private void Exist()
    {
        if (!_isExisting && !_objectInCollider)
        {
            _dissolveScript.UnDissolve();
            col.isTrigger = false;
            _isExisting = true;
        }
    }

    private void DontExist()
    {
        if (_isExisting)
        {
            _dissolveScript._Dissolve();
            col.isTrigger = true;
            _isExisting = false;
        }

    }

    public void AddColorToList(LanternColor colorToAdd)
    {
        if (!colorsHittingNow.Contains(colorToAdd))
        {
            colorsHittingNow.Add(colorToAdd);
        }
    }

    public void RemoveColorFromList(LanternColor colorToRemove)
    {
        if (colorsHittingNow.Contains(colorToRemove))
        {
            colorsHittingNow.Remove(colorToRemove);
        }
    }

    private bool CompareListToColorTag(List<LanternColor> list, LanternColor[] lanternColorArray)
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

    public void CheckIfShouldExist()
    {
        if (colorsHittingNow == null)
        {
            Debug.LogError("colorsHittingnow is Null");
        }
            if (CompareListToColorTag(colorsHittingNow, colorTag))
            {
                DontExist();
            }
            else
            {
                Exist();
            }
    }

    private void HandleParticleColor()
    {
        var main = particles.main;

        //maybe better to have a function to detect what color hitting now, and then save
        //the color that is hitting and use it in particle system and in existing
        switch (colorsHittingNow)
        {
            case var _ when colorsHittingNow.Count == 0 && !_objectInCollider:
                particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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
                main.startColor = new Color(1f, 0.6745098f, 0.1098039f, 1f); //orange
                if (!particles.isEmitting)
                    particles.Play();
                break;

            case var _ when colorsHittingNow.Contains(LanternColor.Blue) && colorsHittingNow.Contains(LanternColor.Red) && colorsHittingNow.Count == 2:
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

    private void HandleParticleSystemSize(Collider col)
    {
        var shape = particles.shape;

        shape.scale = col.bounds.size; 
    }

}
