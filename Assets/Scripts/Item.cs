using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	public enum itemTypes{
		HEALTH,
		STRENGTH,
		VITALITY
	};

	public bool useInField;
	public bool useInBattle;
	public bool targetSelf;
	public bool isAOE;
	
	public itemTypes itemType;
	public int value;
	public int count;

	public string name;
	public string description;

	public Item(){}

	public Item(string n, itemTypes type, int v, int c, string d, bool uif=true, bool uib=true, bool ts=true, bool aoe=false){
		name = n;
		itemType = type;
		value = v;
		count = c;
		description = d;
		useInField = uif;
		useInBattle = uib;
		targetSelf = ts;
		isAOE = aoe;
	}

	public void use(Player player){
		switch(itemType){
			case(itemTypes.HEALTH):
				player.health += value;
				if(player.health > player.maxHealth)
					player.health = player.maxHealth;
				break;
			case(itemTypes.STRENGTH):
				player.strength += value;
				break;
			case(itemTypes.VITALITY):
				player.vitality += value;
				break;
		}
		--count;
	}

	public void use(Enemy enemy){
		switch(itemType){
			case(itemTypes.HEALTH):
				enemy.health -= value;
				break;
			case(itemTypes.STRENGTH):
				enemy.strength -= value;
				break;
			case(itemTypes.VITALITY):
				enemy.vitality -= value;
				break;
		}
		--count;
	}

	public bool Usable(Player player){
		if(!targetSelf)
			return false;
		if(!useInField)
			return false;
		if(itemType == itemTypes.HEALTH && player.health == player.maxHealth)
			return false;
		return true;
	}
}
