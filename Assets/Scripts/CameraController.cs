using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private Border _border;

    public Border NewBorder { set => _border = value; }
   
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(_player.position.x, _player.position.y, -10),
            _speed * Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, _border.left, _border.right),
            Mathf.Clamp(transform.position.y, _border.bottom, _border.top),
            -10);
        
    }
}

[Serializable]
public struct Border
{
    public float left;
    public float right;
    public float bottom;
    public float top;
}