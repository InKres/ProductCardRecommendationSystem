using UnityEngine;

public class UIShowController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroupShowController showController;

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
