using System;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    public abstract Type Tick();
}