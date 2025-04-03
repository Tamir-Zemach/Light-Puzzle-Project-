using UnityEngine;

public class OutlineHandler : MonoBehaviour
{
    public Outline _currentOutLine;

    public void ResetHighlight()
    {
        _currentOutLine?.ClearHighlight();
        _currentOutLine = null;
    }

    public void ShowPickupVisibleHint()
    {
        _currentOutLine?.Highlight();
    }

 }
