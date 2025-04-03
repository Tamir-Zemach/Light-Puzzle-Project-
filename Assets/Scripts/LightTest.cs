using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class LightTest1 : MonoBehaviour
{

    [SerializeField] float RayCastRange = 15f;
    [SerializeField] float sphereCastRadius = 2f;
    [SerializeField] LayerMask IgnoredLayer;
    [SerializeField] LanternColor lanternColor;




    private LightReactionTest currentHitLightReactionScript;
    //private LightReactionTest lightReactionScriptHitBySphere;
    private Light[] VisualLights;

    private SphereCollider OverlapSphere;

    private Color debugColor; //must be a better way to do this

    private void OnValidate()
    {

        VisualLights = GetComponentsInChildren<Light>();

        OverlapSphere = GetComponent<SphereCollider>();

        switch (lanternColor)
        {
            case LanternColor.Red:
                foreach (var light in VisualLights)
                {
                    light.color = Color.red;
                    debugColor = Color.red;
                }
                break;

            case LanternColor.Yellow:
                foreach (var light in VisualLights)
                {
                    light.color = Color.yellow;
                    debugColor = Color.yellow;
                }
                break;

            case LanternColor.Blue:
                foreach (var light in VisualLights)
                {
                    light.color = Color.blue;
                    debugColor = Color.blue;
                }
                break;
        }

        OverlapSphere.radius = sphereCastRadius;
    }

    private void Awake()
    {
        OverlapSphere.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var lightReactionScript = other.GetComponent<LightReactionTest>();
        if (lightReactionScript != null)
        {
            lightReactionScript.AddColorToList(lanternColor);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var lightReactionScript = other.GetComponent<LightReactionTest>();
        if (lightReactionScript != null)
        {
            lightReactionScript.AddColorToList(lanternColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var lightReactionScript = other.GetComponent<LightReactionTest>();
        if (lightReactionScript != null)
        {
            lightReactionScript.RemoveColorFromList(lanternColor);
        }
    }


    void Update()
    {
        RaycastForLightReaction();
    }

    private void OnDrawGizmos()
    {
        if (this.enabled)
        {
            Debug.DrawRay(transform.position, transform.forward * RayCastRange, debugColor);
            Gizmos.color = debugColor;
            Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
        }

    }



    private void RaycastForLightReaction()
    {
        Physics.queriesHitTriggers = true;

        /* before checking for light Reaction script, make it null and remove color tag from the list - 
         * so that if it hits a different target it still removes colortag from the first hit*/
        if (currentHitLightReactionScript != null)
        {
            currentHitLightReactionScript.RemoveColorFromList(lanternColor);
            currentHitLightReactionScript = null;


        }

        bool isRaycastHitting = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, RayCastRange, ~IgnoredLayer);


        if (isRaycastHitting)
        {
            var hitObject = hitInfo.transform;

            hitObject.TryGetComponent<LightReactionTest>(out currentHitLightReactionScript);

            if (currentHitLightReactionScript != null)
            {
                currentHitLightReactionScript.AddColorToList(lanternColor);
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

    private void OnEnable()
    {
        foreach (var light in VisualLights)
        {
            light.enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach(var light in VisualLights)
        {
            light.enabled = false;
        }
        if (currentHitLightReactionScript != null)
        {
            currentHitLightReactionScript.RemoveColorFromList(lanternColor);
            currentHitLightReactionScript = null;
        }
    }
}
