﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {
	public static Singleton instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
}
