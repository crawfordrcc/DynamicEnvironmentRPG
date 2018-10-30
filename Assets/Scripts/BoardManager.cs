using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    [Serializable]
    public class Count{
        public int minimum;
        public int maximum;

        public Count(int min, int max){
            minimum = min;
            maximum = max;
        }
    }
	
	public GameObject[] floorTiles;
	public GameObject[] buildingTiles;
	public GameObject[] npcs;
	public Map currentMap;

	private Transform boardHolder;

	private List<Vector3> gridPositions = new List<Vector3>();

	void InitializeList (){
		/*gridPositions.Clear();
		for (int i = 0; i < columns; ++i)
			for(int j = 0; j < rows; ++i)
				gridPositions.Add(new Vector3(i, j, 0f));*/
		/*currentLayout = new int[10, 10]{
			{0, 0, 2, 0, 0, 0, 0, 2, 0, 0},
			{0, 0, 2, 0, 0, 0, 0, 2, 0, 0},
			{0, 0, 5, 1, 1, 1, 1, 4, 0, 0},
			{0, 0, 2, 0, 0, 0, 0, 0, 0, 0},
			{1, 1, 9, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 2, 0, 0, 0, 0, 0, 0, 0},
			{0, 0, 3, 1, 1, 1, 1, 6, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 2, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 2, 0, 0},
			{0, 0, 0, 0, 0, 0, 0, 2, 0, 0}
		};*/
		//Console.Write(currentLayout[0, 0]);
	}

	public void Clear(){
		foreach (Transform child in boardHolder)
    		GameObject.Destroy(child.gameObject);
	}

	public void LayoutTiles(){
		GameObject toInstantiate = floorTiles[0];
		for(int i = 0; i < currentMap.layout.Length; ++i){
			for(int j = 0; j < currentMap.layout[i].row.Length; ++j){
				/*if(currentLayout[i, j] < floorTiles.Length)
					toInstantiate = floorTiles[currentLayout[i, j]];
				else
					toInstantiate = buildingTiles[currentLayout[i, j] - floorTiles.Length];
				*/
				toInstantiate = floorTiles[currentMap.layout[i].row[j]];
				GameObject instance = Instantiate(toInstantiate, new Vector3(j, i, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
				if(currentMap.buildingLayout[i].row[j] != 0){
					toInstantiate = buildingTiles[currentMap.buildingLayout[i].row[j] - 1];
					instance = Instantiate(toInstantiate, new Vector3(j, i, 0f), Quaternion.identity);
					instance.transform.SetParent(boardHolder);
				}
				if(currentMap.npcLayout[i].row[j] != 0){
					toInstantiate = npcs[currentMap.npcLayout[i].row[j] - 1];
					instance = Instantiate(toInstantiate, new Vector3(j, i, 0f), Quaternion.identity);
					instance.transform.SetParent(boardHolder);
				}
			}
		}
	}

	public void SetupScene(){
		InitializeList();
		boardHolder = new GameObject ("Board").transform;
		LayoutTiles();
	}
	
	public void LayoutBattleTiles(){
		GameObject toInstantiate = floorTiles[0];
		for(int i = 0; i < currentMap.battleLayout.Length; ++i){
			for(int j = 0; j < currentMap.battleLayout[i].row.Length; ++j){
				/*if(currentLayout[i, j] < floorTiles.Length)
					toInstantiate = floorTiles[currentLayout[i, j]];
				else
					toInstantiate = buildingTiles[currentLayout[i, j] - floorTiles.Length];
				*/
				toInstantiate = floorTiles[currentMap.battleLayout[i].row[j]];
				GameObject instance = Instantiate(toInstantiate, new Vector3(j-12, i-5, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
			}
		}
	}

	public void moveLeft(){
		currentMap = currentMap.left;
		Clear();
		LayoutTiles();
	}

	public void moveRight(){
		currentMap = currentMap.right;
		Clear();
		LayoutTiles();
	}

	public void moveDown(){
		currentMap = currentMap.down;
		Clear();
		LayoutTiles();
	}

	public void moveUp(){
		currentMap = currentMap.up;
		Clear();
		LayoutTiles();
	}
}