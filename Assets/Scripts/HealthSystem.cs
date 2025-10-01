using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private PlayerUpgraide _upgrade;
    [SerializeField] private float _health;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private bool _isHardEnemy;
    [SerializeField] private Type _type;

    private Animator _animator;
    private Collider2D _collider;
    private BaseEnemy _enemy;
    private AudioSource _source;
    private Vector3 _startPos;
    private bool _canDamage;

    public Type TypeCharacter { get => _type; set => _type = value; }
    public float Health { get => _health; set => _health = value; }
    public bool HardEnemy { get => _isHardEnemy; set => _isHardEnemy = value; }
    public Slider HPslider { get => _hpSlider; set => _hpSlider = value; }
    public bool CanDamage { get => _canDamage; set => _canDamage = value; }
    public PlayerUpgraide Upgrade { get => _upgrade; set => _upgrade = value; }


    void Start()
    {
        _canDamage = true;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _enemy = GetComponent<BaseEnemy>();
        _source = GetComponent<AudioSource>();

        if (_type == Type.player)
        {
            _hpSlider.maxValue = _health;
            _hpSlider.value = _health;
            _startPos = transform.position;
        }

    }

    public void AddHealth(float health)
    {
        _hpSlider.value += health;
        _health = _hpSlider.value;
    }

    public void TakeDamage(float damage)
    {
        if (_canDamage)
        {
            _health -= damage;

            if (_type == Type.player)
                _hpSlider.value = _health;

            if (_health <= 0)
            {
                if (_type == Type.player)
                    StartCoroutine(PlayerDead());
                else
                    StartCoroutine(Dead());
                _source.Play();
            }
            else
                _animator.SetTrigger("Hurt");
        }
        else
            _canDamage = true;
    }

    private IEnumerator Dead()
    {
        if (_isHardEnemy)
            PropsAltar.action.Invoke();

        _upgrade.AddExpirience(_enemy.Exp);
        _animator.Play("Death");
        _collider.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _enemy.Revive();
        _enemy.enabled = false;
        enabled = false;
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private IEnumerator PlayerDead()
    {
        _animator.Play("Death");
        enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<PlayerMoviement>().enabled = false;
        _collider.enabled = false;
        yield return new WaitForSeconds(5);
        _animator.enabled = false;
        Invoke(nameof(Revive), 3);
    }

    private void Revive()
    {
        _health = _hpSlider.maxValue;
        _hpSlider.value = _health;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<PlayerMoviement>().enabled = true;
        _collider.enabled = true;
        enabled = true;
        _animator.enabled = true;
        _animator.Play("LightBandit_Idle");
        transform.position = _startPos;
    }

    public void UpgradeHealth(float maxValue)
    {
        _hpSlider.maxValue = maxValue;
        _health = _hpSlider.maxValue;
        _hpSlider.value = _health;
    }


}
[SerializeField]
public enum Type
{
    player,
    enemy,
}
