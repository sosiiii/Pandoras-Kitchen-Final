using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToLevel : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] bool isAlwaysOpen = false;
    bool isPlayerNearby = false;
    bool isDoorOpen = false;

    private SpriteRenderer doorSpriteRenderer;

    [SerializeField] Sprite doorOpenSprite;
    [SerializeField] Sprite doorCloseSprite;

    private void Awake()
    {
        int doorStatus = PlayerPrefs.GetInt(levelName);

        if (doorStatus == 1)
        {
            isDoorOpen = true;
        }

        else
        {
            isDoorOpen = false;
        }

        if (isAlwaysOpen)
        {
            isDoorOpen = true;
        }

        doorSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (isDoorOpen == true)
        {
            doorSpriteRenderer.sprite = doorOpenSprite;
        }

        else if (isDoorOpen == false)
        {
            doorSpriteRenderer.sprite = doorCloseSprite;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.W) && isDoorOpen)
        {
            SceneManager.LoadScene(levelName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerNearby = false;
        }
    }
}
