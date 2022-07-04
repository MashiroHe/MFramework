using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrancePanel : MonoBehaviour
{
    [SerializeField] private TreeView _treeView;
    [SerializeField] private ImageEntranceItem _entranceTemplate;

    private List<ImageEntranceItem> _allEntranceItem = new List<ImageEntranceItem>();
    
    // Start is called before the first frame update
    void Start()
    {
        _entranceTemplate.gameObject.SetActive(false);
        
        _treeView.OnItemSelected += OnTreeItemSelected;

        TreeViewItem defaultSelectItem = null;
        
        //_treeView.AddItem().SetData(1, $"图表示例", $"chart");

        for (int i = 1; i <= 10; i++)
        {
            var item = _treeView.AddItem().SetData($"分类{i}", $"分类{i}");
            if (!defaultSelectItem)
            {
                defaultSelectItem = item;
            }
        }
        
        _treeView.SelectItem(defaultSelectItem);

    }

    public void OnEntranceClicked()
    {
        SceneManager.LoadScene("all_model_preview");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.isPlaying)
            {
                Application.Quit();
            }
        }
    }

    private void OnTreeItemSelected(TreeViewItem item)
    {
        var path = item.UserData as string;

        if (path == "chart")
        {
            SceneManager.LoadScene("chart");
            return;
        }
        
        if (!string.IsNullOrEmpty(path))
        {
            ClearAllEntranceItem();

            for (int i = 0; i < 12; i++)
            {
                AddEntranceItem("ModelPic/LGM-30G", $"{path}-{i}");
            }
        }
    }

    private ImageEntranceItem AddEntranceItem(string imgPath, string desc)
    {
        var entranceItem = CreateEntranceItem();
        entranceItem.SetData(imgPath, desc);
        
        _allEntranceItem.Add(entranceItem);
        return entranceItem;
    }
    
    private void ClearAllEntranceItem()
    {
        foreach (var entranceItem in _allEntranceItem)
        {
            Destroy(entranceItem.gameObject);
        }
        _allEntranceItem.Clear();
    }
    
    private ImageEntranceItem CreateEntranceItem()
    {
        var go = Instantiate(_entranceTemplate.gameObject, _entranceTemplate.transform.parent);
        go.SetActive(true);
        return go.GetComponent<ImageEntranceItem>();
    }
}
