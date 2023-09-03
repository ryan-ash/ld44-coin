using UnityEngine;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager instance;

    public LeanTweenType globalTweenConfig;

    [Header("Intro")]
    public float introAnimationCycleTime = 1f;
    public float introAnimationMinAlpha = 0.5f;

    [Header("Story")]
    public float dialogAnimationTime = 0.25f;

    private void Awake() {
        instance = this;
    }
}
