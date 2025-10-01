using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private int _scene;
    [SerializeField] private Button _button;

    void Start()
    {
        _panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _panel.SetActive(true);
            _button.onClick.AddListener(Next);
            Time.timeScale = 0;
        }
    }

    public void Close()
    {
        _panel.SetActive(false);
        _button.onClick.RemoveAllListeners();
        Time.timeScale = 1;
    }

    public void Next()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_scene);
    }
}
