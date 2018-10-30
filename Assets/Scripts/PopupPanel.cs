using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour {
	public GameObject panel;
	public Text descriptionText;
	public Button confirmButton;
	public Text confirmText;
	public Text cancelText;
	public Button cancelButton;
	public Menu menu;
	private int type;
	private const int CONSUMABLE = 0;
	private const int CONFIRMATION = 1;
	private const int YESNO = 2;
	private bool multipleConfirms;
	private Func<bool> operation;

	void Start () {
		confirmButton.onClick.AddListener(Confirm);
		cancelButton.onClick.AddListener(Cancel);
	}
	
	public void SetType(int t){
		type = t;
		if(type == CONSUMABLE){
			confirmText.text = "Use";
			cancelText.text = "Cancel";
		}
		else if(type == CONFIRMATION){
			confirmText.text = "Confirm";
			cancelText.text = "Cancel";
		}
		else if(type == YESNO){
			confirmText.text = "Yes";
			cancelText.text = "No";
		}
	}

	public void Setup(string description, Func<bool> f, bool multiple = false){
		descriptionText.text = description;
		multipleConfirms = multiple;
		operation = f;
		confirmButton.interactable = true;
	}

	public void Confirm(){
		bool interactable = operation();
		if(!multipleConfirms){
			if(menu.inventoryOpen){
				if(menu.itemButtons.Count != 1){
					if(menu.currentItem == 0)
						menu.itemButtons[1].button.Select();
					else
						menu.itemButtons[++menu.currentItem].button.Select();
				}
			}
			panel.SetActive(false);
		}
		if(!interactable)
			confirmButton.interactable = false;
	}

	public void SelectItemButton(){
		menu.itemButtons[menu.currentItem].button.Select();
	}

	public void DisableConfirm(){
		confirmButton.interactable = false;
	}

	private void Cancel(){
		if(menu.inventoryOpen){
			SelectItemButton();
		}
		panel.SetActive(false);
	}
}
