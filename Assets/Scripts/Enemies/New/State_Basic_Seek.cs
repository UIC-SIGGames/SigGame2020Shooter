using System;
using UnityEngine;

public class State_Basic_Seek : EnemyState
{
    private Vector3? seekDirection;

    public override Type Tick()
    {
        Transform newTarget = ScanForTargets();

        if (newTarget != null)
        {
            enemy.SetTarget(newTarget);
            seekDirection = null;
            return typeof(State_Basic_Pursuit);
        }

        ChangeDirectionTimer();
        CheckWalls();
        Move();

        return null;
    }

    private float timeBetweenChanges = 8f;
    private float timer = 0;
    private void ChangeDirectionTimer()
    {
        if(timer <= 0)
        {
            seekDirection = NewDirection();
            timer = timeBetweenChanges;
        }

        timer -= Time.deltaTime;
    }

    private float wallCheckDistance = 4.5f;
    private void CheckWalls()
    {
        Debug.DrawRay(transform.position, (transform.forward + transform.right) * wallCheckDistance, Color.green);
        Debug.DrawRay(transform.position, (transform.forward - transform.right) * wallCheckDistance, Color.green);
        int iterationCount = 0; // to prevent any unforeseen hard locks

        while (Physics.Raycast(transform.position, transform.forward + transform.right, wallCheckDistance))
        {
            seekDirection = NewDirection();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-transform.right, transform.up), Time.deltaTime * enemy.TurnSpeed);
            if (++iterationCount > 20)
                break;
        }

        iterationCount = 0;
        while (Physics.Raycast(transform.position, transform.forward - transform.right, wallCheckDistance))
        {
            seekDirection = NewDirection();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.right, transform.up), Time.deltaTime * enemy.TurnSpeed);

            if (++iterationCount > 20)
                break;
        }
    }

    private void Move()
    {
        if (!seekDirection.HasValue)
            seekDirection = NewDirection();

        enemy.Move(seekDirection.Value * enemy.MoveSpeed * Time.fixedDeltaTime, seekDirection.Value);
    }

    private Vector3 NewDirection()
    {
        Vector3 newDirection = Vector3.zero;

        // prevent zero vector which breaks LookRotation
        while (newDirection.sqrMagnitude < 0.5f)
        {
            newDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f));
        }

        return Vector3.ClampMagnitude(newDirection, 1); ;
    }

    private const int ANGLE_START = -45;
    private const int ANGLE_STEP = 5;
    private Quaternion startAngle = Quaternion.AngleAxis(ANGLE_START, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(ANGLE_STEP, Vector3.up);
    private Transform ScanForTargets()
    {
        Vector3 rayDirection = transform.rotation * startAngle * Vector3.forward;
        RaycastHit hitInfo;

        for (int i = ANGLE_START; i <= -ANGLE_START; i += ANGLE_STEP)
        {
            Ray ray = new Ray(transform.position, rayDirection);

            if (Physics.Raycast(ray, out hitInfo, enemy.Eyesight, enemy.attackLayer))
                return hitInfo.transform;

            Debug.DrawRay(ray.origin, ray.direction * enemy.Eyesight, Color.red);

            rayDirection = stepAngle * rayDirection;
        }

        return null;
    }
}