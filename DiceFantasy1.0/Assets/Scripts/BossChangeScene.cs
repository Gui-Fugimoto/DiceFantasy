using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossChangeScene : MonoBehaviour
{
    public string SceneName;
    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneName);
    }
}
