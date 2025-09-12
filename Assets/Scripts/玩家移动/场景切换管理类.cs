using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class 场景切换管理类 : MonoBehaviour
{
    public static 场景切换管理类 instance;

    public List<场景传送点> 场景切换传送点;

    public string 目标传送点;

    public GameObject 传送物品;

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
    }

    public static void LoadScene(string 目标场景, string 目标传送点, GameObject 传送物品)
    {
        instance.目标传送点 = 目标传送点;

        DontDestroyOnLoad(传送物品);
        instance.传送物品 = 传送物品;

        instance.StartCoroutine(instance.LoadSceneAsync(目标场景));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // 当加载完成但场景未激活时，asyncLoad.progress 会保持在 0.9
            if (asyncLoad.progress >= 0.9f)
            {
                // 在这里可以添加一些额外的逻辑，例如显示加载完成的UI
                Debug.Log("Press a key to continue...");
                //if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }

        //场景加载完成. 设置位置
        OnLoadScene();

        //找到所有的接口,并调用函数
        var gos = FindObjectOfType<场景切换LogIOnLoadScene>().IOnLoadScenes;
        foreach (var item in gos)
        {
            item.GetComponent<场景切换IOnLoadScene>().OnLoadScene();
        }
    }

    public static void OnLoadScene()
    {
        instance.场景切换传送点 = FindObjectsOfType<场景传送点>().ToList();

        var target = instance.场景切换传送点.Where(p => p.名称 == instance.目标传送点).ToList()[0];

        instance.传送物品.transform.position = target.传送地点.transform.position;
    }
}
