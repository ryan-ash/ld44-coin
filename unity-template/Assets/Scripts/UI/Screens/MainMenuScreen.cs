﻿using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : ScreenBaseController {

    private static MainMenuScreen instance;

    public RectTransform icon;
    public CanvasGroup text;

    private bool introAnimationOver = false;
    private bool animationReversed = false;

    void Start() {
        instance = this;
    }

    public void OnEnable() {
        introAnimationOver = false;
        animationReversed = false;
        RunIntroAnimation();
    }

    private void RunIntroAnimation() {
        if (introAnimationOver) {
            DoContinue();
            return;
        }
        LeanTween.value(gameObject, 0f, 1f, SettingsManager.instance.introAnimationCycleTime).setOnUpdate(
            (float value) => {
                var xScaleAlpha = (animationReversed) ? 1f - value : value;
                var xScale = Mathf.Lerp(1f, -1f, xScaleAlpha);
                var textOpacity = SettingsManager.instance.introAnimationMinAlpha + Mathf.Abs(value - 0.5f);
                icon.localScale = new Vector3(xScale, 1f, 1f);
                text.alpha = textOpacity;
            }
        ).setOnComplete(
            () => {
                animationReversed = !animationReversed;
                RunIntroAnimation();
            }
        ).setEase(SettingsManager.instance.globalTweenConfig);
    }

    public void OnContinue() {
        introAnimationOver = true;
    }

    public void DoContinue() {
        GameManager.ChangeGameStateTo(GameState.InProgress);
    }
}
