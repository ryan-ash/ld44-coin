using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryTextControl : MonoBehaviour
{
    public string[] texts;
    private int currentTextIndex = 0;
    private GameObject forHide;

    public GameObject storyTextTemplate;

    private void Awake()
    {

    }
    
    public void Show(string text)
    {
        if (forHide)
        {
            HideCurrent();
        }

        GameObject newText = Instantiate(storyTextTemplate) as GameObject;
        newText.transform.SetParent(transform, false);

        if (forHide)
        {
            float initialY = -1 * forHide.GetComponent<RectTransform>().sizeDelta.y;
            UpdateTextY(newText, initialY);
            LeanTween.value(gameObject, 0f, 1f, SettingsManager.instance.dialogAnimationTime).setOnUpdate(
                (float value) =>
                {
                    UpdateTextY(newText, Mathf.Lerp(initialY, 0, value));
                }
            ).setEase(SettingsManager.instance.globalTweenConfig);
        }

        forHide = newText;
    }

    public void HideCurrent()
    {
        float finalY = forHide.GetComponent<RectTransform>().sizeDelta.y;
        LeanTween.value(gameObject, 0f, 1f, SettingsManager.instance.dialogAnimationTime).setOnUpdate(
            (float value) =>
            {
                UpdateTextY(forHide, Mathf.Lerp(0, finalY, value));
            }
        ).setOnComplete(
            () =>
            {
                Destroy(forHide);
            }
        ).setEase(SettingsManager.instance.globalTweenConfig);
    }

    private void UpdateTextY(GameObject targetText, float y)
    {
        Vector3 newTextPosition = targetText.transform.localPosition;
        newTextPosition.y = y;
        targetText.transform.localPosition = newTextPosition;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            NextText();
        }
    }

    private void NextText()
    {
        if (currentTextIndex >= texts.Length)
            currentTextIndex = 0;

        Show(texts[currentTextIndex]);
        currentTextIndex += 1;
    }
}
