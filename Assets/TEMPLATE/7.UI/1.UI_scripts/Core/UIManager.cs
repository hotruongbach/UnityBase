using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace Monster.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [DisplayInspector]
        [SerializeField]
        UILibrary library;

        //[SerializeField] GameConfig gameConfig;
        [SerializeField]
        CanvasScaler scaler;

        [SerializeField]
        RectTransform MenuLayer;

        [SerializeField]
        RectTransform PopupLayer;

        [SerializeField]
        PushNotificationManager pushNotification;

        //[SerializeField] LoadingScreen LoadingScreen;

        Dictionary<string, Window> m_Views = new Dictionary<string, Window>();
        Dictionary<string, Window> m_PopUps = new Dictionary<string, Window>();
        public Window m_CurrentView { get; set; }
        readonly Stack<Window> m_viewHistory = new Stack<Window>();
        readonly Stack<Window> m_popupHistory = new Stack<Window>();

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            // InitializeSingleton(true);
        }

        void Start()
        {
            m_viewHistory.Clear();
            pushNotification.Init();
        }

        public void ClearHistory()
        {
            m_viewHistory.Clear();
        }

        /// <summary>
        /// Finds the first registered UI View of the specified type
        /// </summary>
        /// <typeparam name="T">The View class to search for</typeparam>
        /// <returns>The instance of the View of the specified type. null if not found </returns>
        public T GetView<T>()
            where T : Window
        {
            if (m_Views.TryGetValue(typeof(T).ToString(), out Window view))
            {
                return view as T;
            }
            return null;
        }

        /// <summary>
        /// Finds the View of the specified type and makes it visible
        /// </summary>
        // <param name="keepInHistory">Pushes the current View to the history stack in case we want to go back to</param>
        /// <typeparam name="T">The View class to search for</typeparam>
        /// <param name="onComplete"> Call when animation ui run complete</param>
        public T ShowView<T>(
            // bool keepInHistory = true,
            Action onComplete = null,
            object param = null
        )
            where T : Window
        {
            CloseAllPopup();

            // Check if view already exists
            if (m_Views.TryGetValue(typeof(T).ToString(), out Window existingView))
            {
                Show(existingView, existingView.keepHistory, onComplete, param);
                return existingView as T;
            }

            // Create new view if not found
            var newView = Instantiate(library.GetView<T>(), MenuLayer);
            m_Views.Add(typeof(T).ToString(), newView); // add properly with key

            Show(newView, newView.keepHistory, onComplete, param);
            return newView as T;
        }

        /// <summary>
        /// Makes a View visible and hides others
        /// </summary>
        /// <param name="view">The view</param>
        /// <param name="keepInHistory">Pushes the current View to the history stack in case we want to go back to</param>
        public void Show(
            Window view,
            bool keepInHistory = true,
            Action onComplete = null,
            object param = null
        )
        {
            ClosePopup();

            if (m_CurrentView == view)
            {
                view.OnReveal(onComplete, param);
                return;
            }

            if (m_CurrentView != null)
            {
                if (keepInHistory)
                {
                    m_viewHistory.Push(m_CurrentView);
                }
                m_CurrentView.HideImmediate();
            }

            view.Show(onComplete, param);
            m_CurrentView = view;
        }

        /// <summary>
        /// Goes to the page visible previously
        /// </summary>
        public void GoBack()
        {
            if (m_viewHistory.Count != 0)
            {
                Show(m_viewHistory.Pop(), false);
            }
        }

        public T ShowPopup<T>(Action onComplete = null, object param = null)
            where T : Window
        {
            // Try to find popup in dictionary by type key string
            if (m_PopUps.TryGetValue(typeof(T).ToString(), out Window existingPopup))
            {
                if (existingPopup.keepHistory)
                {
                    if (existingPopup.IsShowing)
                    {
                        if (m_popupHistory.Count == 0 || m_popupHistory.Peek() != existingPopup)
                        {
                            // Rebuild popup history without duplicates and put this popup on top
                            var list = m_popupHistory.ToList();
                            m_popupHistory.Clear();
                            foreach (var popup in list)
                            {
                                if (popup != existingPopup)
                                {
                                    m_popupHistory.Push(popup);
                                }
                            }
                            m_popupHistory.Push(existingPopup);
                        }
                        existingPopup.OnReveal(onComplete, param);
                    }
                    else
                    {
                        m_popupHistory.Push(existingPopup);
                        existingPopup.Show(onComplete, param);
                    }
                }
                else
                {
                    if (existingPopup.IsShowing)
                    {
                        existingPopup.OnReveal(onComplete, param);
                    }
                    else
                    {
                        existingPopup.Show(onComplete, param);
                    }
                }

                return existingPopup as T;
            }

            //create new
            var newPopup = Instantiate(library.GetPopup<T>(), PopupLayer);
            m_PopUps.Add(typeof(T).ToString(), newPopup);

            newPopup.Show(onComplete, param);
            if (newPopup.keepHistory)
            {
                m_popupHistory.Push(newPopup);
            }
            return newPopup as T;
        }

        public void ClosePopup(Action onComplete = null)
        {
            if (m_popupHistory.Count > 0)
                m_popupHistory
                    .Pop()
                    .Hide(() =>
                    {
                        onComplete?.Invoke();
                        if (m_popupHistory.Count == 0)
                        {
                            m_CurrentView.OnReveal();
                        }
                        else
                        {
                            m_popupHistory.Peek()?.OnReveal();
                        }
                    });
        }

        public void CloseAllPopup(bool onlyHistory = true, Action onComplete = null)
        {
            onComplete?.Invoke();
            if (onlyHistory)
            {
                foreach (var popup in m_popupHistory)
                {
                    if (popup.IsShowing)
                        popup.HideImmediate();
                }
                m_popupHistory.Clear();
            }
            else
            {
                foreach (var popupKV in m_PopUps)
                {
                    if (popupKV.Value.IsShowing)
                        popupKV.Value.HideImmediate();
                }
                m_popupHistory.Clear();
            }
        }

        public void ClosePopup<T>(Action onComplete = null)
            where T : Window
        {
            if (m_PopUps.TryGetValue(typeof(T).ToString(), out Window existingPopup))
            {
                if (existingPopup.keepHistory)
                {
                    if (m_popupHistory.Count == 0)
                    {
                        return;
                    }
                    if (m_popupHistory.Peek() is T)
                    {
                        m_popupHistory.Pop().Hide(onComplete);
                        if (m_popupHistory.Count == 0)
                        {
                            m_CurrentView.OnReveal();
                        }
                        else
                        {
                            m_popupHistory.Peek()?.OnReveal();
                        }
                    }
                    else
                    {
                        Bug.Log("Some popup is showing above this!");
                    }
                }
                else
                {
                    existingPopup.HideImmediate();
                }
            }
            else
            {
                Bug.Log("Popup not in list!");
            }
        }

        public bool IsAnyPopupShowing()
        {
            foreach (var popup in m_PopUps)
            {
                if (popup.Value.IsShowing)
                {
                    return true;
                }
            }
            return false;
        }

        public void PushNoti(string message)
        {
            pushNotification.PushNewNotification(message);
        }
    }
}
