using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Full Name:        Tyler Miles
Student ID:       101251005
File:             ShrinkingPlatform.cs
Description:      This controls the shrinking platform prefab.
Date last modified: Dec 18, 2021
*/

public class ShrinkingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    float scaleRate = 0.05f;
    bool playerColliding = false;
    Vector2 platformStartSize;
    Vector2 platformStartPos;

    public AudioSource[] sounds;

    bool platformShrunk;

    void Start()
    {
        platformStartSize = this.gameObject.transform.localScale;
        platformStartPos = this.gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.localScale.x < 0)
        {
            this.gameObject.transform.localScale = new Vector3(0, platformStartSize.y, 0);
            Debug.Log(platformStartSize);
            playerColliding = false;

            if (playerColliding == false)
            {
                StartCoroutine(GrowTimer());
            }
        }

        Debug.Log(playerColliding);
        HoverUpDown();
    }

    IEnumerator Shrink()
    {
        while (this.gameObject.transform.localScale.x > 0 && playerColliding)
        {
           
            //this.gameObject.transform.localScale -= new Vector3(scaleRate, scaleRate, 0); // works

            Vector3 resizedPlatform = new Vector3(this.gameObject.transform.localScale.x - scaleRate, this.gameObject.transform.localScale.y, 0);
            Debug.Log(resizedPlatform);
            this.gameObject.transform.localScale = Vector3.Lerp(transform.localScale, resizedPlatform, 0.5f);
            yield return new WaitForSeconds(0.05f);

            
        }



    }

    void HoverUpDown()
    {
        transform.position = new Vector3(transform.position.x, platformStartPos.y + Mathf.PingPong(Time.time / 3, 0.25f), 0.0f);
    }

    IEnumerator Regrow()
    {
        while (this.gameObject.transform.localScale.x < platformStartSize.x)
        {

            //this.gameObject.transform.localScale -= new Vector3(scaleRate, scaleRate, 0); // works

            Vector3 resizedPlatform = new Vector3(this.gameObject.transform.localScale.x + scaleRate, this.gameObject.transform.localScale.y, 0);
            //Debug.Log(resizedPlatform);
            this.gameObject.transform.localScale = Vector3.Lerp(transform.localScale, resizedPlatform, 0.5f);
            yield return new WaitForSeconds(0.1f);


        }
    }

    IEnumerator ShrinkTimer()
    {
        yield return new WaitForSeconds(0.5f);

        if (playerColliding)
        {
            StartCoroutine(Shrink());
            sounds[0].Play();
        }
    }

    IEnumerator GrowTimer()
    {
        yield return new WaitForSeconds(1.5f);

        if (playerColliding == false)
        {
            StartCoroutine(Regrow());
            sounds[1].Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("player touched");
            playerColliding = true;
            StopAllCoroutines();
            StartCoroutine(ShrinkTimer());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("player ceased touch");
            playerColliding = false;
            StopAllCoroutines();
            StartCoroutine(GrowTimer());
        }
    }
}
