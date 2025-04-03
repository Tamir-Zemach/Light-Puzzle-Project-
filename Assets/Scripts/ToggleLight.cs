using System.Collections;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    //if player enters it
    //check if he is holding a lantern
    //if he is
    //turn it off for a timed duration
    //if he isn't
    //do nothing
    [SerializeField] float toggleDuration = 2f;
    [SerializeField] Player_Pickup_Controller pickupController;



    private IEnumerator TurnLightOff(LightTest1 lightHeld)
    {
        if (lightHeld.enabled)
        {
            lightHeld.enabled = false;
        }
        yield return new WaitForSeconds(toggleDuration);

        lightHeld.enabled = true;
    }


    //TODO: need to make decisions : 1. if this works like i want it to, I need a reference to the object that the player is holdin
    //2. should LightScript be able to hit more than 1 light reaction script
    //if it does, how does it remove the color from the list when its not hitting it anymore?
    //can maybe hold a list of objects its currently hitting, but does that help? 
    // i think it does because i can test for hit objects, and remove light color from list on reaction script when i remove the hit object from the light list
}