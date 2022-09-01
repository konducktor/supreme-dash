using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameObject.Find("Main").GetComponent<GameLoader>().finishMenu.SetActive(true);
        }
    }
}
