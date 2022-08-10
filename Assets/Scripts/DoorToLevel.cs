using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorToLevel : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] bool isAlwaysOpen = false;
    bool isPlayerNearby = false;
    bool isDoorOpen = false;

    private int doorStatus;

    private SpriteRenderer doorSpriteRenderer;

    [SerializeField] Sprite doorOpenSprite;
    [SerializeField] Sprite doorCloseSprite;

    private void Awake()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        doorStatus = PlayerPrefs.GetInt(levelName);
        Debug.Log("Doors are " + PlayerPrefs.GetInt("Level_6"));
    }

    private void Start()
    {
        if (isAlwaysOpen)
        {
            isDoorOpen = true;
        }

        else
        {
            if (doorStatus == 1)
            {
                isDoorOpen = true;
            }

            else
            {
                isDoorOpen = false;
            }
        }

        //Change sprite of door
        if (isDoorOpen == true)
        {
            doorSpriteRenderer.sprite = doorOpenSprite;
        }

        else if (isDoorOpen == false)
        {
            doorSpriteRenderer.sprite = doorCloseSprite;
        }
    }

    public void OpenDoor(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && isDoorOpen)
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
