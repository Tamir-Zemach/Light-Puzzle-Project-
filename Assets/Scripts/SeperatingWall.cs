using UnityEngine;

public class SeperatingWall : MonoBehaviour
{
    /* Check for pickups, if detect pickup drop it in front of wall */

    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Player_Pickup_Controller pickupController;
    private Vector3 _dropPoint;
    [SerializeField] private float _dropDistance;

    // TODO: doesn't change automatically when transform changes, why and how to change that?
    // TODO: If player tries to take multiple pickups, they get stacked in the same place, is that fine?

    private void OnValidate() 
    {
        _dropPoint = transform.position + transform.forward * _dropDistance;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (((1 << other.gameObject.layer) & pickupLayer) == 1 << other.gameObject.layer)
        {
           
            pickupController.DropPickupInFrontOfWall(_dropPoint);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_dropPoint, 0.1f);
    }
}
