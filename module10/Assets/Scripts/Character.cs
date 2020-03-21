using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
    public Transform Visual;
    public float MoveForce;
    public float JumpForce;

    Rigidbody2D rigidBody2D;
    TriggerDetector triggerDetector;
    Animator animator;
    float visualDirection;

    // Start is called before the first frame update
    void Start()
    {
        visualDirection = 1.0f;
        rigidBody2D = GetComponent<Rigidbody2D>();
        triggerDetector = GetComponentInChildren<TriggerDetector>();
        animator = GetComponentInChildren<Animator>();
    }

    public void MoveLeft()
    {
        if (triggerDetector.InTrigger)
        {
            rigidBody2D.AddForce(new Vector2(-MoveForce, 0), ForceMode2D.Force);
        }
    }

    public void MoveRight()
    {
        if (triggerDetector.InTrigger)
        {
            rigidBody2D.AddForce(new Vector2(MoveForce, 0), ForceMode2D.Force);
        }
    }

    public void Jump()
    {
        if (triggerDetector.InTrigger)
        {
            rigidBody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        float vel = rigidBody2D.velocity.x;

        if (vel < -0.01f) {
            visualDirection = -1.0f;
            //Debug.Log($"vel={vel:f5} visualDirection={visualDirection}");
        } else if (vel > 0.01f) {
            visualDirection = 1.0f;
            //Debug.Log($"vel={vel:f5} visualDirection={visualDirection}");
        }

        Vector3 scale = Visual.localScale;
        scale.x = visualDirection;
        Visual.localScale = scale;

        animator.SetFloat("speed", Mathf.Abs(vel));
    }
}
