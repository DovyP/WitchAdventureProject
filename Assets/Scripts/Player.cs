using UnityEngine;

/* ****************************************************************************************
 * 
 *      THIS CODE IS PURELY FOR TESTING PURPOSES,
 *      AFTER TESTING PHASE, CODE WILL BE REFACTORED TO USE THE NEW UNITY'S INPUT SYSTEM.
 * 
 * **************************************************************************************** */


public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotateSpeed;

    private bool isWalking;

    [SerializeField] private Inputs inputs;

    private void Update()
    {
        Vector2 inputVector = inputs.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * movementSpeed * Time.deltaTime;  
        isWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}