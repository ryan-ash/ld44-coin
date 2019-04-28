using UnityEngine;

public class GameOverScreen : ScreenBaseController {

    private static GameOverScreen instance;
    
    void Awake()
    {
        instance = this;
    }

    public void OnEnable() {
            
    }

    public void OnRestart() {
        GameManager.ChangeGameStateTo(GameState.InProgress);
    }
}
