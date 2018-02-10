using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController: MonoBehaviour {

    Animator animator;
    const string COLLECTED = "COLLECTED";
    bool collected = false;
   public AudioClip clip;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            Debug.Log("PlayerDetected!");
            collected = true;
            animator.SetBool(COLLECTED, collected);
            SoundManager.PlaySFX(clip);
        }
    }

    public void EnterAnimationCollectedFinished() {
        Destroy(gameObject);
    }
}
