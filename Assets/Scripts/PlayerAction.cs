using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

	public GameObject curObject = null;
	public InteractiveObject curInterObject = null;
	public Text messageText;
	public GameObject textPanel;
	public GameObject popupPanel;
	public GameManager game;

	void Update () {
		if(Input.GetButtonDown("Submit") && curObject){
			curInterObject.Interact(gameObject.GetComponent<Player>(), messageText, textPanel);
		}
		
		if(Input.GetButtonDown("Fire3")){
			game.ToggleMenu();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("InteractiveObject")){
			curObject = other.gameObject;
			curInterObject = curObject.GetComponent<InteractiveObject>();
		}
		if(other.CompareTag("EventTrigger")){
			game.paused = true;
			other.gameObject.GetComponent<EventTrigger>().StartEvent(textPanel, popupPanel, messageText, game);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("InteractiveObject")){
			if(other.gameObject == curObject){
				if(curInterObject.talking){
					curInterObject.talking = false;
					curInterObject.messageIndex = 0;
					textPanel.SetActive(false);
				}
				curObject = null;
			}
		}
		if(other.CompareTag("EventTrigger")){
			gameObject.GetComponent<Player>().enabled = true;
		}
	}

}
