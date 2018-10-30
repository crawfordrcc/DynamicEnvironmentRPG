using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int maxHealth;
	public int health;
	public int strength;
	public int vitality;
	public int speed;
	public int exp;
	public float atbMax = 100f;
	public float atbMin = 0f;
	public bool clicked;
	public string displayName;

	private Animator animator;
	public bool attacking = false;

	public void Start(){
		animator = GetComponent<Animator>();
		clicked = false;
	}

	public void Attack(Player player){
		player.health -= strength*2 - player.vitality/5;
		attacking = true;
		animator.SetBool("attacking", true);
	}

	public void AttackFinished(){
		animator.SetBool("attacking", false);
		attacking = false;
	}

	public void OnMouseDown(){
		clicked = true;
	}
}
