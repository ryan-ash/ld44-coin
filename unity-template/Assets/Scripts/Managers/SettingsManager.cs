using UnityEngine;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager instance;

    public LeanTweenType globalTweenConfig;

    [Header("Intro")]
    public float introAnimationCycleTime = 1f;
    public float introAnimationMinAlpha = 0.5f;

    private void Awake() {
        instance = this;
    }
}
