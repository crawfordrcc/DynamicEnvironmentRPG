using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public static Inventory instance = null;
	public Dictionary<string, Item> items;

	public void Awake(){
		if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	public void Start(){
		string d = "Heals 30 health.";
		Item potion = new Item("Potion", Item.itemTypes.HEALTH, 30, 5, d);
		items = new Dictionary<string, Item>();
		Add(potion);
		d = "Heals 60 health.";
		Item hipotion = new Item("Hi-Potion", Item.itemTypes.HEALTH, 60, 1, d);
		Add(hipotion);
		d = "Deals 10 damage.";
		Item rock = new Item("Rock", Item.itemTypes.HEALTH, 10, 1, d, false, true, false, false);
		Add(rock);

		
	}

	public void Add(Item item){
		Item temp = null;
		if(items.TryGetValue(item.name, out temp))
			if(temp != null)
				temp.count += item.count;
			else
				Debug.Log("Item add failed");
		else{
			items.Add(item.name, item);
			Debug.Log("New item added.");
		}
	}
}
