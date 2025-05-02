using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIElementGetter : MonoBehaviour
{
    public GameObject _startCanvas;
    public CinemachineCamera _uiCamera;
    public PlayerInput _playerInput;
    public PlayAudioOnStart playAudioOnStart;

    private void Awake()
    {

        _startCanvas = ValidateObject(GameObject.FindGameObjectWithTag("StartCanvas"), "StartCanvas");
        _uiCamera = ValidateComponent<CinemachineCamera>(GameObject.Find("Start_UI_Camera"), "UI Camera");
        _playerInput = ValidateComponent<PlayerInput>(GameObject.FindGameObjectWithTag("Player"), "PlayerInput");
        playAudioOnStart = ValidateComponent<PlayAudioOnStart>(GameObject.Find("AudioOnStart_ScriptHolder"), "PlayAudioOnStart");
    }

    private T ValidateComponent<T>(GameObject obj, string name) where T : Component
    {
        if (obj == null)
        {
            Debug.LogError($"{name} GameObject is missing or not found!");
            return null;
        }

        T component = obj.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"{name} component is missing in {obj.name}!");
        }

        return component;
    }

    private GameObject ValidateObject(GameObject obj, string name)
    {
        if (obj == null)
        {
            Debug.LogError($"{name} GameObject is missing or not found!");
        }
        return obj;
    }

    public bool HasMissingReferences()
    {
        return _startCanvas == null || _uiCamera == null || _playerInput == null || playAudioOnStart == null;
    }
}
