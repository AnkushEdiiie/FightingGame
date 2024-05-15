using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEditor.PackageManager;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
public class RebindManager : MonoBehaviour
{
    //public static GameInput _inputActions;
    public static PlayerInput _inputActions;

    public static event Action _rebindComplete;
    public static event Action _rebindCanceled;
    public static event Action<InputAction, int> _rebindStarted;

    private void Awake()
    {
        _inputActions = FindObjectOfType<PlayerInput>();
    }

    public static void StartRebind(string _actionname, int _bindingIndex, TMP_Text _statusText)
    {
        InputAction _action = _inputActions.actions.FindAction(_actionname);

        if(_action == null || _action.bindings.Count <= _bindingIndex)
        {
            Debug.Log("Coudn't find action or binding");
            return;
        }

        if (_action.bindings[_bindingIndex].isComposite)
        {
            var _firstPartIndex = _bindingIndex + 1;

            if(_firstPartIndex <_action.bindings.Count && _action.bindings[_firstPartIndex].isPartOfComposite)
            {
                DoRebind(_action, _firstPartIndex, _statusText, true);
            }
        }
        else
        {
            DoRebind(_action, _bindingIndex, _statusText, false);
        }
    }
    private static void DoRebind(InputAction _actionToRebind, int _bindingIndex, TMP_Text _statusText, bool _allCompositeParts)
    {
        if(_actionToRebind != null && _bindingIndex < 0)
            return;

        _statusText.text = $"Press a {_actionToRebind.expectedControlType}";

        _actionToRebind.Disable();

        var _rebind = _actionToRebind.PerformInteractiveRebinding(_bindingIndex);



        _rebind.OnComplete(operation =>
        {
            _actionToRebind.Enable();
            operation.Dispose();

            if (_allCompositeParts)
            {
                var _nextBindingIndex = _bindingIndex + 1;
                if(_nextBindingIndex< _actionToRebind.bindings.Count && _actionToRebind.bindings[_nextBindingIndex].isComposite)
                    DoRebind(_actionToRebind, _nextBindingIndex, _statusText, _allCompositeParts);
            }

            _rebindComplete?.Invoke();
        });

        _rebind.OnCancel(operation =>
        {
            _actionToRebind.Enable();
            operation.Dispose();

            _rebindCanceled?.Invoke();
        });

        _rebindStarted?.Invoke(_actionToRebind, _bindingIndex);
        _rebind.Start(); // Start the Rebinding Process
    }
}