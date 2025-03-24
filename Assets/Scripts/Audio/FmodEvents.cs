using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootstep { get; private set; }
    [field: SerializeField] public EventReference playerLand { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference windAndWaves { get; private set; }




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

