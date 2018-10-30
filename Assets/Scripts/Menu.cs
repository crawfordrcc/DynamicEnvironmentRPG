using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public static Menu instance = null;
	public Button inventoryButton;
	public Button statusButton;
	public Button equipmentButton;
	public Button saveButton;
	public Button optionsButton;
	public Button backButton;
	public ItemButton itemButton;
	//public Button[] itemButtons;
	public Inventory inventory;
	public bool opened;
	public bool inventoryOpen;
	public List<ItemButton> itemButtons;
	public Player player;

	public GameObject menuPanel;
	public GameObject itemPanel;
	public GameObject scrollContent;
	public GameObject dropdownLayer;
	public PopupPanel PopupPanel;
	public PopupPanel window;

	public int currentTab = 0;
	public int currentItem = 0;

	public void Awake(){
		if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	
	public void Start(){
		opened = false;
		inventoryOpen = false;
		inventoryButton.onClick.AddListener(() => InventoryClicked());
		backButton.onClick.AddListener(() => backClicked());
		itemButtons = new List<ItemButton>();
	}

	public void Update(){
		if(currentTab > 0){
			if(Input.GetButtonDown("Cancel")){
				switch(currentTab){
					case(1): 
						CloseInventory();
						break;
				}
				if(currentTab < 7)
					currentTab = 0;
				if(currentTab == 8)
					PopupPanel.cancelButton.Select();
			}
		}
	}

	public void InventoryClicked(){
		/*inventoryButton.gameObject.SetActive(false);
		statusButton.gameObject.SetActive(false);
		equipmentButton.gameObject.SetActive(false);
		saveButton.gameObject.SetActive(false);
		optionsButton.gameObject.SetActive(false);
		*/
		if(currentTab == 1)
			return;
		Debug.Log(inventory.items.Count);
		
		//int x_offset = 0;
		//int y_offset = 0;
		int i = 0;
		foreach(KeyValuePair<string, Item> entry in inventory.items){
			Item item = entry.Value;
			ItemButton button = Instantiate(itemButton, Vector3.zero, Quaternion.identity);
			button.SetItem(item);
			button.SetPlayer(player);
			button.panel = window;
			//button.SetDropdownParent(dropdownLayer.transform);
			//Button itemButton;
			button.transform.SetParent(scrollContent.transform, false);
			/*RectTransform buttonRT = (RectTransform)button.transform;
			RectTransform itemPanelRT = (RectTransform)itemPanel.transform;
			float w = buttonRT.rect.width;
			float h = buttonRT.rect.height;
			float left = itemPanelRT.rect.xMin + x_offset * w;
			float top = itemPanelRT.rect.yMax - y_offset * h;
			Debug.Log(left);
			Debug.Log(top);
			buttonRT.offsetMin = new Vector2(left, top-h);
			buttonRT.offsetMax = new Vector2(left+w, top);*/
			button.itemName.text = entry.Key + " x" + item.count;
			/*if(x_offset == 0){
				x_offset = 1;
			}
			else{
				x_offset = 0;
				++y_offset;
			}*/
			//itemButtons[i].gameObject.SetActive(true);
			itemButton.menu = this;
			itemButton.index = i;
			itemButtons.Add(button);
			++i;
		}
		itemPanel.gameObject.SetActive(true);
		backButton.gameObject.SetActive(true);
		itemButtons[0].button.Select();
		currentTab = 1;
	}

	public void backClicked(){
		itemPanel.gameObject.SetActive(false);
		backButton.gameObject.SetActive(false);
		/*inventoryButton.gameObject.SetActive(true);
		statusButton.gameObject.SetActive(true);
		equipmentButton.gameObject.SetActive(true);
		saveButton.gameObject.SetActive(true);
		optionsButton.gameObject.SetActive(true);*/
		currentTab = 0;
		destroyList();
	}

	public void CloseInventory(){
		itemPanel.gameObject.SetActive(false);
		backButton.gameObject.SetActive(false);
		destroyList();
		currentItem = 0;
	}

	public void Open(){
		inventoryButton.gameObject.SetActive(true);
		statusButton.gameObject.SetActive(true);
		equipmentButton.gameObject.SetActive(true);
		saveButton.gameObject.SetActive(true);
		optionsButton.gameObject.SetActive(true);
		menuPanel.gameObject.SetActive(true);
		inventoryButton.Select();
		opened = true;
	}

	public void Close(){
		//inventoryButton.gameObject.SetActive(false);
		//statusButton.gameObject.SetActive(false);
		//equipmentButton.gameObject.SetActive(false);
		//saveButton.gameObject.SetActive(false);
		//optionsButton.gameObject.SetActive(false);
		menuPanel.gameObject.SetActive(false);
		itemPanel.gameObject.SetActive(false);
		opened = false;
		currentTab = 0;
		destroyList();
	}

	private void destroyList(){
		while(itemButtons.Count != 0){
			ItemButton ib = itemButtons[0];
			itemButtons.RemoveAt(0);
			Destroy(ib.gameObject);
		}

	}
}
