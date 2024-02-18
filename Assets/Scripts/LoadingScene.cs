using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadGame");
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
