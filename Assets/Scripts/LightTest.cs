
using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour
{

    [SerializeField] float RayCastRange = 15f;
    [SerializeField] float sphereCastRadius = 2f;
    [SerializeField] LayerMask IgnoredLayer;
    [SerializeField] LanternColor lanternColor;

    private Material[] materials;
    private Color defaultEmission;

    private LightReactionTest currentHitLightReactionScript;
    //private LightReactionTest lightReactionScriptHitBySphere;
    private Light[] VisualLights;

    private SphereCollider OverlapSphere;

    private Color VisualColor; //must be a better way to do this

    private void Awake()
    {

        VisualLights = GetComponentsInChildren<Light>();

        OverlapSphere = GetComponent<SphereCollider>();
        OverlapSphere.isTrigger = true;

        switch (lanternColor)
        {
            case LanternColor.Red:
                foreach (var light in VisualLights)
                {
                    light.color = Color.red;
                    VisualColor = Color.red;
                }
                break;

            case LanternColor.Yellow:
                foreach (var light in VisualLights)
                {
                    light.color = Color.yellow;
                    VisualColor = Color.yellow;
                }
                break;

            case LanternColor.Blue:
                foreach (var light in VisualLights)
                {
                    light.color = Color.blue;
                    VisualColor = Color.blue;
                }
                break;
        }

        OverlapSphere.radius = sphereCastRadius;
    }

    private void Start()
    {
        materials = GetComponentInChildren<Renderer>().materials;
        defaultEmission = VisualColor * 2.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled)
        {
            var lightReactionScript = other.GetComponent<LightReactionTest>();
            lightReactionScript?.AddColorToList(lanternColor);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.enabled)
        {
            var lightReactionScript = other.GetComponent<LightReactionTest>();
            lightReactionScript?.AddColorToList(lanternColor);
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
            Debug.DrawRay(transform.position, transform.forward * RayCastRange, VisualColor);
            Gizmos.color = VisualColor;
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

            currentHitLightReactionScript?.AddColorToList(lanternColor);
        }
    }

    private IEnumerator TurnLightOffForTimer( float toggleDuration) //probably best change this to not disable script.
    {

        

        if (this.enabled)
        {
            //material.SetColor("_EmissionColor", DefaultEmission * -10);
            foreach (Material material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
            this.enabled = false;
            
        }
        yield return new WaitForSeconds(toggleDuration);

        this.enabled = true;
        foreach (Material material in materials)
        {
            material.EnableKeyword("_EMISSION");
        }
        //material.SetColor("_EmissionColor", DefaultEmission);
    }

    public void StartToggleLightCoroutine(float toggleDuration)
    {
        StopAllCoroutines();
        StartCoroutine(TurnLightOffForTimer(toggleDuration));

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
        foreach (var light in VisualLights)
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
