using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	public static int mapSize = 10;
	public IntMatrix[] layout = new IntMatrix[mapSize];
	public IntMatrix[] buildingLayout = new IntMatrix[mapSize];
	public IntMatrix[] battleLayout = new IntMatrix[mapSize];
	public IntMatrix[] npcLayout = new IntMatrix[mapSize];
	public List<Treasure> treasureLayout;
	public Enemy[] encounterableEnemies;
	public int encounterRate = 0;
	public Map right;
	public Map left;
	public Map up;
	public Map down;

}
