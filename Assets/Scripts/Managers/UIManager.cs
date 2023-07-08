using GMTK_2023.UI;
using GMTK_2023.UI.Elements;
using GMTK_2023.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2023.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; } = null;

        public string ActiveViewName => m_activeViewName;

        [SerializeField]
        private SerializableKeyValuePair<string, UIViewBase>[] m_views;
        [SerializeField, Tooltip("Set as empty string to hide all views on start")]
        private string m_initialView = "GameUI";

        private string m_activeViewName = "";
        private Dictionary<string, UIViewBase> m_viewsDict;

        public void SetView(string name)
        {
            if (m_activeViewName.Length > 0)
            {
                m_viewsDict[m_activeViewName].Hide();
            }

            m_activeViewName = name;
            if (name.Length > 0)
            {
                m_viewsDict[name].Show();
            }
            else
            {
                HideAllViews();
            }
        }

        public void HideAllViews()
        {
            for (int i = 0; i < m_views.Length; i++)
            {
                m_views[i].value.Hide();
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {
            m_viewsDict = new();
            for (int i = 0; i < m_views.Length; i++)
            {
                var kp = m_views[i];
                m_viewsDict[kp.key] = kp.value;
            }

            SetView(m_initialView);
        }
    }
}
