using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class AttributeViewProperty : MonoBehaviour
    {
        [SerializeField] private Text _textPropertyName;
        [SerializeField] private Text _textPropertyValue;
        [SerializeField] private Image _imageBg;
        public void SetData(string name, string value)
        {
            _textPropertyName.text = name;
            _textPropertyValue.text = value;
        }

        public void SetBgEnabled(bool enabled)
        {
            _imageBg.enabled = enabled;
        }
    }
}