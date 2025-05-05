using System.Xml.Serialization;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private UIFunctions _uIFunctions;

    public bool _pressedPauseButton;
    public bool _inStartCanvas;
    private void Awake()
    {
        _uIFunctions = GetComponent<UIFunctions>();
        _pressedPauseButton = true;
        _inStartCanvas = true;
    }

    void Update()
    {

        Debug.Log("Cursor State: " + Cursor.lockState);

        if (!_inStartCanvas)
        {
            if (!_pressedPauseButton)
            {
                GetInput();
            }
            else
            {
                GetInputWhilePaused();
            }
        }

    }


    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uIFunctions.PauseGame();
        }
    }

    private void GetInputWhilePaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            _uIFunctions.Continue();
        }
    }



}
