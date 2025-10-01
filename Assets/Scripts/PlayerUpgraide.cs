using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgraide : MonoBehaviour
{
    [SerializeField] private PlayerMoviement _player;
    [SerializeField] private Slider _expSlider;
    [SerializeField] private Level[] _levels;

    private int _currentLevel = 1;
    private float _expirience;

    public Level[] Levels { get => _levels; }
    public static Action action;

    void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            _currentLevel = PlayerPrefs.GetInt("Level");
            _expSlider.maxValue = _levels[_currentLevel].exp;
            _player.LevelUp(_levels[_currentLevel]);
        }

        if (PlayerPrefs.HasKey("Expirience"))
        {
            _expirience = PlayerPrefs.GetFloat("Expirience");
            _expSlider.value = _expirience;
        }
    }

    public void AddExpirience(float exp)
    {
        if (_currentLevel < _levels.Length  - 1)
        {
            _expirience += exp;
            _expSlider.value = _expirience;

            if (_expirience >= _levels[_currentLevel].exp)
            {
                _player.LevelUp(_levels[_currentLevel]);
                _expirience -= _levels[_currentLevel].exp;
                _expSlider.value = _expirience;

                _currentLevel++;
                _expSlider.maxValue = _levels[_currentLevel].exp;
            }
            SaveExp();
        }
    }
    private void SaveExp()
    {
        PlayerPrefs.SetFloat("Expirience", _expirience);
        PlayerPrefs.SetInt("Level", _currentLevel);
    }
}

[Serializable]
public struct Level
{
    public float health;
    public float damage;
    public float exp;
}