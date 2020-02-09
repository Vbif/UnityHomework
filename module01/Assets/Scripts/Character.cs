using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle, MoveForward, Attack, WaitAttackCompelete, MoveBackward
    }

    public float Speed;
    public float Radius;

    private State _state = State.Idle;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private GameObject _target;
    private float _targetRadius;
    private Vector3 _targetPosition;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        switch (_state)
        {
            case State.Idle:
                transform.rotation = originalRotation;
                break;
            case State.MoveForward:
                if (Move(_targetPosition, _targetRadius))
                {
                    _state = State.MoveBackward;
                }
                break;
            case State.Attack:
                break;
            case State.WaitAttackCompelete:
                break;
            case State.MoveBackward:
                if (Move(originalPosition, 0))
                {
                    _state = State.Idle;
                }
                break;
        }
    }

    private bool Move(Vector3 targetPosition, float deltaDistance)
    {
        var distance = targetPosition - transform.position;
        var direction = distance.normalized;

        targetPosition -= direction * deltaDistance;
        distance = targetPosition - transform.position;

        transform.rotation = Quaternion.LookRotation(direction);

        var step = direction * Speed;
        if (step.magnitude >= distance.magnitude)
        {
            transform.position = targetPosition;
            return true;
        }

        transform.position += step;
        return false;
    }

    public void Init()
    {
        _state = State.Idle;
        _target = null;
    }

    public void Attack(GameObject target)
    {
        if (target == null)
        {
            Debug.Log("Cannot attack null object");
            return;
        }
        var c = target.GetComponentInChildren<Character>();
        if (c == null)
        {
            Debug.Log("Cannot attack object without Character component");
            return;
        }
        _state = State.MoveForward;
        _target = target;
        _targetPosition = _target.transform.position;
        _targetRadius = c.Radius;
    }
}
