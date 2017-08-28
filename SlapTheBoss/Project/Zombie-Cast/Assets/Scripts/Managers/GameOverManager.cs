using UnityEngine;

using Google.Cast.RemoteDisplay;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.
        public GameObject gameOverButtonCanvas;
        public GameObject gameOverRemoteCanvas;
        public GameObject restartButton;

        void Awake ()
        {
            // Set up the reference.
            restartButton.SetActive(false);
        }


        void Update ()
        {
            // If the player has run out of health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the game is over.
                gameOverButtonCanvas.GetComponent<Canvas>().enabled = true;
                gameOverButtonCanvas.GetComponent<Animator>().SetTrigger("GameOver");
                restartButton.SetActive(true);
                if (CastRemoteDisplayManager.GetInstance().IsCasting()) {
                    gameOverRemoteCanvas.GetComponent<Animator>().SetTrigger("GameOver");
                } else {
                    gameOverRemoteCanvas.SetActive(false);
                }
            }
        }
    }
}