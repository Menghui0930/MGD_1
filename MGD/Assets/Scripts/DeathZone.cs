using UnityEngine;

public class DeathZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null) {
                playerRespawn.Die();
            }
        }
    }
}