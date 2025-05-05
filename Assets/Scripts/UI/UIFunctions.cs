
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

        _uiElementGetter._pauseCanvas.enabled = false;
        _uiElementGetter._startCanvas.enabled = false;
        _uiElementGetter._StartuiCamera.enabled = false;
        _uiElementGetter._thirdPersonController._canMove = true;
        _uiElementGetter.playAudioOnStart._PlayAudioOnStart();
        pauseButton._pressedPauseButton = false;
        pauseButton._inStartCanvas = false;
        _uiElementGetter._starterAssetsInputs.cursorInputForLook = true;
        _uiElementGetter._starterAssetsInputs.SetCursorState(true);
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
        _uiElementGetter._pauseCanvas.enabled = true;
        _uiElementGetter._thirdPersonController._canMove = false;
        _uiElementGetter._starterAssetsInputs.cursorInputForLook = false;
        _uiElementGetter._starterAssetsInputs.SetCursorState(false);
    }
    

    public void Continue()
    {
        _uiElementGetter._thirdPersonController._canMove = true;
        _uiElementGetter._pauseCanvas.enabled = false;
        _uiElementGetter._audioSettingsCanvas.enabled = false;
        _uiElementGetter._pickupCamera.enabled = false;
        pauseButton._pressedPauseButton = false;
        _uiElementGetter._starterAssetsInputs.cursorInputForLook = true;
        _uiElementGetter._starterAssetsInputs.SetCursorState(true);

    }


    void SetGameObjectAndChildrenActive(GameObject parent, bool state)
    {
        parent.SetActive(state);
        foreach (Transform child in parent.transform)
        {
            SetGameObjectAndChildrenActive(child.gameObject, state);
        }
    }

    public void PlaySound()
    {
        AudioManager.instance.playOneShot(FmodEvents.instance.UIClick, transform.position);
    }
}