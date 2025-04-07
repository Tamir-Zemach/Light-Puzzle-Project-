using System.Collections;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    //if lantern enters it
    //turn it off for a timed duration

    //TODO: should require an input from player while he is holding a lantern
    [SerializeField] float toggleDuration = 2f;
    private LightScript lightToToggle;





    private void OnTriggerEnter(Collider other) 
    {
        var lightToToggle = other.GetComponent<LightScript>();
        lightToToggle?.StartToggleLightCoroutine(lightToToggle, toggleDuration);

    }





}