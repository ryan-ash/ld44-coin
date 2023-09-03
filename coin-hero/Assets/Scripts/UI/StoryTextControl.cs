using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryTextControl : MonoBehaviour
{
    public List<string> storyboard;
    private int currentTextIndex = 0;
    private GameObject forHide;

    public GameObject storyTextTemplate;
    private GameObject currentNewText;

    [HideInInspector]
    public bool ready = false;

    public StoryManager storyManager;
    
    public void ClearStoryBoard() {
        currentTextIndex = 0;
        storyboard.Clear();
    }

    public void AddText(string text) {
        storyboard.Add(text);
    }

    public void Show(string text) {
        if (forHide) {
            HideCurrent();
        }

        GameObject newText = Instantiate(storyTextTemplate) as GameObject;
        newText.transform.SetParent(transform, false);
        newText.GetComponent<Text>().text = text;

        if (forHide) {
            float initialY = -1 * forHide.GetComponent<RectTransform>().sizeDelta.y;
            UpdateTextY(newText, initialY);
            LeanTween.value(gameObject, 0f, 1f, SettingsManager.instance.dialogAnimationTime).setOnUpdate(
                (float value) => {
                    UpdateTextY(newText, Mathf.Lerp(initialY, 0, value));
                }
            ).setEase(SettingsManager.instance.globalTweenConfig);
        } else {
            forHide = newText;            
        }

        currentNewText = newText;
    }

    public void HideCurrent() {
        float finalY = forHide.GetComponent<RectTransform>().sizeDelta.y;
        LeanTween.value(gameObject, 0f, 1f, SettingsManager.instance.dialogAnimationTime).setOnUpdate(
            (float value) => {
                UpdateTextY(forHide, Mathf.Lerp(0, finalY, value));
            }
        ).setOnComplete(
            () => {
                Destroy(forHide);
                forHide = currentNewText;
            }
        ).setEase(SettingsManager.instance.globalTweenConfig);
    }

    private void UpdateTextY(GameObject targetText, float y) {
        Vector3 newTextPosition = targetText.transform.localPosition;
        newTextPosition.y = y;
        targetText.transform.localPosition = newTextPosition;
    }

    void Update() {
        if (ready && Input.anyKeyDown) {
            NextText();
        }
    }

    public void NextText() {
        if (currentTextIndex >= storyboard.Count) {
            return;
        }

        Show(storyboard[currentTextIndex]);
        currentTextIndex += 1;

        if (currentTextIndex >= storyboard.Count) {
            storyManager.DisplayButtons();
        }
    }
}
