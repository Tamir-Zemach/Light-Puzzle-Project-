using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    [field: Header("Landing SFX")]
    [field: SerializeField] public EventReference playerLand { get; private set; }

    [field: Header("Footsteps SFX")]
    [field: SerializeField] public EventReference playerFootstep { get; private set; }

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

