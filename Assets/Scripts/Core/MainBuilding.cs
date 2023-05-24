using System.Collections;
using System.Collections.Generic;
using Abstractions;
using UnityEngine;

public sealed class MainBuilding : MonoBehaviour , ISelectable, IUnitProducer
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform _unitParent;
    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon; 

    private float _health = 1000;
    public void ProduceUnit()
    {
        Instantiate(_unitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitParent);
    }

    
}
