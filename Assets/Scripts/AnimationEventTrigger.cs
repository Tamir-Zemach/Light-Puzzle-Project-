using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    [Tooltip("Animation Component of the Object who should play the clip")]
    public Animation _animationComponent;
    private AnimationClip _animationClip;

    public float initialDelay = 0f;
    public bool triggerOnce = false;
    private bool triggered = false;

    private void OnEnable()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !triggered)
        {
            triggered = true;
            StartCoroutine(StartDelay());
        }

    }


    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        _animationComponent?.Play();
        if (triggerOnce)
        {
            Destroy(this.gameObject);
        }
    }

}
