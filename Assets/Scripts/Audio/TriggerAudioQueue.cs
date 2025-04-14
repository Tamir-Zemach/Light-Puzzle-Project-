using UnityEngine;

public class TriggerAudioQueue : MonoBehaviour
{
    //TODO: i to be able to choose which audio queue gets played



    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.playOneShot(FmodEvents.instance.ChildThemeQueue, transform.position);
        Destroy(this.gameObject);
    }
}
