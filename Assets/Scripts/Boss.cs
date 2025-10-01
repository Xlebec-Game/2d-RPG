using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private PlayerMoviement _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private Exp _exp;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    private bool _anger;
    private bool _canAttack;

    public float Exp { get => _exp.value; set => _exp.value = value; }

    public void Anger()
    {
        _anger = true;
        _animator.SetBool("Run", true);
    }

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _anger = false;
        _canAttack = true;
    }

    void FixedUpdate()
    {
        if (_anger)
        {
            _rb.velocity = (_player.transform.position - transform.position).normalized * _speed;

            _sr.flipX = _player.transform.position.x < transform.position.x;

            if (Vector2.Distance(_player.transform.position, transform.position) < 2f)
            {
                if(_canAttack) 
                    StartCoroutine(Attack());
            }
        } 
    }

    private IEnumerator Attack()
    {
        float speed = _speed;
        _canAttack = false;
        _animator.Play($"Attack{Random.Range(1, 3)}");
        _speed = 0;
        yield return new WaitForSeconds(0.34f);
        if (Vector2.Distance(_player.transform.position, transform.position) < 2f)
        {
            if (!_player.InBlock)
                _player.GetComponent<HealthSystem>().TakeDamage(_damage);
        }            
        yield return new WaitForSeconds(0.34f);
        _speed = speed;
        yield return new WaitForSeconds(1.7f);
        _canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        _health -= _damage;

        if (_health <= 0)
        {
            _anger = false;
            _animator.Play("Death");
            _player.GetComponent<HealthSystem>().Upgrade.AddExpirience(_exp.value);
            Destroy(gameObject, 3);
        }
        else
            _animator.Play("Take_hit");
    }
}