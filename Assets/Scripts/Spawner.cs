using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private PlayerMoviement _player;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Vector2[] _spawnPos;
    [SerializeField] private float _spawnDelay = 20;
    [SerializeField] private PlayerUpgraide _upgrade;

    private List<GameObject> _enemyes = new();

    void Start()
    {
        if (_spawnPos.Length == 0)
        {
            _spawnPos = new Vector2[] { transform.position };
        }
        
        for (int i = 0; i < _spawnPos.Length; i++)
        {
            CreateEnemy(i);
        }
    }

    public void CreateEnemy(int i)
    {
        GameObject gameObject = Instantiate(_spawnObject, _spawnPos[i], _spawnObject.transform.rotation);
        gameObject.GetComponent<BaseEnemy>().SetDependences(_player, _points, transform, this, i);
        _enemyes.Add(gameObject);
        gameObject.GetComponent<HealthSystem>().Upgrade = _upgrade;
    }

    public void Respawn(int index)
    {
        _enemyes.RemoveAt(index);
        StartCoroutine(Revive(index));
    }
    private IEnumerator Revive(int index)
    {
        yield return new WaitForSeconds(_spawnDelay);
        CreateEnemy(index);
    }
}
