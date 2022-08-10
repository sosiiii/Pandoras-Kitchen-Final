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
        /*if (PlayerPrefs.GetInt("LastPlayedScene") == 0)
        {
            Instantiate(player, doors[0].gameObject.transform.GetChild(0).gameObject.transform, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("LastPlayedScene") == 1)
        {
            Instantiate(player, spawnPoints[1].transform.position, Quaternion.identity);
        }*/

        Instantiate(playerOne, doors[PlayerPrefs.GetInt("LastPlayedScene")].transform.GetChild(0).transform.position, Quaternion.identity);
        Instantiate(playerTwo, doors[PlayerPrefs.GetInt("LastPlayedScene")].transform.GetChild(0).transform.position, Quaternion.identity);
    }
}
