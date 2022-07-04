using UnityEngine;
using UnityEngine.UI;

namespace MFramework
{
	public partial class Panel1 
	{
		[SerializeField] private string m_ScriptsFolderPath;
		[SerializeField] private bool IsSpawnPrefab = false;
		[SerializeField] private string m_PrefabFolderPath;
		public GameObject m_Btn1;
		public Transform btn1Trans;
		public GameObject m_Txt1;
		public Transform txt1Trans;
		public GameObject m_Img1;
		public Transform img1Trans;
	}
}
