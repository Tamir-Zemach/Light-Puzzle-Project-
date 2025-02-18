using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightTest1 : MonoBehaviour
{
    //raycast in direction of light, possibly cone raycast
    //need the user to be able to choose the light color in inspector, or the ray cast to get the light from a light component as reference
    //range should also effect light


    [SerializeField] float maxRayCastDistance = 15f;
    [SerializeField] LayerMask lightHitLayer;
    [SerializeField] LanternColor lanternColor;



    private LightReactionTest currentHitLightReactionScript;
    private Light spotLightChild; // should it be child?

    private void OnValidate()
    {

        spotLightChild = GetComponentInChildren<Light>();

        switch (lanternColor)
        {
            case LanternColor.Red:
                spotLightChild.color = Color.red;
                break;

            case LanternColor.Green:
                spotLightChild.color = Color.green;
                break;

            case LanternColor.Blue:
                spotLightChild.color = Color.blue;
                break;
        }
    }


    void Update()
    {

        Physics.queriesHitTriggers = true;

        /* before checking for light Reaction script, make it null and remove color tag from the list - 
         * so that if it hits a different target it still removes colortag from the first hit*/
        if (currentHitLightReactionScript != null)
        {
            if (currentHitLightReactionScript.colorsHittingNow.Contains(lanternColor))
            {
                currentHitLightReactionScript.colorsHittingNow.Remove(lanternColor);
                currentHitLightReactionScript = null;
            }

        }

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxRayCastDistance, lightHitLayer))
        {


            var hitObject = hitInfo.transform;

            currentHitLightReactionScript = hitObject.GetComponent<LightReactionTest>();

            if (currentHitLightReactionScript != null)
            {
                if (!currentHitLightReactionScript.colorsHittingNow.Contains(lanternColor))
                {
                    currentHitLightReactionScript.colorsHittingNow.Add(lanternColor);
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * maxRayCastDistance, spotLightChild.color);
    }
}
