using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager instance;

    public bool isFade;

    string sceneName;

    Animator animator;

    [SerializeField] float waitTime;

    private void Awake()
    {
       if (instance == null)
       {
           instance = this;
           DontDestroyOnLoad(this.gameObject);
       }
       else
       {
           Destroy(this.gameObject);
       }

        animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SceneTransition(string scenename)
    {
        if (!isFade)
        {
            sceneName = scenename;
            isFade = true;
            animator.Play("FadeIn");
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(nameof(Wait));

    }

    void FalseIsFade()
    {
        isFade = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        animator.Play("FadeOut");
    }
}
