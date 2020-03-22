using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] path;
    public float speed;

    private float current;  // от 0.0 до 1.0
    private float speedInv = -1;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        GameLogic.Instance.OnInit += Init;
        Init();
    }

    public void Init()
    {
        current = 0.0f;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var start = GetPosition(index - 1);
        var end = GetPosition(index);

        if (speedInv < 0)
        {
            current = 0.0f;
            speedInv = (end - start).magnitude / speed;
        }

        current += Time.deltaTime / speedInv;
        if (current > 1.0f) {
            current = 1.0f;

            index = (index + 1) % path.Length;
            speedInv = -1;
        }

        transform.position = Vector3.Lerp(start, end, current);
    }

    private Vector3 GetPosition(int index)
    {
        if (path.Length == 0)
        {
            return new Vector3();
        }
        else if (path.Length == 1)
        {
            return path[0].position;
        }

        if (index < 0)
        {
            index = path.Length - 1;
        }
        else if (index >= path.Length)
        {
            index = 0;
        }

        return path[index].position;
    }
}
