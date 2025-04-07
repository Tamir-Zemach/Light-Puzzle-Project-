using UnityEngine;

public class SeperatingWall : MonoBehaviour
{
    /* Check for pickups, if detect pickup drop it in front of wall */

    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Player_Pickup_Controller pickupController;
    [SerializeField] private float _dropDistance;

    private Vector3 _dropPoint;
    private void OnValidate()
    {
        _dropPoint = transform.position + transform.forward * _dropDistance;
    }
    private void Awake()
    {
        pickupController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Pickup_Controller>();
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
