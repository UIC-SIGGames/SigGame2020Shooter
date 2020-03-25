using UnityEngine;

public abstract class aEnemy : MonoBehaviour, iEnemy // temp for player detection
{
    #region Customizables
    [Header("Basic Properties")]
    public LayerMask attackLayer;
    public float MoveSpeed = 10f,
                 TurnSpeed = 2f,
                 StunTime = .4f;
    #endregion

    public Transform Target { get; private set; }
    public Rigidbody Rb { get; private set; }

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        nextPosition = transform.position;

        InitializeStates();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public bool IsDead()
    {
        return false;
    }

    #region Movement
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    public void SetMoveProperties(Vector3 posOffset, Vector3 rotationOffset, float moveSpeedMultiplier = 1f)
    {
        nextPosition = transform.position + posOffset * MoveSpeed * Time.fixedDeltaTime * moveSpeedMultiplier;
        nextRotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(rotationOffset),
            Time.fixedDeltaTime * TurnSpeed);
    }

    private void Move()
    {
        Rb.MovePosition(nextPosition);
        transform.rotation = nextRotation;
    }
    #endregion

    #region State Machine Stuff
    public void SetTarget(Transform target) { Target = target; }
    protected StateMachine stateMachine;
    protected abstract void InitializeStates();
    #endregion
}