﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle, MoveForward, Attack, WaitAttackCompelete, MoveBackward, Dead, WaitDead
    }

    public enum WeaponType
    {
        Gun, Bat, Hand
    }

    public float Speed;
    public float Radius;
    public WeaponType Weapon;

    private State _state = State.Idle;
    private Animator _animator;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private GameObject _target;
    private Character _targetCharacter;
    private Vector3 _targetPosition;

    public bool IsDead { get { return _state == State.Dead || _state == State.WaitDead; } }

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        _animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        switch (_state)
        {
            case State.Idle:
                _animator.SetFloat("speed", 0);
                transform.rotation = originalRotation;
                _animator.ResetTrigger("ressurect");
                break;
            case State.MoveForward:
                _animator.SetFloat("speed", Speed);
                if (Move(_targetPosition, _targetCharacter.Radius))
                {
                    _state = State.Attack;
                }
                break;
            case State.Attack:
                _animator.SetFloat("speed", 0);
                if (Weapon == WeaponType.Gun)
                {
                    _animator.SetTrigger("shoot");
                }
                else if (Weapon == WeaponType.Bat)
                {
                    _animator.SetTrigger("attackBat");
                }
                else
                {
                    _animator.SetTrigger("attackHand");
                }
                _state = State.WaitAttackCompelete;
                break;
            case State.WaitAttackCompelete:
                _animator.SetFloat("speed", 0);
                break;
            case State.MoveBackward:
                _animator.SetFloat("speed", Speed);
                if (Move(originalPosition, 0))
                {
                    _state = State.Idle;
                }
                break;
            case State.Dead:
                _animator.SetFloat("speed", 0);
                _animator.SetTrigger("death");
                _state = State.WaitDead;
                break;
            case State.WaitDead:
                _animator.SetFloat("speed", 0);
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
        _animator.ResetTrigger("death");
        _animator.ResetTrigger("shoot");
        _animator.ResetTrigger("attackBat");
        _animator.ResetTrigger("attackHand");
        _animator.SetTrigger("ressurect");
    }

    public void Attack(GameObject target)
    {
        if (IsDead)
        {
            Debug.Log("Character is dead");
            return;
        }
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
        if (c.IsDead)
        {
            Debug.Log("Cannot attack already died enemy");
            return;
        }
        _target = target;
        _targetPosition = _target.transform.position;
        _targetCharacter = c;

        if (Weapon == WeaponType.Gun)
        {
            _state = State.Attack;
        }
        else
        {
            _state = State.MoveForward;
        }
    }

    public void AttackComplete()
    {
        if (_state == State.WaitAttackCompelete)
        {
            if (Weapon == WeaponType.Gun)
            {
                _state = State.Idle;
            }
            else
            {
                _state = State.MoveBackward;
            }
        }
        else
        {
            Debug.Log("AttackComplete in wrong state");
        }
    }

    public void AttackDoDamage()
    {
        if (_state == State.WaitAttackCompelete)
        {
            _targetCharacter.MakeDead();
        }
        else
        {
            Debug.Log("AttackDoDamage in wrong state");
        }
    }

    public void MakeDead()
    {
        if (_state == State.Idle)
        {
            _state = State.Dead;
        }
        else
        {
            Debug.Log("Dont try to touch all buttons at once");
        }
    }
}
