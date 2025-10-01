using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemy : BaseEnemy
{
   private new Vector3 _startPos;
    
    new void Start()
    {
        base.Start();
        //_startPos = new Vector3(Mathf.RoundToInt(transform.position.x),
        //                        Mathf.RoundToInt(transform.position.y),
        //                        Mathf.RoundToInt(transform.position.z));   
        _startPos = transform.position;
    }
    
    new void FixedUpdate()
    {
        base.FixedUpdate();
        _rb.velocity = (_currentTarget - transform.position).normalized * _currentSpeed;
        //if(Rb.velocity.magnitude < 4 )
        //    Rb.simulated = true;
        //else
        //    Rb.simulated = false;

        if (Vector2.Distance(transform.position, _target.transform.position) < _range)
        {
            //_rb.simulated = true;
            _currentTarget = _target.transform.position;
            _animator.SetBool("Run", true); 
        }
        else
            _currentTarget = _startPos;

        if (Vector2.Distance(transform.position, _startPos) < 0.5f)
        { 
            //_rb.isKinematic = true; 
            _animator.SetBool("Run", false);
        }
        _rb.velocity = (_currentTarget - transform.position).normalized * _currentSpeed;

    }
}
