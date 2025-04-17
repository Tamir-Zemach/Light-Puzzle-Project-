using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{

    [field: Header("SFX")]
    [field: SerializeField] public EventReference Dissolve { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootstep { get; private set; }
    [field: SerializeField] public EventReference playerLand { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference windAndWaves { get; private set; }

    [field: Header("Audio Queues")]
    [field: SerializeField] public EventReference ChildThemeQueue { get; private set; }






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

