using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
	public GameObject panelTutorial;
	public Button buttonBack;
	public Button buttonNext;

	public Image img;
	public TextMeshProUGUI textDialogue;

	public Sprite[] images;

	[TextArea]
	public string[] dialogues;

	int index = 0;

	void Start() {
		buttonBack.interactable = false;
		LoadDialogue();

	}

	public void Back() {
		if (index > 0) {
			index--;
			LoadDialogue();

			if (index == 0) {
				buttonBack.interactable = false;
			}
		}
		buttonNext.interactable = true;
	}

	public void Next() {
		if (index < dialogues.Length - 1) {
			{
				index++;
				LoadDialogue();
				if (index == dialogues.Length - 1) {
					buttonNext.interactable = false;
				}
			}
			buttonBack.interactable = true;
		}

	}
	public void LoadDialogue() {
		img.sprite = images[index];
		textDialogue.text = dialogues[index];
	}

	public void CloseTutorial() {
		panelTutorial.SetActive(false);
	}

	public void OpenTutorial() {
		panelTutorial.SetActive(true);
	}
}
