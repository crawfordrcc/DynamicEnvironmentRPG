using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractiveObject : MovingObject {
	private Animator animator;
	public bool talks;
	public bool talking = false;
	public bool treasure;
	public Item item;
	public string[] message;
	public int messageIndex = 0;

	public void Start(){
		if(treasure){
			message[0] = "You received ";
			if(item.count == 1)
				message[0] += "a ";
			else
				message[0] += item.count;
			message[0] += item.name + "!";	
		}
	}

	protected override void OnCantMove<T>(T component){
	}
	
	public void Interact(Player player, Text messageText, GameObject textPanel){
		if(talks){
			if(talking){
				if(++messageIndex < message.Length)
					messageText.text = message[messageIndex];
				else{
					talking = false;
					messageIndex = 0;
					messageText.gameObject.SetActive(false);
					textPanel.SetActive(false);
				}
			}
			else{
				textPanel.SetActive(true);
				messageText.gameObject.SetActive(true);
				messageText.text = message[messageIndex];
				talking = true;
			}
		}

		if(treasure){
			player.inventory.Add(item);
			messageText.gameObject.SetActive(false);
			gameObject.SetActive(false);
		}
	}
}
