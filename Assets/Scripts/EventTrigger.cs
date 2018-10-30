using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EventTrigger : MonoBehaviour {
	public bool finished;

	public abstract void StartEvent(GameObject textPanel, GameObject popupPanel, Text messageText, GameManager gameManager);
}
