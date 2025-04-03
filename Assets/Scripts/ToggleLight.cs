using System.Collections;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    //if lantern enters it
    //turn it off for a timed duration

    //TODO: should require an input from player while he is holding a lantern
    [SerializeField] float toggleDuration = 2f;
    private LightTest1 lightToToggle;





    private void OnTriggerEnter(Collider other)
    {
        var lightToToggle = other.GetComponent<LightTest1>();
        if (lightToToggle != null)
        {
           // print(lightToToggle.gameObject.name);
            StopAllCoroutines();
            StartCoroutine(TurnLightOffForTimer(lightToToggle));
        }
    }

    private IEnumerator TurnLightOffForTimer(LightTest1 lightHeld) // should probably be called from light script
    {
        if (lightHeld.enabled)
        {
            lightHeld.enabled = false;
        }
        yield return new WaitForSeconds(toggleDuration);

        lightHeld.enabled = true;
    }



}