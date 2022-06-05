using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLogic : MonoBehaviour
{
    //-------------------------------------------

    public List<GameObject> bridgeSections;

    [Range(0f, 0.7f)]
    public float tick = 0.7f;
    
    private float timeToNext = 0;
    private bool reverse = true;
    private int currentSection;

    //-------------------------------------------

    private void Start()
    {
        currentSection = bridgeSections.Count - 1;
    }

    //-------------------------------------------

    private void Update()
    {
        timeToNext += Time.deltaTime;
        if (timeToNext > tick)
        {
            bridgeSections[currentSection].gameObject.SetActive(!reverse);
            currentSection += !reverse ? 1 : -1;

            if (currentSection < 0)
            {
                currentSection = 0;
                reverse = false;
            }
            else if (currentSection > bridgeSections.Count - 1)
            {
                currentSection = bridgeSections.Count - 1;
                reverse = true;
            }

            timeToNext = 0;
        }
    }


    //-------------------------------------------
}
