using UnityEngine;

public class Collision_Checker : MonoBehaviour
{
    private LightReactionTest _lightReactionTest;

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.TryGetComponent<LightReactionTest>(out var component))
        {
            _lightReactionTest = component;
            component._objectInCollider = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<LightReactionTest>(out var component))
        {
            _lightReactionTest = component;
            component._objectInCollider = false;
            _lightReactionTest = null;
        }
    }

}
