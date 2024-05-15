using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference _inputActionReference;

    [SerializeField] private bool _excludeMouse = true;

    [Range(0, 10)]
    [SerializeField] private int _selectedBinding;

    [SerializeField] InputBinding.DisplayStringOptions _displayStringOptions;

    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField] 
    private InputBinding _inputBinding;
    private int _bindingIndex;
    private string _actionName;

    [Header("UI Fields")]
    [SerializeField] private TMP_Text _actionText;
    [SerializeField] private TMP_Text _rebindText;
    [SerializeField] private Button _rebindButton;
    [SerializeField] private Button _resetButton;

    private void OnEnable()
    {
        _rebindButton.onClick.AddListener(() => DoRebind());
        _resetButton.onClick.AddListener(() => ResetBinding());

        if(_inputActionReference != null)
            GetBindingInfo();
            UpdateUI();

        RebindManager._rebindComplete += UpdateUI;
    }
    private void OnDisable()
    {
        RebindManager._rebindComplete -= UpdateUI;
    }


    private void OnValidate()
    {
        if (_inputActionReference.action != null)
            return;

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if(_inputActionReference.action != null)
            _actionName = _inputActionReference.action.name;

        if(_inputActionReference.action.bindings.Count > _selectedBinding)
        {
            _inputBinding = _inputActionReference.action.bindings[_selectedBinding];
            _bindingIndex = _selectedBinding;
        }
    }
    private void UpdateUI()
    {
        if(_actionText != null)
            _actionText.text = _actionName;

        if(_rebindText != null)
        {
            if (Application.isPlaying)
            {
                _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
            }
            else
                _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
        }
    }
    private void DoRebind()
    {
        RebindManager.StartRebind(_actionName, _bindingIndex, _rebindText);
    }
    private void ResetBinding()
    {

    }

}
