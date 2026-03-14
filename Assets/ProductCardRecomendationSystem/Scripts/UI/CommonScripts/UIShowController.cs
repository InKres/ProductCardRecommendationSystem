using UnityEngine;

public class UIShowController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroupShowController showController;

    public bool IsShown => showController.IsShown;

    public void Show(bool isImmediately = false)
    {
        if (isImmediately)
        {
            showController.ImmediatelyShow();
        }
        else
        {
            showController.Show();
        }
    }

    public void Hide(bool isImmediately = false)
    {
        if (isImmediately)
        {
            showController.ImmediatelyHide();
        }
        else
        {
            showController.Hide();
        }
    }
}
