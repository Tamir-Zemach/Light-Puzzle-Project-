using UnityEngine;

public class TriggerCelloAudioQueue : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.playOneShot(FmodEvents.instance.CelloMelody, transform.position);
        Destroy(this.gameObject);
    }
}
