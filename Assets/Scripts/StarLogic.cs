using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLogic : MonoBehaviour
{
    //-------------------------------------------

    private float aliveTime;
    private SpriteRenderer spriteRenderer;

    //-------------------------------------------

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewPosition();
    }

    //-------------------------------------------

    private void SetNewPosition()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Random.Range(0.2f, 0.8f));
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(0.5f, 4.5f), transform.position.z);

        aliveTime = Random.Range(2f, 4f);
        Invoke(nameof(SetNewPosition), aliveTime);
    }

    //-------------------------------------------
}
