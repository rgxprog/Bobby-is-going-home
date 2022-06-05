using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovHorizontal : MonoBehaviour
{
    //-------------------------------------------

    public float speed;
    public char direction = 'R';

    private int directionValue;
    private float sizeX;

    //-------------------------------------------

    private void Awake()
    {
        directionValue = direction == 'R' ? 1 : -1;
        sizeX = GetComponent<SpriteRenderer>().localBounds.size.x * transform.localScale.x;
    }

    //-------------------------------------------

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * directionValue * Time.deltaTime, transform.position.y, transform.position.z);
        CheckBoundaries();
    }

    //-------------------------------------------

    private void CheckBoundaries()
    {
        if (direction == 'R' && transform.position.x > 8f + sizeX/2)
        {
            transform.position = new Vector3(-8f - sizeX/2, transform.position.y, transform.position.z);
        }
        else if (direction == 'L' && transform.position.x < -8f - sizeX/2)
        {
            transform.position = new Vector3(8f + sizeX/2, transform.position.y, transform.position.z);
        }

    }

    //-------------------------------------------
}
