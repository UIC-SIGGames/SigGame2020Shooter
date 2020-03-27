using UnityEngine;

public abstract class aEnemy : MonoBehaviour
{
    #region Customizables
    [Header("Basic Properties")]
    public LayerMask attackLayer;
    public float MoveSpeed = 10f,
                 TurnSpeed = 2f,
                 StunTime = .4f,
                 PostStunScanRadius = 30f;
    public int   MaxNumBatteriesDropped = 3;
    public ScoreType ScoreType = ScoreType.EnemyCharger;
    #endregion

    public Transform Target { get; private set; }
    public Rigidbody Rb { get; private set; }
    public bool IsDead { get; private set; }

    private iHealth health;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        nextPosition = transform.position;

        health = GetComponent<iHealth>();
        health.OnTakeDamage += HandleDamage;

        InitializeStates();

        ScoreManager.Instance?.TrackPeripheralMetrics(MetricType.EnemySpawned);
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void HandleDamage()
    {
        if (health.PercentLeft() <= 0.01)
            stateMachine.SendInterrupt(InterruptTypes.Dead);
        else
            stateMachine.SendInterrupt(InterruptTypes.Hit);
    }
    public void SetDead() => IsDead = true;

    #region Movement
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    bool newMoveReady = false; // allow cool flips when shot
    public void SetMoveProperties(Vector3 posOffset, Vector3 rotationOffset, float moveSpeedMultiplier = 1f)
    {
        nextPosition = transform.position + posOffset * MoveSpeed * Time.fixedDeltaTime * moveSpeedMultiplier;
        nextRotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(rotationOffset),
            Time.fixedDeltaTime * TurnSpeed);

        nextRotation = Quaternion.Euler(0, nextRotation.eulerAngles.y, 0);

        newMoveReady = true;
    }

    private void Move()
    {
        if (newMoveReady)
        {
            Rb.MovePosition(nextPosition);
            transform.rotation = nextRotation;
            newMoveReady = false;
        }
    }
    #endregion

    #region State Machine Stuff
    public void SetTarget(Transform target) => Target = target;
    protected StateMachine stateMachine;
    protected abstract void InitializeStates();
    #endregion
}