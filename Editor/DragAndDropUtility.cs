#if UNITY_2021_2_OR_NEWER

using Packages.GradientTextureGenerator.Runtime;
using UnityEditor;
using UnityEngine;

namespace Packages.GradientTextureGenerator.Editor
{
    /// <summary>
    /// Drag and drop implementation for <see cref="GradientTexture"/> to move it directly onto Texture2D slots.
    /// </summary>
    public static class DragAndDropUtility
    {
#if UNITY_6000_3_OR_NEWER
        private static DragAndDrop.ProjectBrowserDropHandlerV2 projectHandler;
#else
        private static DragAndDrop.ProjectBrowserDropHandler projectHandler;
#endif
        
        [InitializeOnLoadMethod]
        public static void Init()
        {
            projectHandler = ProjectDropHandler;
#if UNITY_6000_3_OR_NEWER
            DragAndDrop.RemoveDropHandlerV2(projectHandler);
            DragAndDrop.AddDropHandlerV2(projectHandler);
#else
            DragAndDrop.RemoveDropHandler(projectHandler);
            DragAndDrop.AddDropHandler(projectHandler);
#endif
        }

#if UNITY_6000_3_OR_NEWER
        private static DragAndDropVisualMode ProjectDropHandler(UnityEngine.EntityId dragInstanceId, string dropUponPath, bool perform)
#else
        private static DragAndDropVisualMode ProjectDropHandler(int dragInstanceId, string dropUponPath, bool perform)
#endif
        {
            if (!perform)
            {
                Object[] dragged = DragAndDrop.objectReferences;
                bool found = false;
                
                for (int i = 0; i < dragged.Length; i++)
                {
                    if (dragged[i] is GradientTexture gradient)
                    {
                        dragged[i] = gradient.Texture;
                        found = true;
                    }
                }
                
                if (!found)
                {
                    return default;
                }
                
                DragAndDrop.objectReferences = dragged;
                GUI.changed = true;
                
                return default;
            }
            
            return default;
        }
    }
}

#endif
