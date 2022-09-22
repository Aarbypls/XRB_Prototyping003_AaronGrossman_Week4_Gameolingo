using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

[SerializeField] private InputActionReference MenuActionReference;
    // Start is called before the first frame update
    void Start()
    {
        MenuActionReference.action.performed += OnMenu;
    }

    private void OnMenu(InputAction.CallbackContext obj)
    {
        Transform panel = transform.GetChild(0);
        if(panel.gameObject.activeSelf) panel.gameObject.SetActive(false);
        else
        {
            panel.gameObject.SetActive(true);
        }
    }
}
