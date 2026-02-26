using System;
using UnityEngine;

public class WindowView : MonoBehaviour
{
    public event Action OnShowWindow;
    public event Action OnHideWindow;

    [Header("Components")]
    [SerializeField]
    protected CanvasGroupShowController showController;

    public bool IsShown => showController.IsShown;

    protected bool isInitialized;

    public virtual void Init()
    {
        if (isInitialized == true) return;

        showController.OnShowed += OnShow;
        showController.OnHided += OnHide;

        isInitialized = true;
    }

    public virtual void Dispose()
    {
        if (isInitialized == false) return;

        showController.OnShowed -= OnShow;
        showController.OnHided -= OnHide;

        isInitialized = false;
    }

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

    public virtual void OnShow()
    {
        OnShowWindow?.Invoke();
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

    public virtual void OnHide()
    {
        OnHideWindow?.Invoke();
    }
}