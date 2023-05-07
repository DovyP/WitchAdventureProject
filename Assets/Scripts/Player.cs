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

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}
