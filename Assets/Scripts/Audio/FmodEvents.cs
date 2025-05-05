using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{

    [field: Header("SFX")]
    [field: SerializeField] public EventReference Dissolve { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootstepMetal { get; private set; }
    [field: SerializeField] public EventReference playerFootstepWood { get; private set; }
    [field: SerializeField] public EventReference playerLand { get; private set; }

    [field: SerializeField] public EventReference playerPickup { get; private set; }

    [field: SerializeField] public EventReference playerDrop { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference windAndWaves { get; private set; }

    [field: Header("Audio Queues")]
    [field: SerializeField] public EventReference PianoMelody { get; private set; }
    [field: SerializeField] public EventReference CelloMelody { get; private set; }
    [field: SerializeField] public EventReference StartQueue { get; private set; }

    [field: Header("OST")]
    [field: SerializeField] public EventReference EndingTheme { get; private set; }






    public static FmodEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FmodEvents instance");
        }
        instance = this;
    }
}

