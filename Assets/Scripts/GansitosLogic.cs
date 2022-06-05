using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GansitosLogic : MonoBehaviour
{
    //-------------------------------------------

    public List<Transform> gansitos = new List<Transform>();

    private List<Vector3> startPositions = new List<Vector3>();

    //-------------------------------------------

    private void Awake()
    {
        foreach (Transform gansito in gansitos)
        {
            startPositions.Add(gansito.position);
        }
    }

    //-------------------------------------------

    public void ResetPositions()
    {
        for (int i = 0; i < gansitos.Count; i++)
        {
            gansitos[i].position = startPositions[i];
        }
    }

    //-------------------------------------------
}
