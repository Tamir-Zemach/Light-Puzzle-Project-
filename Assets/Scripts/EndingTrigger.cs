using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Ending", LoadSceneMode.Additive);
        other.TryGetComponent<PlayerInput>(out PlayerInput player);
        player.DeactivateInput();
    }
}
