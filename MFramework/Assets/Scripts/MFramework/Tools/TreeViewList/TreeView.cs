using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class TreeView : MonoBehaviour
    {
        private class TreeNode<T> where T:new()
        {
            public TreeNode(TreeNode<T> parent, T current)
            {
                Parent = parent;
                Value = current;
            }
            
            public T Value;
            public TreeNode<T> Parent = null;
            public List<TreeNode<T>> Children = new List<TreeNode<T>>();
            
            public TreeNode<T> FindItem(Predicate<T> predicate)
            {
                if (predicate(Value))
                {
                    return this;
                }

                foreach (var child in Children)
                {
                    var node = child.FindItem(predicate);
                    if (node != null)
                    {
                        return node;
                    }
                }

                return null;
            }

            public void Foreach(Action<TreeNode<T>> action)
            {
                action(this);
                foreach (var child in Children)
                {
                    child.Foreach(action);
                }
            }
            
            public int ParentCount
            {
                get
                {
                    int count = 0;
                    
                    var parent = Parent;
                    while (parent != null)
                    {
                        count += 1;
                        parent = parent.Parent;
                    }

                    return count;
                }
            }
            
            public void Clear()
            {
                foreach (var node in Children)
                {
                    node.Clear();
                }
                
                Children.Clear();
            }
        }
        
        [SerializeField] private TreeViewItem _itemTemplate;
        [SerializeField] private RectTransform _content;
        [SerializeField] private float _lineSpace = 10;
        [SerializeField] private float _tabSpace = 10;

        private TreeNode<TreeViewItem> _root = new TreeNode<TreeViewItem>(null, null);
        
        [SerializeField] private bool _treeDirty = false;

        public Action<TreeViewItem> OnItemSelected;
        
        /// <summary>
        /// 增加创建Item按钮
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public TreeViewItem AddItem(TreeViewItem parent = null)
        {
            _treeDirty = true;

            var parentNode = _root.FindItem((p) => p == parent);
            if (parentNode == null)
            {
                throw new System.Exception("parent not found");
            }
            
            var treeViewItem = CreateItem();
            treeViewItem.SetParentTreeView(this);
            var node = new TreeNode<TreeViewItem>(parentNode, treeViewItem);
            
            parentNode.Children.Add(node);

            node.Value.SetIndex(parentNode.Children.Count);
            
            return treeViewItem;
        }

        public TreeViewItem GetItem(object userData)
        {
            var node = _root.FindItem((p) => p && p.UserData == userData);

            if (node != null)
            {
                return node.Value;
            }

            return null;
        }
        
        public void Clear()
        {
            _root.Foreach((p) => {if(p.Value) {Destroy(p.Value.gameObject);}});
            
            _root.Clear();
        }

        private void Update()
        {
            if (_treeDirty)
            {
                _treeDirty = false;
                Reposition();
            }
        }

        public void SelectItem(TreeViewItem item)
        {
            OnTreeViewItemContentClicked(item);
        }
        
        public void OnTreeViewItemContentClicked(TreeViewItem treeViewItem)
        {
            //点击按钮主体部分 改变折叠图标显示
            treeViewItem.IsFolding = !treeViewItem.IsFolding;
            _treeDirty = true;
            //触发回调事件，先设置当前选中按钮状态为false,然后将点击的按钮状态设为选中，并调用触发回调函数
            var currentSelected = _root.FindItem(p => p && p.IsSelected);
            if (currentSelected != null)
            {
                currentSelected.Value.IsSelected = false;
            }

            if (treeViewItem)
            {
                treeViewItem.IsSelected = true;
            
                OnItemSelected?.Invoke(treeViewItem);
            }
        }
        //点击刷新UI绘制并改变折叠图标状态
        public void OnTreeViewItemFoldingClicked(TreeViewItem treeViewItem)
        {
            treeViewItem.IsFolding = !treeViewItem.IsFolding;
            _treeDirty = true;
        }

        private float _currentX;
        private float _currentY;
        
        private void Reposition()
        {
            _currentX = _currentY = 0;
            
            _root.Foreach(RePosition);
            
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, -_currentY);
        }

        private void RePosition(TreeNode<TreeViewItem> node)
        {
            var treeViewItem = node.Value;
            if (treeViewItem == null)
            {
                return;
            }

            var isFolding = false;
            var parent = node.Parent;
            while (parent != null)
            {
                if (parent.Value && parent.Value.IsFolding)
                {
                    isFolding = true;
                    break;
                }
                parent = parent.Parent;
            }

            if (isFolding)
            {
                treeViewItem.gameObject.SetActive(false);
                return;
            }
            else
            {
                treeViewItem.gameObject.SetActive(true);
            }
            
            _currentX = (node.ParentCount -1) * _tabSpace;
            
            treeViewItem.transform.localPosition = new Vector3(_currentX, _currentY);
            treeViewItem.SetFoldingEnabled(node.Children.Count > 0);
            
            _currentY -= _lineSpace;
        }

        private void Awake()
        {
            _itemTemplate.gameObject.SetActive(false);
        }
        /// <summary>
        /// 实例化模板Item并获取其控制脚本返回
        /// </summary>
        /// <returns></returns>
        private TreeViewItem CreateItem()
        {
            var go = Instantiate(_itemTemplate.gameObject, _content);
            go.SetActive(true);
            return go.GetComponent<TreeViewItem>();
        }
    }
}