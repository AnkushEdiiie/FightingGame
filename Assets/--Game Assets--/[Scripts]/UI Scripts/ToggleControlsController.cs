using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleControlsController : MonoBehaviour
{
    public InputActionReference ToggleControls;
    [SerializeField] private GameObject toggleText;
    [SerializeField] private GameObject controlsText;
    private bool isToggleControls = false;

    private void OnEnable()
    {
        ToggleControls.action.Enable();
    }

    private void OnDisable()
    {
        ToggleControls.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //ToggleControls.performed += OnToggleControls;
        ToggleControls.action.performed += OnToggleControls;
    }

    public void OnToggleControls(InputAction.CallbackContext obj)
    {
        isToggleControls = !isToggleControls;
        if (isToggleControls)
        {
            toggleText.SetActive(false);
            controlsText.SetActive(true);
        }
        else
        {
            toggleText.SetActive(true);
            controlsText.SetActive(false);
        }
    }
}
