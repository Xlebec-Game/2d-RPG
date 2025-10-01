using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Collider2D _col;
    [SerializeField] private Boss _boss;

    private SpriteRenderer _sr;
    private bool _isOpen;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isOpen)
        {
            Transform key = collision.transform.Find("Key");
            if (key != null)
            {
                Destroy(key.gameObject);
                _isOpen = true;
                _boss.Anger();
                _sr.sprite = _openSprite;
                _col.isTrigger = true;
            }           
        }
    }
}
