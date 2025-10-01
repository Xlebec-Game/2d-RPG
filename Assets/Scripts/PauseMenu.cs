using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private bool _isPause;
    [SerializeField] private GameObject _panel;
    [SerializeField] private int _scene;
    [SerializeField] private Button _Escape;
    [SerializeField] private Button _MainMeny;

    void Start()
    {
        _panel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        _isPause = !_isPause;
        _panel.SetActive(_isPause);
        Time.timeScale = _isPause ? 0 : 1;
    }




    public void MainMeny()
    {
        SceneManager.LoadScene(0);
    }
}
