using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class MovieEndSceneChange : MonoBehaviour
{
    [Header("フェードのフレーム")]
    [SerializeField] long FADE_FLAME;
    [Header("次シーンの名前")]
    [SerializeField] string NEXT_SCENE_NAME;

    VideoPlayer vp;

    // Start is called before the first frame update
    void Start()
    {
        vp = GameObject.Find("Movie").GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("frame = " + vp.frame);
        Debug.Log("frameCount = " + (long)vp.frameCount);

        if (vp.frame  >= (long)vp.frameCount - (FADE_FLAME / 2))
        {
            //SceneManager.LoadScene(NEXT_SCENE_NAME);
            SceneChangeManager.instance.SceneTransition(NEXT_SCENE_NAME);
        }        
    }
}
