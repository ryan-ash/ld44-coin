using UnityEngine;

public class GameOverScreen : ScreenBaseController {

    private static GameOverScreen instance;

    void Start() {
        instance = this;
    }

    public void OnEnable() {
            
    }

    public void OnRestart() {
        GameManager.ChangeGameStateTo(GameState.InProgress);
    }
}
