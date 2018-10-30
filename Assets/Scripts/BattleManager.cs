using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;

public class BattleManager : MonoBehaviour {

	public enum BattleStates{
		START,
		WAIT,
		PLAYERTURN,
		SELECTTARGET,
		PLAYERANIMATE,
		ENEMYTURN,
		ENEMYANIMATE,
		GAMEOVER,
		WIN
	}

	public Button attackButton;
	public Button itemButton;
	public Button skillButton;
	public Button fleeButton;
	public GameObject commandPanel;

	public GameObject atb;

	public BattleStates currentState;
	public Text[] textObjects;
	public GameObject textPanel;
	public GameObject battleObjects;
	//public CameraController camera;
	public Inventory Inventory;
	private Player player;
	public Enemy[] enemies;
	public Button[] enemyButtons;
	//private Enemy target;
	private RectTransform atb_transform;
	private int current;
	private int maxText;
	private int curTarget;
	//private bool filling = false;
	//private bool full = false;
	private float atbMax = 100f;
	private float atbCur = 0f;
	private char curCommand = 'A';
	public bool battleWon;
	private Color32 filling = new Color32(169, 168, 154, 255);
	private Color32 full = new Color32(206, 202, 44, 255);

	public void SetupBattle(Player p){
		battleWon = false;
		player = p;
		currentState = BattleStates.WAIT;
		curTarget = 0;
		//camera = c;
		//camera.BattleCameraStart();
		int i = 0;
		//int numEnemies = Random.Range(1, 3);
		//enemies = new Enemy[numEnemies];
		/* 
		int i = 0;
		while(i < numEnemies){
			int n = Random.Range(0, encounterableEnemies.Length);
			x = -3;
			if(i % 2 == 0)
				x -= 2;
			y = (i + 1) * (8/(numEnemies + 1));
			enemies[i] = Instantiate(encounterableEnemies[n], new Vector3(x, y, 0f), Quaternion.identity);
			enemies[i].health = enemies[i].maxHealth;
			++i;
		}
		target = enemies[0];*/
		current = 0;
		attackButton.onClick.AddListener(PlayerAttack);
		enemyButtons[0].onClick.AddListener(SelectTarget1);
		enemyButtons[1].onClick.AddListener(SelectTarget2);
		//textObjects = textPanel.GetComponents<Text>();
		commandPanel.gameObject.SetActive(false);
		textPanel.gameObject.SetActive(true);
		textObjects[0].text ="Kagatsuchi: " + player.health + "/" + player.maxHealth;
		textObjects[0].gameObject.SetActive(true);

		battleObjects.SetActive(true);
		maxText = 1 + enemies.Length;

		for(i = 1; i < maxText; ++i){
			enemies[i-1].health = enemies[i-1].maxHealth;
			enemies[i-1].transform.position = new Vector3(-2f - 2*((i-1)%2), 3f - 1.5f*(i-1), 1);
			enemyButtons[i-1].interactable = true;
			textObjects[i].text = enemies[i-1].name + ": " + enemies[i-1].health + " / " + enemies[i-1].maxHealth;
			textObjects[i].gameObject.SetActive(true);
		}
		atb_transform = (RectTransform)atb.transform;

	}
	
	public void UpdateBattle(){
		//if(target == null && !player.attacking)
			//currentState = BattleStates.WIN;

		switch(currentState){
			case (BattleStates.START):
				break;
			case (BattleStates.WAIT):
				if(player.health <= 0)
					currentState = BattleStates.GAMEOVER;
				else{
					if(atbCur < atbMax){
						atbCur += 40*Time.deltaTime;
						atb_transform.offsetMax = new Vector2(atbCur, atb_transform.rect.yMax);
					}
					else{
						Debug.Log("Player's turn");
						Image image = atb.GetComponent<Image>();
						image.color = full;
						currentState = BattleStates.PLAYERTURN;
						commandPanel.gameObject.SetActive(true);
						attackButton.Select();
					}
				}
				break;
			case (BattleStates.PLAYERTURN):	
				if(player.health <= 0)
					currentState = BattleStates.GAMEOVER;
				else{
					if(Input.GetButtonDown("Submit")){
						if(curCommand == 'A'){
							currentState = BattleStates.SELECTTARGET;
							enemyButtons[curTarget].Select();
						}
					}
				}	
				break;
			case (BattleStates.SELECTTARGET):
				
				break;
			case (BattleStates.PLAYERANIMATE):
				if(!player.attacking){
					StartCoroutine(ChangeState(BattleStates.WAIT, SetState));
					/*if(target.health <= 0){
						target.gameObject.SetActive(false);
		 				/*if(current < enemies.Length - 1){
						++current;
						target = enemies[current];
						}
						for(int i = 0; i < enemies.Length; ++i){
							if(enemies[i].gameObject.activeSelf){
								target = enemies[i];	
								return;
							}
						}
						battleWon = true;
					}*/
				}
				break;
			case (BattleStates.ENEMYTURN):
				if(battleWon)
					StartCoroutine(ChangeState(BattleStates.WIN, SetState));
				else{
					currentState = BattleStates.ENEMYANIMATE;
					for(int i = 0; i < enemies.Length; ++i)
						if(enemies[i].gameObject.activeSelf && !enemies[i].attacking){
							enemies[i].Attack(player);
							textObjects[1].text ="Player: " + player.health + "/" + player.maxHealth;
					}
				}
				break;
			case (BattleStates.ENEMYANIMATE):
				if(!enemies[enemies.Length-1].attacking)
					currentState = BattleStates.PLAYERTURN;
				break;
			case (BattleStates.GAMEOVER):
				break;
			case (BattleStates.WIN):
				attackButton.gameObject.SetActive(false);
				for(int i = 0; i < enemies.Length; ++i)
					player.exp += enemies[i].exp;
				if(player.exp >= player.expToLevel){
					++player.level;
					player.expToLevel += player.expToLevel;
					player.LevelUp();
				}
				textPanel.gameObject.SetActive(false);
				for(int i = 1; i < maxText; ++i)
					textObjects[i].gameObject.SetActive(false);
				//camera.ResetCamera();
				break;
		}
	}

	void PlayerAttack(){
		curCommand = 'A';
		bool selected = false;
		int i = 0;
		battleObjects.SetActive(true);
		Debug.Log("Attack!");
		while(!selected){
			if(enemyButtons[i].interactable){
				enemyButtons[i].Select();
				selected = true;
			}
			i += 1;
		}
		//currentState = BattleStates.PLAYERANIMATE;
		//commandPanel.SetActive(false);
		//player.Attack(target);
	}

	void SelectTarget1(){
		battleObjects.SetActive(false);
		player.Attack(enemies[0]);
		if(enemies[0].health <= 0){
			enemies[0].gameObject.SetActive(false);
			enemyButtons[0].interactable = false;
		}
		commandPanel.SetActive(false);
		Image image = atb.GetComponent<Image>();
		image.color = filling;
		atb_transform.offsetMax = new Vector2(atb_transform.rect.xMin, atb_transform.rect.yMax);
		atbCur = 0;
		currentState = BattleStates.PLAYERANIMATE;
		bool alldead = true;
		for(int i = 0; i < enemyButtons.Length; ++i){
			if(enemyButtons[i].interactable)
				alldead = false;
		}
		if(alldead){
			currentState = BattleStates.WIN;
		}
	}

	void SelectTarget2(){
		battleObjects.SetActive(false);
		player.Attack(enemies[1]);
		if(enemies[1].health <= 0){
			enemies[1].gameObject.SetActive(false);
			enemyButtons[1].interactable = false;
		}
		commandPanel.SetActive(false);
		Image image = atb.GetComponent<Image>();
		image.color = filling;
		atb_transform.offsetMax = new Vector2(atb_transform.rect.xMin, atb_transform.rect.yMax);
		atbCur = 0;
		currentState = BattleStates.PLAYERANIMATE;
		bool alldead = true;
		for(int i = 0; i < enemyButtons.Length; ++i){
			if(enemyButtons[i].interactable)
				alldead = false;
		}
		if(alldead){
			currentState = BattleStates.WIN;
		}
	}

	void SelectTarget3(){

	}

	void SetState(BattleStates newState){
		currentState = newState;
	}
	IEnumerator ChangeState(BattleStates newState, Action<BattleStates> onComplete){
    	yield return new WaitForSeconds(0.5f);
    	onComplete(newState);
	}

}
