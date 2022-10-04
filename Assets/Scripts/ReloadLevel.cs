using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadLevel : MonoBehaviour
{
    private Button reload;

    private void Awake()
    {
        reload = GetComponent<Button>();
    }

    private void OnEnable()
    {
        reload.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    private void OnDisable()
    {
        reload?.onClick.RemoveAllListeners();
    }
}
