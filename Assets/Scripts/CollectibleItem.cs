using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    public int additionalShots;
    public GameObject art;
    public float flashTime;
    private Coroutine flashCoroutine;

    private void Awake() {
        flashCoroutine = StartCoroutine(flash(flashTime));
    }

    private void OnDestroy() {
        StopCoroutine(flashCoroutine);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().gameControllerInstance.AddShots(additionalShots);
            Destroy(gameObject);
        }
    }

    IEnumerator flash(float blinkTime){
        while(true){
            art.SetActive(false);
            yield return new WaitForSeconds(flashTime);
            art.SetActive(true);
            yield return new WaitForSeconds(flashTime);
        }
    }

}
