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
    [SerializeField] LanternColor lanternColor;



    private LightReactionTest currentHitLightReactionScript;
    private Light spotLightChild; // should it be child?
    private string colorTag; //working with strings feels dangerous

    // TODO: Ask pavel about strings, maybe there is a better way to do it

    private void OnValidate()
    {
        spotLightChild = GetComponentInChildren<Light>();

        if (lanternColor == LanternColor.Red)
        {
            colorTag = "Red";
            spotLightChild.color = Color.red;
        }
        if (lanternColor == LanternColor.Green)
        {
            colorTag = "Green";
            spotLightChild.color = Color.green;
        }
        if (lanternColor == LanternColor.Blue)
        {
            colorTag = "Blue";
            spotLightChild.color = Color.blue;
        }
    }


    void Update()
    {

        Physics.queriesHitTriggers = true;

        /* before checking for light Reaction script, make it null nad remove color tag from the list - 
         * so that if it hits a different target it still removes colortag from the first hit*/
        if (currentHitLightReactionScript != null)
        {
            if (currentHitLightReactionScript.colorsHittingNow.Contains(colorTag))
            {
                currentHitLightReactionScript.colorsHittingNow.Remove(colorTag);
                currentHitLightReactionScript = null;
            }

        }

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

        // is this required code? seems to work fine without it since it checks for it at the start of update
        /* else
         {
             if (currentHitLightReactionScript != null)

                 if (currentHitLightReactionScript.colorsHittingNow.Contains(colorTag))
                 {
                     currentHitLightReactionScript.colorsHittingNow.Remove(colorTag);
                     currentHitLightReactionScript = null;
                 }
         } */

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * maxRayCastDistance, spotLightChild.color);
    }
}
