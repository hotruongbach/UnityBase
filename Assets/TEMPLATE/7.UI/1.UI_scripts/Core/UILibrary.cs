using System;
using System.Collections.Generic;
using UnityEngine;
namespace Monster.UI
{
    [CreateAssetMenu(fileName = "UILibrary", menuName = "Game/UILibrary")]
    public class UILibrary : ScriptableObject
    {
        public List<UIData> ListView = new List<UIData>();
        public List<UIData> ListPopup = new List<UIData>();

        private void OnValidate()
        {
            for (int i = 0; i < ListView.Count; i++)
            {
                ListView[i].name = ListView[i].view.name;
            }

            for (int i = 0; i < ListPopup.Count; i++)
            {
                if (ListPopup[i] != null)
                {
                    if (ListPopup[i].view == null) continue;
                    ListPopup[i].name = ListPopup[i].view.name;
                }
            }
        }

        public T GetView<T>() where T : Window
        {
            foreach (var view in ListView)
            {
                if (view.view is T tView)
                {
                    return tView;
                }
            }
            Bug.Log($"Popup {nameof(T)} is missing!","red");
            return null;
        }
        public T GetPopup<T>() where T : Window
        {
            foreach (var view in ListPopup)
            {
                if (view.view is T tView)
                {
                    return tView;
                }
            }
            Bug.Log($"Popup {nameof(T)} is missing!", "red");
            return null;
        }

        public void ScanForMissing()
        {
            foreach(var view in ListPopup)
            {
                if(view.view == null)
                {
                    Bug.Log($"Popup {view.name} is missing!", "red");
                }
            }
            foreach(var view in ListView)
            {
                if (view.view == null)
                {
                    Bug.Log($"View {view.name} is missing!", "red");
                }
            }

            ListPopup.RemoveAll(view => view.view == null);
            ListView.RemoveAll(view => view.view == null);
        }
    }

    [Serializable]
    public class UIData
    {
        [HideInInspector] public string name;
        public Window view;
    }
}

