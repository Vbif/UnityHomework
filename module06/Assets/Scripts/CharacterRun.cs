using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterRun : MonoBehaviour
{
    public Transform[] path;
    public float Speed;
    public float Delay;

    private Vector3[] _pathPositions;
    private int _pathPositionsIndex = 0;
    private float _delayTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (path.Length > 0) {

            _pathPositions = path.Select(a => a.position).ToArray();
            transform.position = _pathPositions[0];

            var animator = GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetFloat("speed", 1);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_delayTimer < Delay)
        {
            _delayTimer += Time.fixedDeltaTime;
            return;
        }

        if (Move(_pathPositions[_pathPositionsIndex]))
        {
            _pathPositionsIndex++;
            if (_pathPositionsIndex >= _pathPositions.Length)
            {
                _pathPositionsIndex = 0;
            }
        }
    }

    private bool Move(Vector3 targetPosition)
    {
        var distance = targetPosition - transform.position;
        var direction = distance.normalized;

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
}
