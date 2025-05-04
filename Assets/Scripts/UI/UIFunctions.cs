
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(UIElementGetter))] 
public class UIFunctions : MonoBehaviour
{
    private UIElementGetter _uiElementGetter;
    private PauseButton pauseButton;
    private void Awake()
    {
        _uiElementGetter = GetComponent<UIElementGetter>();
        pauseButton = GetComponent<PauseButton>();
    }

    public void StartGame()
    {
        
        if (_uiElementGetter.HasMissingReferences())
        {
            Debug.LogError("Cannot start the game because one or more required components are missing!");
            return;
        }

        // Game Start Logic
        SetGameObjectAndChildrenActive(_uiElementGetter._pauseCanvas, false);
        _uiElementGetter._startCanvas.SetActive(false);
        _uiElementGetter._StartuiCamera.enabled = false;
        _uiElementGetter._playerInput.enabled = true;
        _uiElementGetter.playAudioOnStart._PlayAudioOnStart();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseButton._pressedPauseButton = true;
        _uiElementGetter._pickupCamera.enabled = true;
        _uiElementGetter._startEventSystem.enabled = false;
        _uiElementGetter._pauseEventSystem.enabled = true;
        SetGameObjectAndChildrenActive(_uiElementGetter._pauseCanvas, true);
        _uiElementGetter._playerInput.enabled = false;
    }

    public void Continue()
    {
        _uiElementGetter._playerInput.enabled = true;
        SetGameObjectAndChildrenActive(_uiElementGetter._pauseCanvas, false);
        _uiElementGetter._pickupCamera.enabled = false;
        pauseButton._pressedPauseButton = false;

    }


    void SetGameObjectAndChildrenActive(GameObject parent, bool state)
    {
        parent.SetActive(state);
        foreach (Transform child in parent.transform)
        {
            SetGameObjectAndChildrenActive(child.gameObject, state);
        }
    }
}