using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTipSystem : MonoBehaviour
{
	[SerializeField]
	private Color BGColor = Color.white;
	[SerializeField]
	private Color textColor = Color.black;
	[SerializeField]
	private TextMeshProUGUI boxText;
	[SerializeField]
	private int fontSize = 24;
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private RectTransform box;
	[SerializeField]
	private float speed = 10;

	private string text;
	private Image img;
	private Color BGColorFade;
	private Color textColorFade;

	void Awake()
    {
		img = box.GetComponent<Image>();
		BGColorFade = BGColor;
		BGColorFade.a = 0;
		textColorFade = textColor;
		textColorFade.a = 0;
		img.color = BGColorFade;
		boxText.color = textColorFade;
		boxText.alignment = TextAlignmentOptions.Center;
	}

    void Update()
    {
		bool show = false;
		boxText.fontSize = fontSize;

		int nonDraggableMask = 1 << 7;
		int outputMask = 1 << 8;
		int inputMask = 1 << 9;
		int graphMask = 1 << 10;

		int layerMask = nonDraggableMask | inputMask | outputMask | graphMask;

		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 0;
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 100, layerMask);
		if (hit.transform != null)
		{
			//Debug.Log(hit.transform.name);
			if (hit.transform.GetComponent<ToolTipObject>())
			{
				text = hit.transform.GetComponent<ToolTipObject>().Text;
				show = true;
			}
		}

		boxText.text = text;

		if (show)
		{
			float curY = (Input.mousePosition.y + 2) * (1920F / Screen.width);
			float curX = (Input.mousePosition.x + 2) * (1920F / Screen.width);

			//Debug.Log($"{Screen.height}, {Screen.width}");
			//Debug.Log($"Позиция мыши по y: {Input.mousePosition.y}, позиция мыши итог: {curY}");

			box.anchoredPosition = new Vector2(curX, curY);

			img.color = Color.Lerp(img.color, BGColor, speed * Time.deltaTime);
			boxText.color = Color.Lerp(boxText.color, textColor, speed * Time.deltaTime);
		}
		else
		{
			img.color = Color.Lerp(img.color, BGColorFade, speed * Time.deltaTime);
			boxText.color = Color.Lerp(boxText.color, textColorFade, speed * Time.deltaTime);
		}
	}
}
