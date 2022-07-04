using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using XCharts.Runtime;
using Debug = UnityEngine.Debug;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private TreeView _treeView;
    [SerializeField] private AttributeView _attributeView;
    [SerializeField] private LineChart _lineChart;

    private GameObject _currentModel;
    
    public enum TreeViewItemType
    {
        Invalid,
        Model,
        LineChart,
        ImageChart,
    }
    
    public class TreeViewItemUserData
    {
        public TreeViewItemUserData(TreeViewItemType type, string parameter)
        {
            Type = type;
            Parameter = parameter;
        }
        public TreeViewItemType Type;
        public string Parameter;
    }
    
    private void Start()
    {
        _lineChart.gameObject.SetActive(false);
        
        TreeViewItem defaultSelecteItem = null;
        //首先，注册点击列表按钮回调事件函数
        _treeView.OnItemSelected = OnTreeViewItemSelected;
        //创建根节点按钮（一级按钮）
        var rootItem = _treeView.AddItem(null).SetData("烈火IV");
        //在根节点按钮基础上创建子节点（二级按钮）按钮，并初始化数据
        var wholeItem = _treeView.AddItem(rootItem)
            .SetData("整弹", new TreeViewItemUserData(TreeViewItemType.Model, "ModelPreview/LGM-30G"));//参数依次是：显示标题，数据结构：触发显示类型，加载资源路径

        defaultSelecteItem = wholeItem;
        
        {
            //创建3级按钮以及其8个子级按钮
            var electromagnetism = _treeView.AddItem(wholeItem).SetData("电磁特性");
            electromagnetism.IsFolding = true;
            _treeView.AddItem(electromagnetism).SetData("电磁仿真数据", new TreeViewItemUserData(TreeViewItemType.LineChart, ""));//三级子按钮注册点击事件回调判断数据
            _treeView.AddItem(electromagnetism).SetData("室内电磁测量数据");
            _treeView.AddItem(electromagnetism).SetData("外场测量数据");
            _treeView.AddItem(electromagnetism).SetData("散射中心数据");
            _treeView.AddItem(electromagnetism).SetData("雷达回波数据");
            _treeView.AddItem(electromagnetism).SetData("距离像");
            _treeView.AddItem(electromagnetism).SetData("二维图像");
            _treeView.AddItem(electromagnetism).SetData("三维图像");
            _treeView.AddItem(electromagnetism).SetData("电磁实验库数据");
        }
        {
            //2级按钮同级按钮
            var movement = _treeView.AddItem(wholeItem).SetData("运动特性");
            movement.IsFolding = true;
            _treeView.AddItem(movement).SetData("外场测试数据");
            _treeView.AddItem(movement).SetData("弹道轨迹");
        }
        {
            //2级按钮同级按钮以及其子级按钮
            var light = _treeView.AddItem(wholeItem).SetData("光学特性");
            light.IsFolding = true;
            _treeView.AddItem(light).SetData("温度仿真");
            _treeView.AddItem(light).SetData("辐射强度仿真");
            _treeView.AddItem(light).SetData("散射强度仿真");
            _treeView.AddItem(light).SetData("激光仿真");
            _treeView.AddItem(light).SetData("光学重构参数集");
            _treeView.AddItem(light).SetData("实验室光学测试");
            _treeView.AddItem(light).SetData("光学外场测量");
            _treeView.AddItem(light).SetData("光学处理");
            _treeView.AddItem(light).SetData("光学实验库数据");
        }
        {
            //2级按钮同级按钮以及其子级按钮
            var physical = _treeView.AddItem(wholeItem).SetData("物理特性");
            physical.IsFolding = true;
            _treeView.AddItem(physical).SetData("计算模型");
            _treeView.AddItem(physical).SetData("材料模型");
            _treeView.AddItem(physical).SetData("显示模型");
            _treeView.AddItem(physical).SetData("显示材料文件");
            _treeView.AddItem(physical).SetData("目标图片几何");
            _treeView.AddItem(physical).SetData("电磁或红外特性");
        }
        {
            //其它无子级按钮的二级按钮，//参数依次是：显示标题，数据结构：触发显示类型，加载资源路径
            _treeView.AddItem(wholeItem).SetData("无一级导弹", new TreeViewItemUserData(TreeViewItemType.Model, "ModelPreview/LGM-30G-Part1"));
            _treeView.AddItem(wholeItem).SetData("弹头", new TreeViewItemUserData(TreeViewItemType.Model, "ModelPreview/LGM-30G-Part2"));
            _treeView.AddItem(wholeItem).SetData("一级助推器", new TreeViewItemUserData(TreeViewItemType.Model, "ModelPreview/LGM-30G-Part3"));
            _treeView.AddItem(wholeItem).SetData("二级助推器", new TreeViewItemUserData(TreeViewItemType.Model, "ModelPreview/LGM-30G-Part4"));
        }

        //创建添加属性列表参数
        _attributeView.AddTitle().SetData("参考属性");
        {
            _attributeView.AddProperty().SetData("空重", "2000kg");
            _attributeView.AddProperty().SetData("最大起飞重量", "3000kg");
            _attributeView.AddProperty().SetData("长度", "50M");
            _attributeView.AddProperty().SetData("宽度", "3M");
            _attributeView.AddProperty().SetData("高度", "3M");
            _attributeView.AddProperty().SetData("高度", "3M");
            _attributeView.AddProperty().SetData("高度", "3M");
        }
        _attributeView.AddTitle().SetData("参考性能");
        {
            _attributeView.AddProperty().SetData("空重", "2000kg");
            _attributeView.AddProperty().SetData("最大起飞重量", "3000kg");
            _attributeView.AddProperty().SetData("长度", "50M");
            _attributeView.AddProperty().SetData("宽度", "3M");
            _attributeView.AddProperty().SetData("高度", "3M");
            _attributeView.AddProperty().SetData("高度", "3M");
            _attributeView.AddProperty().SetData("高度", "3M");
        }
        
        _treeView.OnItemSelected = OnTreeViewItemSelected;
        _treeView.SelectItem(defaultSelecteItem);
    }
    /// <summary>
    /// 点击回调函数，根据上面初始化传递保存数据，点击回调触发时，判断执行相应事件
    /// </summary>
    /// <param name="item"></param>
    private void OnTreeViewItemSelected(TreeViewItem item)
    {
        var userData = item.UserData as TreeViewItemUserData;
        if (userData != null)
        {
            switch (userData.Type)
            {
                case TreeViewItemType.Model:
                    LoadModel(userData.Parameter);
                    break;
                case TreeViewItemType.LineChart:
                    LoadLineChart(userData.Parameter);
                    break;
                case TreeViewItemType.ImageChart:
                    LoadImageChart(userData.Parameter);
                    break;
                case TreeViewItemType.Invalid:
                    break;
                default:
                    break;
            }
        }
    }
    
    private void LoadModel(string modelPath)
    {
        if (_currentModel)
        {
            Destroy(_currentModel);
        }

        var prefab = Resources.Load<GameObject>(modelPath);
        if (prefab)
        {
            _currentModel = Instantiate(prefab);
        }
        else
        {
            Debug.LogError($"加载模型失败:{modelPath}");
        }
       
    }
    
    private void LoadLineChart(string lineChartPath)
    {
        _lineChart.gameObject.SetActive(true);

        //Y轴标志数据初始化
        var yAxis = _lineChart.GetChartComponent<YAxis>();
        yAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        yAxis.min = 0;
        yAxis.max = 100;

        _lineChart.RemoveData();
        
        var colors = new Color[] {Color.green, Color.blue, Color.yellow, Color.red};
        for (int i = 0; i < 4; i++)
        {
            var serie = _lineChart.AddSerie<Line>("Line");
            serie.lineStyle.width = 0.6f;
            serie.lineType = LineType.Smooth;
            colors[i].a = 0.5f;
            serie.lineStyle.color = colors[i];
            serie.symbol.show = false;

        }

        for (float i = 0; i < Mathf.PI * 7; i += 0.1f*Mathf.PI)
        {
            //x轴标志数据初始化
            _lineChart.AddXAxisData("" + (i + 1));
            //添加折线点数据
            _lineChart.AddData(0, 65 + 10 * Mathf.Sin(i) + UnityEngine.Random.Range(0, 30));
            _lineChart.AddData(1, 50 + 10 * Mathf.Sin(i) + UnityEngine.Random.Range(0, 30));
            _lineChart.AddData(2, 25 + 10 * Mathf.Sin(i) + UnityEngine.Random.Range(0, 30));
            _lineChart.AddData(3, 10 + 10 * Mathf.Sin(i) + UnityEngine.Random.Range(0, 30));
        }
    }
    
    private void LoadImageChart(string lineChartPath)
    {
        
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.isPlaying)
            {
                SceneManager.LoadScene("entrance");
            }
        }
    }
}
