using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryPickup : MonoBehaviour {

    [SerializeField] AudioClip  BerryPickUpSFX;
    [SerializeField] int pointsForBerryPickup = 100;

    bool wasCollected = false;

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player" && !wasCollected)
        {
            wasCollected =true;
            FindObjectOfType<GameSession>().AddToScore(pointsForBerryPickup);
            AudioSource.PlayClipAtPoint(BerryPickUpSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
            
        Destroy(gameObject);
    
    }
}
