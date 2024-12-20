using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float loadDelaySeconds;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSpecificScene(sceneName, -1));
    }

    public void LoadScene(int sceneIndex) {
        StartCoroutine(LoadSpecificScene(null, sceneIndex));
    }

    public void ReloadScene() {
        StartCoroutine(LoadSpecificScene(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadSpecificScene(string sceneName = null, int sceneIndex = -1) {
        anim.SetTrigger("exitScene");
        yield return new WaitForSeconds(loadDelaySeconds);
        if (sceneName != null) {
            SceneManager.LoadScene(sceneName);
        } else {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
