using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]

    [SerializeField] private string parameterName;
    [SerializeField] [Range(0,1)] private float parameterValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
        }
    }
}
