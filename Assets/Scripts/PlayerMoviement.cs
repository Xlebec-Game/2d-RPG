using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoviement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private Slider _dashSlider;

     private Rigidbody2D _rb;   
    private Animator _animator;
    private float _hor;
    private float _ver;
    private Damager _damager;
    private bool _inBlock;
    private HealthSystem _health;
    private AudioSource _source;
    private bool _canDash = true;

    public Slider DashSlider { get => _dashSlider; set => _dashSlider = value; }

    public bool InBlock { get => _inBlock; }

    public void StartRegeneration()
    {
        InvokeRepeating(nameof(Regeneration), 0, 3);
    }

    private void Regeneration()
    {
        _health.AddHealth(2);        
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        GetComponentInChildren<Damager>().SetDamage(_damage);
        _health = GetComponent<HealthSystem>();
        _source = GetComponent<AudioSource>();
        StartCoroutine(DashCoolDown());
    }

    public void LevelUp(Level level)
    {
        _damage = level.damage;
        _health.UpgradeHealth(level.health);
    }

    void FixedUpdate()
    {
        if (!_inBlock)
        { 
            _hor = Input.GetAxis("Horizontal");
            _ver = Input.GetAxis("Vertical");

            _rb.velocity = new Vector2(_hor, _ver) * _speed;

            if (_hor > 0)
                transform.localEulerAngles = Vector2.up * 180;
            if (_hor < 0)
                transform.localEulerAngles = Vector2.zero;

            if (_hor == 0 && _ver == 0)
            {
                _animator.SetBool("Run", false);
                _source.Stop();
            }
            else               
            {
                _animator.SetBool("Run", true);                
                if (!_source.isPlaying)
                    _source.Play();
            }

            if (Input.GetMouseButton(0))
            {
                _animator.Play("LightBandit_Attack");                
            }

            if (Input.GetKey(KeyCode.Space) && _canDash && _dashSlider.value > 0)
            {
                StartCoroutine(Dash(new Vector2(_hor, _ver)));
            }
        }
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("Block", true);
            _inBlock = true;
            _animator.SetBool("Run", false);
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _animator.SetBool("Block", false);
            _inBlock = false;
        }            
    }

    private IEnumerator Dash(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _canDash = false;
            _dashSlider.value -= 5;
            _rb.AddForce(direction * _speed * 5, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.005f);
            _canDash = true;
        }
    }

    private IEnumerator DashCoolDown()
    {
        _dashSlider.value += 2;
        yield return new WaitForSeconds(1);
        StartCoroutine(DashCoolDown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            collision.transform.parent = transform;
            collision.gameObject.SetActive(false);
        }
    }

}
    