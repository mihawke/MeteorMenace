using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    //InputActions field
    private InputActions inputActions;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputActions(); //Assign inputactions created
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        float move = 0f;
        if (inputActions.Player.Left.IsPressed())
        {
            move = -1f;
        }
        if (inputActions.Player.Right.IsPressed())
        {
            move = 1f;
        }
        rb.linearVelocityX = move * 5f;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Meteor meteor))
        {
            Destroy(gameObject);
            GameManager.Instance.TriggerGameOver();
        }
    }
}
