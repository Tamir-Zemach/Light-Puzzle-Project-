using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightTest : MonoBehaviour
{
    //raycast in direction of light, possibly cone raycast
    //need the user to be able to choose the light color in inspector, or the ray cast to get the light from a light component as reference
    //range should also effect light
    //if raycast hit something that is supposed to react to light, should it react in this script?

    [SerializeField] float maxRayCastDistance = 15f;
    [SerializeField] LayerMask lightHitLayer;
    [SerializeField] ColorPicker lightColorPicker;



    private LightReactionTest currentHitLightReactionScript;
    private Light spotLightChild; // should it be child?
    private string colorTag;

    private void OnValidate()
    {
        spotLightChild = GetComponentInChildren<Light>();

        if (lightColorPicker == ColorPicker.red)
        {
            colorTag = "Red";
            spotLightChild.color = Color.red;
        }
        if (lightColorPicker == ColorPicker.green)
        {
            colorTag = "Green";
            spotLightChild.color = Color.green;
        }
        if (lightColorPicker == ColorPicker.blue)
        {
            colorTag = "Blue";
            spotLightChild.color = Color.blue;
        }
    }
    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Physics.queriesHitTriggers = true;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxRayCastDistance, lightHitLayer))
        {


            var hitObject = hitInfo.transform;

            currentHitLightReactionScript = hitObject.GetComponent<LightReactionTest>();

            if (currentHitLightReactionScript != null)
            {
                if (!currentHitLightReactionScript.colorsHittingNow.Contains(colorTag))
                {
                    currentHitLightReactionScript.colorsHittingNow.Add(colorTag);
                }
            }
        }
        else
        {
            if (currentHitLightReactionScript != null)

                if (currentHitLightReactionScript.colorsHittingNow.Contains(colorTag))
                {
                    currentHitLightReactionScript.colorsHittingNow.Remove(colorTag);
                    currentHitLightReactionScript = null;
                }
        }





        /*if (currentHitLightReactionScript != null)
        {
            if (colorTag == currentHitLightReactionScript.colorTag)
            {
                currentHitLightReactionScript.Exist();
            }
        }
    }
    else
    {
        if(currentHitLightReactionScript != null)
        {
            if (colorTag == currentHitLightReactionScript.colorTag)
            {
                currentHitLightReactionScript.DontExist();
                currentHitLightReactionScript = null;
            }
        }
    }*/
    }

    public enum ColorPicker
    {
        red, green, blue
    };


    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * maxRayCastDistance, spotLightChild.color);
    }
}
