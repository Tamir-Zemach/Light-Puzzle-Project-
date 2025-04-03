using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class LightScript : MonoBehaviour
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
        if (this.enabled)
        {
            var lightReactionScript = other.GetComponent<LightReactionTest>();
            if (lightReactionScript != null)
            {
                lightReactionScript.AddColorToList(lanternColor);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.enabled)
        {
            var lightReactionScript = other.GetComponent<LightReactionTest>();
            if (lightReactionScript != null)
            {
                lightReactionScript.AddColorToList(lanternColor);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.enabled)
        {
            var lightReactionScript = other.GetComponent<LightReactionTest>();
            lightReactionScript?.RemoveColorFromList(lanternColor);
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
    }

    private IEnumerator TurnLightOffForTimer(LightScript lightHeld, float toggleDuration) 
    {
        if (lightHeld.enabled)
        {
            lightHeld.enabled = false;
        }
        yield return new WaitForSeconds(toggleDuration);

        lightHeld.enabled = true;
    }

    public void StartToggleLightCoroutine(LightScript lightHeld, float toggleDuration)
    {
        StopAllCoroutines();
        StartCoroutine(TurnLightOffForTimer(lightHeld, toggleDuration));

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
