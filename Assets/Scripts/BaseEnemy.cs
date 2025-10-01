using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected PlayerMoviement _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] protected float _range;
    [SerializeField] private int _expMin;
    [SerializeField] private int _expMax;

    protected Rigidbody2D _rb;
    private bool _inAttack;
    private SpriteRenderer _sr;
    protected Animator _animator;
    protected float _currentSpeed;
    private bool _canAttack;
    protected Vector3 _currentTarget;
    protected Transform[] _points;
    protected Vector3 _startPos;
    protected Spawner _spawner;
    private int _index;
    private int _currentExp;

    public int Exp { get => _currentExp; }

    public void SetDependences(PlayerMoviement player, Transform[] points, Transform start, Spawner spawner, int i)
    {
        _target = player;
        _points = points;
        _startPos = start.position;
        _spawner = spawner;
        _index = i;
    }

    public void Revive()
    {
        _spawner.Respawn(_index);
    }   

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentSpeed = _speed;
        _canAttack = true;

        _currentExp = Random.Range(_expMin, _expMax);
    }

    public void FixedUpdate()
    {
        if (_currentTarget.x < transform.position.x)
            _sr.flipX = false;
        if (_currentTarget.x > transform.position.x)
            _sr.flipX = true;

        if (Vector2.Distance(transform.position, _target.transform.position) < 1.5f && _canAttack)
        {
            StartCoroutine(Attack());
        }

    }
    private IEnumerator Attack()
    {
        _canAttack = false;
        _animator.Play("Attack");

        _currentSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(transform.position, _target.transform.position) < 1.5f)
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
