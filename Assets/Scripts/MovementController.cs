using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 0.5f;
    public float slowDown = 0.01f;

    private Rigidbody2D playerRB;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;

    private Vector2 dirrection = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
            dirrection.x = -1;
        }
        else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            dirrection.x = -1;
        }
        else if (Input.GetKeyDown(rightKey))
        {
            dirrection.x = 1;
        }
        else if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
        {
            dirrection.x = 1;
        }
        else if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            dirrection.x = 0;
        }


        if (Input.GetKeyDown(downKey))
        {
            dirrection.y = -1;
        }
        else if (Input.GetKey(downKey) && !Input.GetKey(upKey))
        {
            dirrection.y = -1;
        }
        else if (Input.GetKeyDown(upKey))
        {
            dirrection.y = 1;
        }
        else if (Input.GetKey(upKey) && !Input.GetKey(downKey))
        {
            dirrection.y = 1;
        }
        else if (!Input.GetKey(downKey) && !Input.GetKey(upKey))
        {
            dirrection.y = 0;
        }

        Vector2 newVelocity = playerRB.velocity;
        newVelocity.x += acceleration * dirrection.x;
        newVelocity.y += acceleration * dirrection.y;

        if (newVelocity.x > maxSpeed)
        {
            newVelocity.x = maxSpeed;
        }
        else if (newVelocity.x < -maxSpeed)
        {
            newVelocity.x = -maxSpeed;
        }

        if (newVelocity.y > maxSpeed)
        {
            newVelocity.y = maxSpeed;
        }
        else if (newVelocity.y < -maxSpeed)
        {
            newVelocity.y = -maxSpeed;
        }

        if (dirrection.x == 0)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, slowDown);
        }
        if (dirrection.y == 0)
        {
            newVelocity.y = Mathf.Lerp(newVelocity.y, 0, slowDown);
        }

        playerRB.velocity = newVelocity;
    }
}
