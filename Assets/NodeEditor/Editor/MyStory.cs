using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class MyStory : EditorWindow
{
    private PrgTreeViewer prgTreeViewer;
    public Paragraph currentPrg;
    private ListView listViewer;
    private Button addNodeBtn;
    private Button removeNodeBtn;
    private Button newNodeBtn;
    [MenuItem("Window/UI Toolkit/MyStory")]
    public static void ShowExample()
    {
        MyStory wnd = GetWindow<MyStory>();
        wnd.titleContent = new GUIContent("MyStory");
    }

    [System.Obsolete]

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/NodeEditor/Editor/MyStory.uxml");
        visualTree.CloneTree(root);
        //绑定关键元素
        //FIXME 绑定 没指定名称
        prgTreeViewer = root.Q<PrgTreeViewer>();
        prgTreeViewer.OnPrgViewerSelected += SelectPrgViwer;
        prgTreeViewer.OnRemoveNode += OnRemoveNode;
        //左侧的东西
        listViewer = root.Q<ListView>();
        listViewer.makeItem = MakeListItem;
        listViewer.bindItem = BindListItem;
        listViewer.selectionChanged += ListSelectChange;
        //按钮
        addNodeBtn = root.Q<Button>("addNodeBtn");
        addNodeBtn.clicked += OnAddNodeBtnClick;
        removeNodeBtn = root.Q<Button>("removeNodeBtn");
        removeNodeBtn.clicked += OnRemoveNodeBtnClick;
        newNodeBtn = root.Q<Button>("newNodeBtn");
        newNodeBtn.clicked += OnNewNodeBtnClick;
    }

    private void ListSelectChange(IEnumerable<object> enumerable)
    {
        // if (enumerable == null || !enumerable.Any())
        // {
        //     Debug.Log("Enumerable is null or empty");
        //     return;
        // }

        foreach (var item in enumerable)
        {
            Selection.activeObject = (UnityEngine.Object)item;
        }
    }

    private void OnNewNodeBtnClick()
    {
        TheNode theNode = CreateInstance<TheNode>();
        theNode.name = $"N {currentPrg.name}--{currentPrg.nodes.Count}";
        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(theNode, currentPrg);
        }
        AssetDatabase.SaveAssets();
        currentPrg.nodes.Add(theNode);

        if (listViewer == null)
        {
            Debug.LogError("listViewer is null");
            return;
        }

        listViewer.RefreshItems();
    }

    private void OnRemoveNodeBtnClick()
    {
        TheNode theNode = listViewer.selectedItem as TheNode;
        if (theNode != null)
        {
            AssetDatabase.RemoveObjectFromAsset(theNode);
            Undo.DestroyObjectImmediate(theNode);
            AssetDatabase.SaveAssets();
            currentPrg.nodes.Remove(theNode);
            listViewer.RefreshItems();
        }
        else
        {
            Debug.Log("未选中删除节点");
        }
    }
    private void OnRemoveNode(TheNode theNode)
    {
        if (theNode != null)
        {
            AssetDatabase.RemoveObjectFromAsset(theNode);
            //Undo.DestroyObjectImmediate(theNode);
            AssetDatabase.SaveAssets();
            currentPrg.nodes.Remove(theNode);
            listViewer.RefreshItems();
        }
        else
        {
            Debug.Log("未选中删除节点");
        }
    }

    private void OnAddNodeBtnClick()
    {
        TheNode theNode = CreateInstance<TheNode>();
        if (theNode != null)
        {
            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(theNode, currentPrg);
            }
            AssetDatabase.SaveAssets();
            currentPrg.nodes.Insert(listViewer.selectedIndex + 1, theNode);
            listViewer.RefreshItems();
        }
        else
        {
            Debug.Log("未选中插入节点");
        }
    }

    private void BindListItem(VisualElement element, int index)
    {
        NodeViewer nodeViewer = element as NodeViewer;
        nodeViewer.NodeViewerBind((TheNode)listViewer.itemsSource[index]);
    }

    private void Sel2ctPrgViwer(PrgViewer viewer)
    {
        if (viewer == null)
        {
            Debug.LogError("viewer is null");
            return;
        }
        if (viewer.prg == null)
        {
            Debug.LogError(" viewer.prg is null");
            return;
        }
        if (listViewer == null)
        {
            Debug.LogError("listViewer is null");
            return;
        }

        if (viewer.prg.nodes == null)
        {
            Debug.LogError("viewer.prg.nodes is null");
            return;
        }

        if (viewer.prg.nodes.Count == 0)
        {
            return;
        }
        else
        {
            listViewer.itemsSource = null;
            listViewer.itemsSource = viewer.prg.nodes;
        }
    }

    [System.Obsolete]
    public void SelectPrgViwer(PrgViewer viewer)
    {
        if (viewer == null)
        {
            Debug.LogError("Viewer is null");
            return;
        }

        if (viewer.prg == null)
        {
            Debug.LogError("Viewer's prg is null");
            return;
        }

        if (viewer.prg.nodes == null)
        {
            Debug.LogError("Viewer's prg.nodes is null");
            return;
        }

        currentPrg = viewer.prg;

        // 记录日志
        Debug.Log("Clearing listViewer's itemsSource");

        // 清空 listViewer 的 itemsSource
        listViewer.itemsSource = null;

        // 检查 listViewer 的状态
        if (listViewer == null)
        {
            Debug.LogError("listViewer is null after clearing itemsSource");
            return;
        }

        // 记录日志
        Debug.Log("Setting listViewer's itemsSource to new nodes");

        // 重新设置 itemsSource
        listViewer.itemsSource = viewer.prg.nodes;

        // 检查 listViewer 的状态
        if (listViewer.itemsSource == null)
        {
            Debug.LogError("listViewer's itemsSource is null after setting new nodes");
            return;
        }

        // 更新 ListView（如果需要）
        listViewer.Refresh();

        // 记录日志
        Debug.Log("ListView refreshed successfully");
    }
    private VisualElement MakeListItem()
    {
        return new NodeViewer();
    }

    // Unity编辑器中，选择新的一个东西时调用
    private void OnSelectionChange()
    {
        if (Selection.activeObject is PrgTree prgTree)
        {
            prgTreeViewer.PopulateViewer(prgTree);
        }
    }
}