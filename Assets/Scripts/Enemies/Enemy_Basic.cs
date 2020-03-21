using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour, iEnemy
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float stunTime = .35f;

    private bool stunned = false,
                 dead = false;

    private Rigidbody rb;
    private Transform target;

    private bool shouldFollow { get { return target != null && !stunned && !dead; } }

    private void Start()
    {
        // find with raycasts or within range soon
        rb = GetComponent<Rigidbody>();
        GetComponent<Health_Enemy>().OnChangeState += ProcessState;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (shouldFollow)
            FollowTarget();
    }

    public bool IsDead()
    {
        return dead;
    }

    private void ProcessState(EnemyStates state)
    {
        if (state == EnemyStates.Stunned)
            StartCoroutine(GetStunned());
        else if (state == EnemyStates.Dead && !dead)
            StartCoroutine(Die());
    }

    private void FollowTarget()
    {
        rb.MovePosition(Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime));
        transform.LookAt(target);
    }

    private IEnumerator GetStunned()
    {
        stunned = true;
        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }

    private IEnumerator Die()
    {
        dead = true;
        SpawnBattery(UnityEngine.Random.Range(0,4));
        ScoreManager.Instance?.AddPoints(ScoreType.BasicEnemy);
        yield return new WaitForSeconds(GameManager.DespawnTime);
        Destroy(gameObject);
    }

    private void SpawnBattery(int numBatteries)
    {
        bool playerLowHealth = false;
        if (BatteryManager.Instance != null)
            playerLowHealth = BatteryManager.Instance.LowEnergy();

        if (playerLowHealth && numBatteries < 2)
            numBatteries = 2;

        while(numBatteries > 0)
            Instantiate(Resources.Load<GameObject>("Battery"), transform.position + Vector3.up * numBatteries--, Quaternion.identity);
    }
}
