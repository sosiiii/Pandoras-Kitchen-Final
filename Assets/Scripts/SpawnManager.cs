using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Player playerOne;
    public Player playerTwo;
    public List<DoorToLevel> doors = new List<DoorToLevel>();

    private void Start()
    {
        // Spawn player number one
        Instantiate(playerOne, doors[PlayerPrefs.GetInt("LastPlayedScene")].transform.GetChild(0).transform.position, Quaternion.identity);

        // Spawn player number two
        Instantiate(playerTwo, doors[PlayerPrefs.GetInt("LastPlayedScene")].transform.GetChild(0).transform.position, Quaternion.identity);
    }
}
