using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {

	public Button button;
	public Text itemName;
	//public Button useButton;
	//public Button discardButton;
	public Item item;
	public Player player;
	public PopupPanel panel;
	public Menu menu;
	public int index;
	//public GameObject itemLayer;
	//public GameObject dropdownLayer;
	

	// Use this for initialization
	void Start () {
		/*RectTransform buttonRT = (RectTransform)button.transform;
		RectTransform useButtonRT = (RectTransform)useButton.transform;
		RectTransform discardButtonRT = (RectTransform)discardButton.transform;
		float w = buttonRT.rect.width;
		float h = buttonRT.rect.height;
		float w2 = useButtonRT.rect.width;
		float h2 = useButtonRT.rect.height;
		float left = buttonRT.rect.xMin + w;
		float top = buttonRT.rect.yMax;
		useButtonRT.offsetMin = new Vector2(left, top-h2);
		discardButtonRT.offsetMin = new Vector2(left, top-2*h2);
		useButtonRT.offsetMax = new Vector2(left+w2, top);
		discardButtonRT.offsetMax = new Vector2(left+w2, top-h2);*/
		//useButton.gameObject.SetActive(false);
		//discardButton.gameObject.SetActive(false);
		button.onClick.AddListener(ItemClicked);
		//useButton.onClick.AddListener(UseItem);
		//itemLayer = GameObject.FindGameObjectWithTag("ItemLayer");
		//dropdownLayer = GameObject.FindGameObjectWithTag("DropDownLayer");
		//open = false;
	}
	
	public void SetDropdownParent(Transform layer){
		//useButton.transform.SetParent(layer, false);
		//discardButton.transform.SetParent(layer, false);
	}

	public void SetItem(Item i){
		item = i;
	}
	
	public void SetPlayer(Player p){
		player = p;
	}

	void ItemClicked(){
		/*if(!open){
			useButton.gameObject.SetActive(true);
			discardButton.gameObject.SetActive(true);
			open = true;
		}
		else{
			useButton.gameObject.SetActive(false);
			discardButton.gameObject.SetActive(false);
			open = false;
		}*/
		panel.SetType(0);
		panel.Setup(item.description, UseItem, true);
		if(!item.Usable(player)){
			panel.DisableConfirm();
		}
		panel.gameObject.SetActive(true);
		if(panel.confirmButton.IsInteractable())
			panel.confirmButton.Select();
		else
			panel.cancelButton.Select();
		menu.currentTab = 8;
		menu.currentItem = index;
	}

	bool UseItem(){
		item.use(player);
		itemName.text = item.name + " x" + item.count;
		if(item.count == 0)
			return false;
		return true;
	}
}
