using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortMethodSelector : MonoBehaviour
{
    [Serializable]
    public class SortMethodSettings
    {
        [SerializeField]
        private string m_SortMethodName;
        public string SortMethodName => m_SortMethodName;

        [SerializeField]
        private SortMethod m_SortMethod = SortMethod.Popularity;
        public SortMethod SortMethod => m_SortMethod;
    }

    public event Action<SortMethod> OnSortMethodSelected;

    [Header("Components")]
    [SerializeField]
    private Dropdown dropdown;

    [Header("Settings")]
    [SerializeField]
    private List<SortMethodSettings> m_SortMethods = new List<SortMethodSettings>();
    public IReadOnlyList<SortMethodSettings> SortMethods => m_SortMethods;

    private bool isInit;

    public void Init()
    {
        if (isInit) return;

        InitDropdown();

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);

        isInit = false;
    }

    public void ResetSelector()
    {
        if (isInit)
        {
            if (dropdown.value == 0)
            {
                OnDropdownValueChanged(0);
            }

            dropdown.value = 0;
        }
    }

    private void InitDropdown()
    {
        List<string> options = new List<string>();

        foreach (SortMethodSettings sortMethod in m_SortMethods)
        {
            options.Add(sortMethod.SortMethodName);
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.value = 0;
    }

    private void OnDropdownValueChanged(int value)
    {
        OnSortMethodSelected?.Invoke(m_SortMethods[value].SortMethod);
    }
}