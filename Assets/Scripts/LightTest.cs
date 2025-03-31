using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class LightTest1 : MonoBehaviour
{
    //raycast in direction of light, possibly cone raycast
    //need the user to be able to choose the light color in inspector, or the ray cast to get the light from a light component as reference
    //range should also effect light




    [SerializeField] float maxRayCastDistance = 15f;
    //[SerializeField] float sphereCastRadius = 2f;
    [SerializeField] LayerMask IgnoredLayer;
    [SerializeField] LanternColor lanternColor;




    private LightReactionTest currentHitLightReactionScript;
    //private LightReactionTest lightReactionScriptHitBySphere;
    private Light spotLightChild; // should it be child?

    private void OnValidate()
    {

        spotLightChild = GetComponentInChildren<Light>();

        switch (lanternColor)
        {
            case LanternColor.Red:
                spotLightChild.color = Color.red;
                break;

            case LanternColor.Yellow:
                spotLightChild.color = Color.yellow;
                break;

            case LanternColor.Blue:
                spotLightChild.color = Color.blue;
                break;
        }
    }


    void Update()
    {
        RaycastForLightReaction();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * maxRayCastDistance, spotLightChild.color);
        Gizmos.color = spotLightChild.color;
        //Gizmos.DrawWireSphere(transform.position, sphereCastRadius);

    }

    private void RaycastForLightReaction()
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

        bool isRaycastHitting = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxRayCastDistance, ~IgnoredLayer);


        if (isRaycastHitting)
        {
            var hitObject = hitInfo.transform;

            hitObject.TryGetComponent<LightReactionTest>(out currentHitLightReactionScript);

            if (currentHitLightReactionScript != null)
            {
                if (!currentHitLightReactionScript.colorsHittingNow.Contains(lanternColor))
                {
                    currentHitLightReactionScript.colorsHittingNow.Add(lanternColor);
                }
            }
        }

        /*Collider[] sphereOverlapColliders = Physics.OverlapSphere(transform.position, sphereCastRadius, ~IgnoredLayer, QueryTriggerInteraction.Collide);

        if (sphereOverlapColliders.Length > 0)
        {
            foreach (var collider in sphereOverlapColliders)
            {
                //need to find a place to remove color from list

                lightReactionScriptHitBySphere = collider.GetComponent<LightReactionTest>();
                if (lightReactionScriptHitBySphere != null)
                {
                    if (!lightReactionScriptHitBySphere.colorsHittingNow.Contains(lanternColor))
                    {
                        lightReactionScriptHitBySphere.colorsHittingNow.Add(lanternColor);
                    }
                }

            }
        }
        */
    }

    private void OnDisable()
    {
        if(currentHitLightReactionScript != null)
        {
            if (currentHitLightReactionScript.colorsHittingNow.Contains(lanternColor))
            {
                currentHitLightReactionScript.colorsHittingNow.Remove(lanternColor);
                currentHitLightReactionScript = null;
            }

        }
    }
}
