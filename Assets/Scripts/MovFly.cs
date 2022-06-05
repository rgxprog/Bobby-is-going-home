using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovFly : MonoBehaviour
{
    //-------------------------------------------

    public List<Transform> PointsToFly;
    public float speed = 1f;

    private Transform nextPointToFly;
    private Vector3 startPosition, moveDirection;
    private float originalDistance;

    //-------------------------------------------

    private void Awake()
    {
        SetPointToFly();
    }

    //-------------------------------------------

    private void Update()
    {
        transform.position += speed * Time.deltaTime * moveDirection;
        if (Vector2.Distance(startPosition, transform.position) >= originalDistance)
        {
            SetPointToFly();
        }
    }

    //-------------------------------------------

    private void SetPointToFly()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        nextPointToFly = PointsToFly[Random.Range(0, PointsToFly.Count)];

        if (startPosition == nextPointToFly.transform.position)
            SetPointToFly();

        moveDirection = (nextPointToFly.position - transform.position).normalized;
        originalDistance = Vector2.Distance(transform.position, nextPointToFly.position);
    }

    //-------------------------------------------
}
