using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    float scaleRate = 0.1f;
    bool playerColliding = false;
    float resizeTimer = 0.25f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.localScale.x <= 0 || this.gameObject.transform.localScale.y <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator Shrink()
    {
        while (this.gameObject.transform.localScale.x > 0 && this.gameObject.transform.localScale.y > 0)
        {
           
            this.gameObject.transform.localScale -= new Vector3(scaleRate, scaleRate, 0); // works


            //float newScaleRate = 0.1f;
            //Vector3 resizedPlatform = new Vector3(this.gameObject.transform.localScale.x - newScaleRate, this.gameObject.transform.localScale.y - newScaleRate, 0);
            //Debug.Log(resizedPlatform);
            //this.gameObject.transform.localScale = Vector3.Lerp(transform.localScale, resizedPlatform, Time.deltaTime);
            yield return new WaitForSeconds(0.1f);

            
        }



    }

    IEnumerator ShrinkTimer()
    {
         yield return new WaitForSeconds(0.5f);

        if (playerColliding)
        {
            StartCoroutine(Shrink());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("player touched");
            playerColliding = true;
            StartCoroutine(ShrinkTimer());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("player ceased touch");
            playerColliding = false;
        }
    }
}
