using UnityEngine;
using System.Collections;

public class ToggleLight : MonoBehaviour
{
    //if lantern enters it
    //turn it off for a timed duration

    [SerializeField] float toggleDuration = 3.5f;
    [SerializeField] float flashDuration = 0.3f;
    [SerializeField] int flashCount = 2;



    private void OnTriggerEnter(Collider other)
    {
        var lightToToggle = other.GetComponent<LightScript>();
        lightToToggle?.StartToggleLightCoroutine(toggleDuration, flashCount, flashDuration);

    }


}