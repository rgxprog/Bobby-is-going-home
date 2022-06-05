using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    //-------------------------------------------

    public string sceneName;
    public char playerSide;

    //-------------------------------------------

    private void Start()
    {
        if (sceneName == string.Empty)
            gameObject.SetActive(false);
    }

    //-------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.ResetPosX(playerSide);
            GameManager.instance.ChangeScene(sceneName);
        }
    }

    //-------------------------------------------
}
