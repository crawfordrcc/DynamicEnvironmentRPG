using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour {

	public Vector3 pos;
	public Vector3 scale;
	public string sceneName;
	private GameManager gm;

	void Start(){
		gm = (GameManager)FindObjectOfType(typeof(GameManager));
	}

	void OnTriggerEnter2D(Collider2D player){
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
		//boxCollider.enabled = false;
		Player p = player.GetComponent<Player>();
		p.StopMovement();
		p.StopAllCoroutines();
		if(sceneName == "")
			gm.ChangePositionTransition(pos, scale);
		else
			gm.ChangePositionTransition(pos, scale, sceneName);
		Debug.Log("entered");
	}
}
