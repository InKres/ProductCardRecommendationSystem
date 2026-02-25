using System;
using UnityEngine;

public class WindowView : MonoBehaviour
{
    public event Action OnShowWindow;
    public event Action OnHideWindow;

    [Header("Components")]
    [SerializeField]
    protected CanvasGroupShowController showController;

    [Header("Settings")]
    [SerializeField]
    protected bool isInitOnStart = true;

    public bool IsShown => showController.IsShown;


    private bool isInitialized;

    private void Start()
    {
        if (isInitOnStart)
        {
            Init();
        }
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Init()
    {
        if (isInitialized == false)
        {
            showController.OnShowed += OnShow;
            showController.OnHided += OnHide;

            isInitialized = true;
        }
    }

    public void Dispose()
    {
        if (isInitialized == true)
        {
            showController.OnShowed -= OnShow;
            showController.OnHided -= OnHide;

            isInitialized = false;
        }
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