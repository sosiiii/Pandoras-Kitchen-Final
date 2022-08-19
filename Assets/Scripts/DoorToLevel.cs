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
        if (isPlayerNearby && isDoorOpen && context.performed)
        {
            StartCoroutine(EnterDoor());
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                player.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator EnterDoor()
    {
        float time = 0;
        Vector3 startPos = Camera.main.transform.position;
        Vector3 finalPos = transform.position - new Vector3(0.25f, 0, 0);
        finalPos.z = -10;

        while (Vector2.Distance(finalPos, Camera.main.transform.position) > 1 || Camera.main.orthographicSize > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(startPos, finalPos, time*30);
            Camera.main.orthographicSize -= Time.deltaTime * 20;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(levelName);
        //go to level
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
