using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button openBtn;
    [SerializeField] private Button closeBtn;

    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private PlayerMove player;

    private void OnEnable()
    {
        openBtn.onClick.AddListener(PauseGame);
        closeBtn.onClick.AddListener(ResumeGame);
    }

    private void OnDisable()
    {
        openBtn.onClick.RemoveListener(PauseGame);
        closeBtn.onClick.RemoveListener(ResumeGame);
    }

    private void PauseGame()
    {
        pauseCanvas.SetActive(true);
        player.enabled = false;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1f;
    }
}
