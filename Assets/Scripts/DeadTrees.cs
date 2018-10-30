using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrees : MonoBehaviour {

	// Use this for initialization
	public GameObject Trees;
	public Sprite spriteImage;
	void Awake () {
		/*foreach (Transform child in this.gameObject.transform){
			Debug.Log(child);
			SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
			sprite.sprite = spriteImage;
		}*/
		SpriteRenderer[] sprites = Trees.GetComponentsInChildren<SpriteRenderer>();
		Debug.Log(sprites.Length);
        foreach (SpriteRenderer sprite in sprites)
            sprite.sprite = spriteImage;
	}
}
