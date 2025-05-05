using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private UIFunctions _uIFunctions;

    public bool _pressedPauseButton;
    private void Awake()
    {
        _uIFunctions = GetComponent<UIFunctions>();
        _pressedPauseButton = true;
    }

    void Update()
    {
        if (!_pressedPauseButton)
        {
            GetInput();
        }
            
    }


    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uIFunctions.PauseGame();
        }
    }

}
