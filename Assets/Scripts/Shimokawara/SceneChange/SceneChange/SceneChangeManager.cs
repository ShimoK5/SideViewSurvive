using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public enum TransitionType {
    Black,
    Obj,
    Fukui
    }


    public static SceneChangeManager instance;

    public bool isFade;

    string SceneName;

    Animator m_Animator;

    TransitionType NowTransitionType = TransitionType.Black;

    const float DEFAULT_WAIT_TIME = 0.2f;
    float NowWaitTime = 0.0f;

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

        m_Animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SceneTransition(string scenename, float waitTime = DEFAULT_WAIT_TIME, TransitionType tt = TransitionType.Fukui)
    {
        //�t�F�[�h������Ȃ����
        if (!isFade)
        {
            //�V�[���l�[���ۑ�
            SceneName = scenename;
            //�҂����ԕۑ�
            NowWaitTime = waitTime;
            //�J�ڃ^�C�v�ۑ�
            NowTransitionType = tt;
            //�t���O�I��
            isFade = true;
            //�^�C�v�ɂ���ăg�����W�V�����ύX
            switch (NowTransitionType)
            {
                case TransitionType.Black:
                    m_Animator.Play("FadeIn");
                    break;
                case TransitionType.Obj:
                    
                    m_Animator.Play("TransitionIn");
                    break;

                case TransitionType.Fukui:
                    m_Animator.Play("FukuiFadeIn");
                    break;
            }

            
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
        StartCoroutine(nameof(Wait));

    }

    void FalseIsFade()
    {
        isFade = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(NowWaitTime);
        switch (NowTransitionType)
        {
            case TransitionType.Black:
                m_Animator.Play("FadeOut");
                
                break;
            case TransitionType.Obj:
                m_Animator.Play("TransitionOut");
                break;

            case TransitionType.Fukui:
                m_Animator.Play("FukuiFadeOut");
                break;
        }
    }
}
