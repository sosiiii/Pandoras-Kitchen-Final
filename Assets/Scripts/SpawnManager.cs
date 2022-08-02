using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Player player;
    public List<Transform> spawnPoints = new List<Transform>();
    public int onlyOnce;

    private void Start()
    {
        if (PlayerPrefs.GetInt("LastPlayedScene") == 0)
        {
            Instantiate(player, spawnPoints[0].transform.position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("LastPlayedScene") == 1)
        {
            Instantiate(player, spawnPoints[1].transform.position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("LastPlayedScene") == 2)
        {
            Instantiate(player, spawnPoints[2].transform.position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("LastPlayedScene") == 3)
        {
            Instantiate(player, spawnPoints[3].transform.position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("LastPlayedScene") == 4)
        {
            Instantiate(player, spawnPoints[4].transform.position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("LastPlayedScene") == 5)
        {
            Instantiate(player, spawnPoints[5].transform.position, Quaternion.identity);
        }

    }
}
