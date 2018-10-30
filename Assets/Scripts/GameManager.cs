using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
    public BoardManager boardScript;
	public BattleManager battleScript;
	public Player player;
	public Menu menu;
	public GameObject canvas;
	public GameObject fadePanel;
	public CameraController camera;

	private bool doingSetup = true;
	private bool battle = false;
	public bool paused = false;
	private Vector2 playerPosition;
	private Vector3 playerScale;
	public int[] encounterRates;
	private Vector3 battlePosition = new Vector3(5.00f, 1.25f, 0.0f);
	private Vector3 battleScale = new Vector3(1.5f, 1.5f, 1.0f);
	public int sceneIndex = 0;

	protected GameManager() { }

	// Use this for initialization
	void Awake () {
		if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
		//battleScript = GetComponent<BattleManager>();
	}
	
	// Update is called once per frame
	
	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		if(battle){
			camera.BattleCameraStart();
			InitBattle();
		}
		StartCoroutine(FadeIn());
		sceneIndex = scene.buildIndex;
		//paused = false;
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	//void OnDisable(){
	//	SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	//}
	
	void InitBattle(){
		//boardScript.Clear();
		//boardScript.LayoutBattleTiles();
		battleScript.SetupBattle(player);
	}

	void Update(){
		//Console.Write(player.encounter);
		//Console.Write(boardScript.currentMap.encounterRate);
		if(!battle){
			/*if(!player.isMoving){
				if(player.transform.position.x < -0.5){
					boardScript.moveLeft();
					player.transform.position = new Vector2(boardScript.currentMap.layout[0].row.Length, player.transform.position.y);
				}
				else if(player.transform.position.x > boardScript.currentMap.layout[0].row.Length - 0.5){
					boardScript.moveRight();
					player.transform.position = new Vector2(0, player.transform.position.y);
				}
				else if(player.transform.position.y < -0.5){
					boardScript.moveDown();
					player.transform.position = new Vector2(player.transform.position.x, boardScript.currentMap.layout.Length);
				}
				else if(player.transform.position.y > boardScript.currentMap.layout.Length - 0.5){
					boardScript.moveUp();
					player.transform.position = new Vector2(player.transform.position.x, 0);
				}
			}*/
			
			if(player.encounter < encounterRates[sceneIndex]){
				player.enemyEncountered = true;
				battle = true;
				playerPosition = player.transform.position;
				playerScale = player.transform.localScale;
				player.encounter = 100;
				player.StopAllCoroutines();
				player.StopMovement();
				Debug.Log("here");
				//paused = true;
				ChangePositionTransition(battlePosition, battleScale, "battle");
			}
		}
		else{
			battleScript.UpdateBattle();
			if(battleScript.currentState == BattleManager.BattleStates.WIN || battleScript.currentState == BattleManager.BattleStates.GAMEOVER){
				battle = false;
				player.encounter = 100;
				player.enemyEncountered = false;
				player.transform.position = playerPosition;
				player.transform.localScale = playerScale;
				//boardScript.Clear();
				//boardScript.LayoutTiles();
			}
		}
	}

	public void ChangePositionTransition(Vector3 position, Vector3 scale, string sceneName="none"){
		paused = true;
		canvas.SetActive(true);
		//Debug.Log(sceneName);
		StartCoroutine(FadeOut(position, scale, sceneName));
	}

	public IEnumerator FadeOut(Vector3 position, Vector3 scale, string sceneName="none"){
		CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		fadePanel.SetActive(true);
		while(canvasGroup.alpha < 1){
			canvasGroup.alpha += Time.deltaTime;
			yield return null;
		}
		if(player.transform.localScale.x < 0 && !battle)
			scale.x = -scale.x;
		player.transform.position = position;
		player.transform.localScale = scale;
		
		if(sceneName != "none")
			SceneManager.LoadScene(sceneName);
		else
			StartCoroutine(FadeIn());
	}
	public IEnumerator FadeIn(){
		CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
		while(canvasGroup.alpha > 0){
			canvasGroup.alpha -= Time.deltaTime;
			yield return null;
		}
		fadePanel.SetActive(false);
		canvasGroup.alpha = 1;
		if(!battle)
			paused = false;
	}

	public void ToggleMenu(){
		if(menu.opened){
			menu.Close();
			paused = false;
		}
		else{
			menu.Open();
			paused = true;
		}
	}
}
