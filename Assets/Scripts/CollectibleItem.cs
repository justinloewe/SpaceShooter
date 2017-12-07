using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    public int additionalShots;
    public GameObject art;
    public float blinkTime;
    private Coroutine blinkCoroutine;

    private void Awake() {
        blinkCoroutine = StartCoroutine(blink(blinkTime));
    }

    private void OnDestroy() {
        StopCoroutine(blinkCoroutine);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().gameControllerInstance.shots += additionalShots;
        }
    }

    IEnumerator blink(float blinkTime){
        while(true){
            art.SetActive(false);
            yield return new WaitForSeconds(blinkTime);
            art.SetActive(true);
        }
    }

}
