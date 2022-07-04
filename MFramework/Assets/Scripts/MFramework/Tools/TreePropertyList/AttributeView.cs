using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class AttributeView : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private AttributeViewTitle _titleTemplate;
        [SerializeField] private AttributeViewProperty _propertyTemplate;
        
        [SerializeField] private float _lineSpaceTitle = 30;
        [SerializeField] private float _lineSpaceProperty = 30;

        private List<MonoBehaviour> _allItem = new List<MonoBehaviour>();
        private bool _viewDirty = false;
        
        public AttributeViewTitle AddTitle()
        {
            var title = CreateTitle();

            _allItem.Add(title);
            _viewDirty = true;
            
            return title;
        }
        
        public AttributeViewProperty AddProperty()
        {
            var property = CreateProperty();
            
            _allItem.Add(property);
            _viewDirty = true;
            
            return property;
        }
        
        public void Clear()
        {
            foreach (var item in _allItem)
            {
                Destroy(item.gameObject);
            }
            
            _allItem.Clear();
        }
        
        private void Update()
        {
            if (_viewDirty)
            {
                _viewDirty = false;
                
                RePosition();
            }
        }

        private float _currentY;
        private void RePosition()
        {
            var propertyCounter = 0;
            foreach (var item in _allItem)
            {
                item.transform.localPosition = new Vector3(0, _currentY);

                if (item is AttributeViewTitle)
                {
                    _currentY -= _lineSpaceTitle;
                }

                if (item is AttributeViewProperty)
                {
                    _currentY -= _lineSpaceProperty;
                    (item as AttributeViewProperty).SetBgEnabled(propertyCounter % 2 == 0);
                    propertyCounter++;
                }
                else
                {
                    propertyCounter = 0;
                }
            }
            
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, -_currentY);
        }
        
        private void Awake()
        {
            _titleTemplate.gameObject.SetActive(false);    
            _propertyTemplate.gameObject.SetActive(false);    
        }
        
        private AttributeViewTitle CreateTitle()
        {
            var go = Instantiate(_titleTemplate.gameObject, _content);
            go.SetActive(true);
            return go.GetComponent<AttributeViewTitle>();
        }
        
        private AttributeViewProperty CreateProperty()
        {
            var go = Instantiate(_propertyTemplate.gameObject, _content);
            go.SetActive(true);
            return go.GetComponent<AttributeViewProperty>();
        }
    }
}