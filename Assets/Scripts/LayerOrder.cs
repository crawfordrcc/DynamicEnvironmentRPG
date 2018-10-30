using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		float height = ((RectTransform)transform).rect.height;
		float y = ((RectTransform)transform).anchoredPosition.y;
		sprite.sortingOrder = (int)Mathf.Round(-100*(y-height/2));
	}
}
