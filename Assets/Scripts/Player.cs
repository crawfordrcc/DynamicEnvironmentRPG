using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MovingObject {
	public static Player instance = null;
	private Animator animator;
	public bool enemyEncountered = false;
	public bool attacking = false;
	public GameManager gameScript;
	public int encounter = 100;
	public int maxHealth;
	public int health;
	public int strength;
	public int vitality;
	public int level;
	public int exp;
	public int expToLevel;
	private SpriteRenderer sprite;
	private float height;

	public Inventory inventory;

	public void Awake(){
		if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator>();
		level = 1;
		exp = 0;
		expToLevel = 10;
		sprite = GetComponent<SpriteRenderer>();
		height = ((RectTransform)transform).rect.height;
		
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameScript.paused){
			animator.SetBool("playerMoving", false);
		}
		else{
			if(isMoving){
				sprite.sortingOrder = (int)Mathf.Round(-100*(transform.position.y-height/2));
			}
			//Debug.Log(gameScript.sceneIndex);
			if(finishingMove && gameScript.encounterRates[gameScript.sceneIndex] != 0){
				sprite.sortingOrder = (int)Mathf.Round(-100*(transform.position.y-height/2));
				encounter = Random.Range(0, 100);
				finishingMove = false;
			}
			
			if(!isMoving && !enemyEncountered){
				int x = 0;
				int y = 0;

				x = (int)Input.GetAxisRaw("Horizontal");
				y = (int)Input.GetAxisRaw("Vertical");

				if (x != 0)
					y = 0;
				if (x != 0 || y!= 0){
					isMoving = true;
					AttemptMove<Player>(x, y);
				}
				else{
					animator.SetBool("playerMoving", false);
				}
					
			}

			if(enemyEncountered){
				animator.SetBool("playerMoving", false);
			}
		}
	}

	protected override void AttemptMove<T>(int x, int y){
		animator.SetBool("playerMoving", true);
		base.AttemptMove<T>(x, y);
	}

	protected override void OnCantMove<T>(T component){
		animator.SetBool("playerMoving", false);
	}

	public void Attack(Enemy enemy){
		enemy.health -= strength*2 - enemy.vitality/5;
		attacking = true;
		animator.SetBool("playerAttacking", true);
	}
	public void AttackFinished(){
		animator.SetBool("playerAttacking", false);
		attacking = false;
	}

	public override void StopMovement(){
		base.StopMovement();
	}

	public void LevelUp(){
		if(level % 4 != 0)
			++strength;
		if(level % 3 != 0)
			++vitality;
		maxHealth += 10 + level/2;
		health = maxHealth;
	}
}
