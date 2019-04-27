using UnityEngine;

public class GameHUD : OverlayBaseController {

    private static GameHUD instance;

    void Start()
    {
        instance = this;
    }

    public void OnEnable() 
    {
            
    }
}
