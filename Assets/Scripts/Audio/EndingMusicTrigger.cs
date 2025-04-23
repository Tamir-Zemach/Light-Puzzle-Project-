using FMODUnity;
using UnityEngine;

public class EndingMusicTrigger : MonoBehaviour
{

    [SerializeField] GameObject originGameObject;
    private StudioEventEmitter emitter;


    private void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FmodEvents.instance.EndingTheme, originGameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        emitter.Play();
    }
}
