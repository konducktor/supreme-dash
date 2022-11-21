using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance;
    private static bool shouldPlayAnimation = false;

    private Text progressPersentage;
    private Scrollbar progressBar;

    private Animator animator;
    private AsyncOperation loadingSceneOperation;

    public static void ChangeScene(string sceneName)
    {
        instance.animator.SetTrigger("sceneClose");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;

    }

    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();
        progressPersentage = GetComponentInChildren<Text>();
        progressBar = GetComponentInChildren<Scrollbar>();

        if (shouldPlayAnimation) animator.SetTrigger("sceneOpen");
    }


    void Update()
    {
        if (loadingSceneOperation != null)
        {
            progressPersentage.text = GlobalControl.Round(loadingSceneOperation.progress * 100f, 0) + "%";
            progressBar.size = loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        shouldPlayAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
