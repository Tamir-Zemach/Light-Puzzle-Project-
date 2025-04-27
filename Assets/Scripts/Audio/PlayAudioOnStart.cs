using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour
{
    public void _PlayAudioOnStart()
    {
        AudioManager.instance.playOneShot(FmodEvents.instance.StartQueue, Vector3.zero);
    }
}
