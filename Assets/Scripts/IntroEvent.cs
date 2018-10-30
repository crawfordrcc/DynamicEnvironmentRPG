using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IntroEvent : EventTrigger {
	public NPC npc;
	private Camera camera;
	private CameraController controller;
	private string[] messages = { "Hey Kagatsuchi, welcome back! I know you just got back from clearing out some monsters, but I have a request.",
		"There have been sightings of a large creature wearing a crown near the outskirts of town.",
		"The blob monsters have been strangely coordinated lately and we believe that this crowned monster is the cause.",
		"It would be a great help if you could defeat it and bring back the crown so we know it has been taken care of.",
		"Would you mind taking care of the crowned monster for me?",
		"Awesome! You are a great help. Feel free to relax in town for a bit before you go.",
		"Well... alright. We could really use your help though...",
		"Let me know if you change your mind!"
	};
	private GameObject textPanel;
	private GameObject popupPanel;
	private Text messageText;
	private PopupPanel window;
	private int i;
	private bool setupFinished = false;
	GameManager game;

	public void Update(){
		if(setupFinished){
			if(!finished){
				if(!popupPanel.activeSelf && i == 5)
					messageText.text = messages[++i];
				if(Input.GetKeyDown("space") && !popupPanel.activeSelf){
					if(i == -1 || i == 7)
						finished = true;
					else if(i+1 < 5 || i == 6)
						messageText.text = messages[++i];
					else if(i++ == 4){
						popupPanel.SetActive(true);
					}
				}
			}
			else{
				textPanel.SetActive(false);
				controller.ResetCamera();
				game.paused = false;
				this.gameObject.SetActive(false);
			}
		}
	}

	public override void StartEvent(GameObject tPanel, GameObject pPanel, Text mText, GameManager gameManager){
		finished = false;
		camera = Camera.main;
		controller = camera.gameObject.GetComponent<CameraController>();
		controller.CenterCameraOnObject(npc.transform, new Vector3(0, -2f, 0));
		textPanel = tPanel;
		popupPanel = pPanel;
		messageText = mText;
		messageText.text = messages[0];
		textPanel.SetActive(true);
		messageText.gameObject.SetActive(true);
		window = popupPanel.GetComponent<PopupPanel>();
		i = 0;
		window.SetType(2);
		Func<bool> f = () => {messageText.text = messages[i]; i = -1; return false;};
		window.Setup("Take care of the crowned monster?", f, false);
		game = gameManager;
		setupFinished = true;
		/*while(popupPanel.activeSelf);
		if(i == 6){
			messageText.text = messages[i];
		}
		else{
			messageText.text = messages[7];
			while(!Input.GetKeyDown("space"));
			messageText.text = messages[8];	
		}
		while(!Input.GetKeyDown("space"));
		textPanel.SetActive(false);
		controller.ResetCamera();*/
	}
}
