using UnityEngine;
using UnityEngine.UI;
 
#if UNITY_EDITOR
using TMPro;
using UnityEditor;
#endif

namespace LubyLib.UIExtras.MultiGraphicButton
{
	[RequireComponent(typeof(MultiTargetGraphics), typeof(RectTransform))]
	public class MultiGraphicButton : Button
	{
#if UNITY_EDITOR
		[MenuItem(itemName: "GameObject/UI/Multi Image Button", isValidateFunction: false, priority: 30)]
		public static void Create()
		{
			var parent = Selection.activeTransform;
			if (parent == null)
			{
				var canvas = FindObjectOfType<Canvas>().transform;
				if (canvas != null)
				{
					parent = canvas.transform;
				}
			}
			GameObject go = new GameObject("Multi Image Button");
			go.transform.SetParent(parent, false);

			var rect = go.AddComponent<RectTransform>();

			rect.sizeDelta = new Vector2(160, 30);

			var button = go.AddComponent<MultiGraphicButton>();
			var graphics = go.GetComponent<MultiTargetGraphics>();

			GameObject backgroundGO = new GameObject("Background");
			GameObject textGO = new GameObject("Text (TMP)");
			backgroundGO.transform.SetParent(go.transform, false);
			textGO.transform.SetParent(go.transform, false);
			
			

			var backgroundImg = backgroundGO.AddComponent<Image>();
			var textTMP = textGO.AddComponent<TextMeshProUGUI>();
			backgroundImg.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Luby/UIExtras/MultiGraphicButton/Sprites/Background.png");

			backgroundImg.rectTransform.anchorMin = Vector2.zero;
			backgroundImg.rectTransform.anchorMax = Vector2.one;
			backgroundImg.rectTransform.offsetMax = Vector2.zero;
			backgroundImg.rectTransform.offsetMin = Vector2.zero;
			backgroundImg.type = Image.Type.Tiled;
			backgroundImg.pixelsPerUnitMultiplier = 5;

			textTMP.rectTransform.anchorMin = Vector2.zero;
			textTMP.rectTransform.anchorMax = Vector2.one;
			textTMP.rectTransform.offsetMax = Vector2.zero;
			textTMP.rectTransform.offsetMin = Vector2.zero;
			textTMP.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
			textTMP.text = "Button";
			textTMP.alignment = TextAlignmentOptions.Midline;
			textTMP.fontSize = 24;

			graphics.targetGraphics = new Graphic[] {backgroundImg, textTMP};
		}

#endif

		private Graphic[] graphics;

		private MultiTargetGraphics targetGraphics;

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			//get the graphics, if it could not get the graphics, return here
			if (!GetGraphics())
				return;

			var targetColor =
				state == SelectionState.Disabled ? colors.disabledColor :
				state == SelectionState.Highlighted ? colors.highlightedColor :
				state == SelectionState.Normal ? colors.normalColor :
				state == SelectionState.Pressed ? colors.pressedColor :
				state == SelectionState.Selected ? colors.selectedColor : Color.white;

			foreach (var graphic in graphics)
				graphic.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
		}

		private bool GetGraphics()
		{
			if (!targetGraphics) targetGraphics = GetComponent<MultiTargetGraphics>();
			graphics = targetGraphics?.targetGraphics;
			return graphics != null && graphics.Length > 0;
		}
	}
}