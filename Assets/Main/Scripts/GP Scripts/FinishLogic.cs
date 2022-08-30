using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLogic : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;

    void OnTriggerEnter2D(Collider2D collider) //collider.GetComponent<FinishLogic>().isCustom
    {
        if (!collider.CompareTag("Player"))
        {
            return;
        }

        Instantiate(finishMenu);

    }
}
