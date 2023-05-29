using System;
using UnityEngine;

public class Player : MonoBehaviour, IHerbloreObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickUpObject;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
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
    [SerializeField] private Transform herbloreObjectHolder;


    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private HerbloreObject herbloreObject;

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
        inputs.OnInteractAlternateAction += Inputs_OnInteractAlternateAction;
    }

    private void Inputs_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.instance.IsGameInProgress())
            return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Inputs_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.instance.IsGameInProgress())
            return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // has ClearCounter component
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
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
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

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
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetHerbloreObjectFollowTransform()
    {
        return herbloreObjectHolder;
    }

    public void SetHerbloreObject(HerbloreObject herbloreObject)
    {
        this.herbloreObject = herbloreObject;

        if (herbloreObject != null)
        {
            OnPickUpObject?.Invoke(this, EventArgs.Empty);
        }
    }

    public HerbloreObject GetHerbloreObject()
    {
        return herbloreObject;
    }

    public void ClearHerbloreObject()
    {
        herbloreObject = null;
    }

    public bool HasHerbloreObject()
    {
        return herbloreObject != null;
    }
}