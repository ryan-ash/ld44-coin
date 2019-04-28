using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameScreen : ScreenBaseController
{
    private static GameScreen instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void ChoiceButtonClick(int number)
    {
        Debug.Log("Choice number " + number);
    }
}