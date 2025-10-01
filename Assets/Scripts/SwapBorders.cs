using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBorders : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private Border _leftBorder;
    [SerializeField] private Border _rightBorder;

    private void Start()
    {
        _camera.NewBorder = _leftBorder;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                _camera.NewBorder = _leftBorder;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                _camera.NewBorder = _rightBorder;
            }
        }

    }
}
