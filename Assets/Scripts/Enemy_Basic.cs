using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float stunTime = .35f;

    private bool stunned = false,
                 dead = false;

    private Rigidbody rb;
    private Transform target;

    private void Start()
    {
        // find with raycasts or within range soon
        rb = GetComponent<Rigidbody>();
        GetComponent<Health_Enemy>().OnChangeState += ProcessState;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (ShouldFollow())
            FollowTarget();
    }

    private bool ShouldFollow()
    {
        return target != null && !stunned && !dead;
    }

    private void ProcessState(EnemyStates state)
    {
        if (state == EnemyStates.Stunned)
            StartCoroutine(GetStunned());
        else if (state == EnemyStates.Dead)
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
        ScoreManager.Instance?.AddPoints(ScoreType.Destructible);
        yield return new WaitForSeconds(GameManager.DespawnTime);
        Destroy(gameObject);
    }
}
