using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    private Character _character;

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponentInParent<Character>();
    }

    void AttackComplete()
    {
        if (_character != null) {
            _character.AttackComplete();
        }
    }
}
