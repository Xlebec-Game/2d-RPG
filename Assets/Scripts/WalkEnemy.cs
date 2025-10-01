using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WalkEnemy : BaseEnemy
{
    
       

    
    private bool _inAttack;
    private int _indexPoint;
    
    //private Transform _startPos;
    
    
    new void Start()
    {
        base.Start();
        if (_points.Length > 0)
        {
            _indexPoint = Random.Range(0, _points.Length);
            _currentTarget = _points[_indexPoint].position;
            _animator.SetBool("Run", true);
        }
        //else
        //{
        //    _currentTarget = transform.position;
        //}

    }


    new void FixedUpdate()
    {
        base .FixedUpdate();
        _rb.velocity = (_currentTarget - transform.position).normalized * _currentSpeed;
        
        if (!_inAttack && _points.Length > 0)
        {
            if (Vector2.Distance(transform.position, _points[_indexPoint].position) < 0.1f)
            {
                _indexPoint = Random.Range(0, _points.Length);
                _currentTarget = _points[_indexPoint].position;
            }
        }


        if (Vector2.Distance(transform.position, _target.transform.position) < _range && _target.enabled)
        {
            _inAttack = true;
            _currentTarget = _target.transform.position;
            _animator.SetBool("Run", true);
        }
        else if (_points.Length > 0)
        {
            _inAttack = false;
            _currentTarget = _points[_indexPoint].position;
        }
        




    }
}
