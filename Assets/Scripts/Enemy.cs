using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerMoviement _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private Transform[] _points;

    private Rigidbody2D _rb;
    private bool _inAttack;
    private int _indexPoint;
    private SpriteRenderer _sr;
    private Transform _currentTarget;
    private Transform _startPos;
    private Animator _animator;
    private float _currentSpeed;
    private bool _canAttack;

    public void SetDependences(PlayerMoviement player, Transform[] points, Transform start)
    {
        _target = player;
        _points = points;
        _startPos = start;        
    }  

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentSpeed = _speed;
        _canAttack = true;

        if (_points.Length > 0)
        {
            _indexPoint = Random.Range(0, _points.Length);
            _currentTarget = _points[_indexPoint];
            _animator.SetBool("Run", true);
        }
        else
        {
            _currentTarget = transform;            
        }
    }
                   
    void FixedUpdate()
    {
        if (_currentTarget.position.x < transform.position.x)
            _sr.flipX = false;
        if (_currentTarget.position.x > transform.position.x)
            _sr.flipX = true;


        if (!_inAttack && _points.Length > 0)
        {
            
            if (Vector2.Distance(transform.position, _points[_indexPoint].position) < 0.1f)
            {
                _indexPoint = Random.Range(0, _points.Length);
                _currentTarget = _points[_indexPoint];
            }
            
        }


        if (Vector2.Distance(transform.position, _target.transform.position) < _range && _target.enabled)
        {
            _inAttack = true;
            _currentTarget = _target.transform;
            _animator.SetBool("Run", true);
        }
        else if (_points.Length > 0)
        {
            _inAttack = false;
            _currentTarget = _points[_indexPoint];
        }
        else
        {
            _currentTarget = _startPos;
            _inAttack = false;
            if (Mathf.RoundToInt(_rb.velocity.magnitude) == 0)
                _animator.SetBool("Run", false);
        }

        if (_inAttack)
        {
            if (Vector2.Distance(transform.position, _target.transform.position) < 1 && _canAttack)
            {
                StartCoroutine(Attack());
            }
        }
        _rb.velocity = (_currentTarget.position - transform.position).normalized * _currentSpeed;
    }  

    private IEnumerator Attack()
    {
        _canAttack = false;
        _animator.Play("Attack");
       
        _currentSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(transform.position, _target.transform.position) < 1.1f)
        {
            if (transform.position.x > _target.transform.position.x)
            {
                if (_target.transform.rotation.y == 0)
                    _target.GetComponent<HealthSystem>().TakeDamage(_damage);

                if (_target.transform.rotation.y == -1)
                {
                    if (!_target.GetComponent<PlayerMoviement>().InBlock)
                        _target.GetComponent<HealthSystem>().TakeDamage(_damage);
                }
            }
            else
            {
                if (_target.transform.rotation.y == -1)
                    _target.GetComponent<HealthSystem>().TakeDamage(_damage);

                if (_target.transform.rotation.y == 0)
                {
                    if (!_target.GetComponent<PlayerMoviement>().InBlock)
                        _target.GetComponent<HealthSystem>().TakeDamage(_damage);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        _currentSpeed = _speed;
        _canAttack = true;
    }
}
