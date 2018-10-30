using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSys : MonoBehaviour {
	public static EventSys instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
}
