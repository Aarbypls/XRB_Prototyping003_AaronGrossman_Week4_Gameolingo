using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

[SerializeField] private InputActionReference MenuActionReference;

[SerializeField] private List<GameObject> teleportPoints;
    // Start is called before the first frame update
    void Start()
    {
        MenuActionReference.action.performed += OnMenu;
        Hideteleports();
    }

    private void Hideteleports()
    {
        foreach (var tp in teleportPoints)
        {
           tp.SetActive(false); 
        }
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
