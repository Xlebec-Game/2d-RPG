using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject[] _poition;
    [SerializeField] private GameObject _skrewer;

    private int _index;

    void Start()
    {
        _skrewer.SetActive(false);
        _index = Random.Range(0, _poition.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Damager _))
        {
           //if(Input.GetMouseButton(0))
           //{
                Instantiate(_poition[_index], transform.position, Quaternion.identity);
                _skrewer.SetActive(true);
                _skrewer.transform.parent = null;
                Destroy(gameObject);
           //}
                       
        }
    }
}
