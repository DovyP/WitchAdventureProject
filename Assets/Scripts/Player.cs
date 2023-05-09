using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [Header("Physics")]
    [SerializeField] private float playerRadius;
    [SerializeField] private float playerHeight;
    [SerializeField] private float interactDistance;

    [Space(10)]
    [Header("References")]
    [SerializeField] private Inputs inputs;
    [SerializeField] private LayerMask countersLayerMask;


    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than 1 Player Instance");
        }
        Instance = this;
    }

    private void Start()
    {
        inputs.OnInteractAction += Inputs_OnInteractAction;
    }

    private void Inputs_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = inputs.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            // using try for null check
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // has ClearCounter component
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = inputs.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // cant move towards move direction

            // move on X
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                // can move on X
                moveDirection = moveDirectionX;
            }
            else
            {
                // cannot move on X

                // move on Z
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    // can move on Z
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    // cant move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}