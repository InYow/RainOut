using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NodeCanvas.Tasks.Actions;
using UnityEditor;
using UnityEngine;

public class 虚拟摄像机跟随玩家OnLoadScene : MonoBehaviour, 场景切换IOnLoadScene
{
    public void OnLoadScene()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<玩家移动>().gameObject.transform;
    }

    private void OnValidate()
    {
        // 使用 EditorApplication.update 延迟调用 ValidateInEditor 方法
        //EditorApplication.update += ValidateInEditor;
        ValidateInEditor();
    }

    public void ValidateInEditor()
    {
        // 移除延迟调用以避免重复调用
        //EditorApplication.update -= ValidateInEditor;

        // 查找 LogIOnLoadScene 对象
        var log = FindObjectOfType<场景切换LogIOnLoadScene>();

        // 如果没有找到，创建一个新的 GameObject 并添加 LogIOnLoadScene 组件
        if (log == null)
        {
            Debug.LogError("场景中没有'记录场景切换行为 LogIOnLoadScene'");
            return;
            //var GO = new GameObject("记录场景切换行为 LogIOnLoadScene");
            //GO.transform.position = Vector3.zero;

            //log = GO.AddComponent<LogIOnLoadScene>();
        }

        // 获取 LogIOnLoadScene 中的列表
        if (log.IOnLoadScenes == null)
            log.IOnLoadScenes = new();
        var list = log.IOnLoadScenes;


        // 检查列表中是否已经包含当前对象
        if (!list.Contains(this.gameObject))
        {
            list.Add(this.gameObject);
        }
    }

}
