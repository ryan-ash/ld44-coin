using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ink.Runtime;

public class InkNarratorService {// : MonoBehaviour {
	
	public InkNarratorService(string text) {
		story = new Story (text);
		actionsDictionary = new Dictionary<Text, UnityAction> ();
	}

	public string getNextStoryLine() {
			if (story.canContinue) {
			string text = story.Continue ();
			text = text.Trim();

		foreach (var tag in story.currentTags) {
			Debug.Log("Tag: ");
			Debug.Log(tag);
		}

			return text;
		}

		return "";
	}

	public bool isAnyChoiceAvailable () {
		return story.currentChoices.Count > 0;
	}

	public bool isNextStoryLineAvailable() {
		return story.canContinue;
	}
	 public List<ChoiceInfo> getChoices () {
	
		List<ChoiceInfo> choices = new List<ChoiceInfo>();
		if(isAnyChoiceAvailable()) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				choices.Add(new ChoiceInfo(choice.text.Trim(), i));
			}
		}
		return choices;
	}

	 public void chooseChoiceIndex(int index) {
		story.ChooseChoiceIndex (index);
	}

	private Story story;

	private Dictionary<Text, UnityAction> actionsDictionary;

}