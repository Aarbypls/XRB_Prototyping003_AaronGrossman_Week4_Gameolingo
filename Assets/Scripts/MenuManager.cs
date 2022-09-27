using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputActionReference MenuActionReference;
    [SerializeField] private List<GameObject> teleportPoints;
    [SerializeField] private TMP_Text _scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        MenuActionReference.action.performed += OnMenu;
        HideTeleports();
    }

    private void HideTeleports()
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

    public void UpdateScoreText(int score)
    {
        _scoreText.SetText(score + "/5000 points");
    }
}
