using System.Collections.Generic;
using Features.Popup.Application;
using UnityEngine;

namespace Features.Popup.Main
{
    public static class PopupManager
    {
        private static readonly Stack<IPopup> popups = new Stack<IPopup>();

        public static T ShowPopup<T>(T popupPrefab, Transform parent) where T : Object, IPopup
        {
            var popup = Object.Instantiate(popupPrefab, parent);
            popups.Push(popup);
            return popup;
        }

        public static void CloseLastPopup()
        {
            var lastPopup = popups.Pop();
            lastPopup.Close();
        }

        public static void CloseAll()
        {
            for (var i = 0; i < popups.Count; i++)
            {
                var popup = popups.Pop();
                popup.Close();
            }
        }
    }
}