using UnityEngine;
using System.Collections.Generic;

public class GUIUtils
{
    /// <summary>
    /// Tries to find a style in the current GUI skin. If found, it is returned.
    /// If not, the default is returned.
    /// 
    /// This is a convenience method for readability.
    /// </summary>
    /// <param name="name">The name of the style to find.</param>
    /// <param name="def">The default to use if the requested style is not found</param>
    /// <returns>Some GUIStyle</returns>
    public static GUIStyle FindStyleOrDefault(string name, GUIStyle def)
    {
        GUIStyle requested = GUI.skin.FindStyle(name);

        if (requested != null)
            return requested;

        return def;
    }


    /// <summary>
    /// Draw a label.
    /// </summary>
    /// <param name="textArea">The designated area to draw in</param>
    /// <param name="text">The text to draw</param>
    /// <param name="style">The style to draw with</param>
    /// <param name="alignRight">if true, align the text to the right border of the draw area</param>
    /// <returns>The area actualy used for drawing</returns>
    public static Rect DrawLabel(Rect textArea, string text, GUIStyle style, bool alignRight)
    {
        Vector2 textSize = style.CalcSize(new GUIContent(text));

        if (alignRight)
        {
            // Offset the left corner
            textArea.x = textArea.x + textArea.width - textSize.x;
        }

        textArea.width = Mathf.Min(textArea.width, textSize.x);
        textArea.height = Mathf.Min(textArea.height, textSize.y);

        GUI.Label(textArea, text, style);

        return textArea;
    }
}
