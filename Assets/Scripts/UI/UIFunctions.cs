
using UnityEngine;

[RequireComponent(typeof(UIElementGetter))] 
public class UIFunctions : MonoBehaviour
{
    private UIElementGetter uiElementGetter;

    private void Awake()
    {
        uiElementGetter = GetComponent<UIElementGetter>();
    }

    public void StartGame()
    {
        
        if (uiElementGetter.HasMissingReferences())
        {
            Debug.LogError("Cannot start the game because one or more required components are missing!");
            return;
        }

        // Game Start Logic
        uiElementGetter._startCanvas.SetActive(false);
        uiElementGetter._uiCamera.enabled = false;
        uiElementGetter._playerInput.enabled = true;
        uiElementGetter.playAudioOnStart._PlayAudioOnStart();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}