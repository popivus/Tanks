using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesOnMap = 1, killsCount = 0;

    public static GameManager instanse;

    public void Awake()
    {
        instanse = this;
    }

    void Update()
    {
        if (killsCount == enemiesOnMap)
        {
            UIController.instanse.FinishLevel();
        }
    }
}
