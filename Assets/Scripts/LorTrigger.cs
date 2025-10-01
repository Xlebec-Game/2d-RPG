using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class LorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;
    [SerializeField, TextArea] private string _field;

    void Start()
    {
        _panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _text.text = _field;
            _panel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _panel.SetActive(false);
    }
}