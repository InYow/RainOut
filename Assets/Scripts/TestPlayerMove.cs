using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    public CubeMove cubeMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movedic = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movedic += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movedic += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movedic += Vector2.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movedic += Vector2.up;
        }
        cubeMove.direction = movedic;
    }
}
