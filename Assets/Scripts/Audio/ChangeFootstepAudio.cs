using StarterAssets;
using UnityEngine;

public class ChangeFootstepAudio : MonoBehaviour
{
    [SerializeField] bool FromMetalToWood = true;


    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller);

        if (FromMetalToWood)
        {
            if (controller.isWalkingOnMetal == true)
            {
                controller.isWalkingOnMetal = false;
            }

        }
        else
        {
            if (controller.isWalkingOnMetal == false)
            {
                controller.isWalkingOnMetal = true;
            }
        }
    }
}
