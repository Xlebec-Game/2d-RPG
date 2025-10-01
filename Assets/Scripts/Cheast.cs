using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheast : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private GameObject _key;

    private SpriteRenderer _sr;
    private bool _isOpen;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _key.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isOpen)
        {
            _isOpen = true;
            _sr.sprite = _openSprite;
            _key.SetActive(true);
            _key.transform.parent = null;
        }
    }

}
