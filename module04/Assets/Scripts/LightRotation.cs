using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float XMax;
    public float YMax;
    public float ZMax;
    public float XShift;
    public float YShift;
    public float ZShift;

    public float Speed;

    private Vector3 _rotation;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _rotation = transform.rotation.eulerAngles;
    }

    float RotateOne(float original, float max, float shift)
    {
        return original + Mathf.Sin(_timer + shift) * max;
    }

    // Update is called once per frame
    void Update()
    {
        float x = RotateOne(_rotation.x, XMax, XShift);
        float y = RotateOne(_rotation.y, YMax, YShift);
        float z = RotateOne(_rotation.z, ZMax, ZShift);

        transform.rotation = Quaternion.Euler(x, y, z);

        _timer += Time.deltaTime * Speed;
    }
}
