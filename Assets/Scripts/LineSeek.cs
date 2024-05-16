using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LineSeek : MonoBehaviour
{
    public Button test;

    // Move at a speed of 5 units per second
    float speed = 5.0f;

    void Start()
    {
        // Lab 1 task:
        // Add "Previous" and "Next" buttons to each scene so
        // that scenes change accordingly when each button is pressed.

        // Scene-changing code:
        //SceneManager.LoadScene("BeginScene");
        //SceneManager.LoadScene("EndScene");
        //SceneManager.LoadScene("PlayScene");

        // Button-clicking code:
        test.onClick.AddListener(OnTestClick);
    }

    void OnTestClick()
    {
        Debug.Log("Test");
    }

    // Move the object towards the mouse cursor
    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0.0f;
        transform.position = Vector3.MoveTowards(transform.position, mouse, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    // Proximity-based collision handlers -- check "Is Trigger" in Collider Component's Inspector
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log($"Trigger between {name} and {collision.gameObject.name} started!");
    //}
    //
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log($"Trigger between {name} and {collision.gameObject.name} persisting...");
    //}
    //
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log($"Trigger between {name} and {collision.gameObject.name} ended!");
    //}

    // Physics-based collision -- uncheck "Is Trigger" in Collider Component's Inspector
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log($"Collision between {name} and {collision.gameObject.name} started!");
    //}
    //
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log($"Collision between {name} and {collision.gameObject.name} persisting...");
    //
    //}
    //
    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log($"Collision between {name} and {collision.gameObject.name} ended!");
    //}
}
