using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one AudioManager instances found");
        }
        instance = this;
    }

    public void playOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
