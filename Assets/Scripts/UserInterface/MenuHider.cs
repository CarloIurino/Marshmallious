using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (CanvasGroup))]
public class MenuHider : MonoBehaviour {
	CanvasGroup canvasGroup;

	[SerializeField]
	bool isVisible;
	[SerializeField]
	float smoothTimeMultipler;

	bool isShowing = false;
	bool isHiding = false;

	float delta;

	public bool IsVisible{ get; set; }

	void Awake(){
		canvasGroup = GetComponent<CanvasGroup> ();

		IsVisible = isVisible;

		if (isVisible) {
			canvasGroup.alpha = 1;
			canvasGroup.blocksRaycasts = true;
			canvasGroup.interactable = true;
		} else{
			canvasGroup.alpha = 0;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
		}

	}
		

	public void Show(){
		if (IsVisible)
			return;

		isHiding = false;
		isShowing = true;

		delta = 0;
	}

	void Showing(){
		if (!isShowing)
			return;

		canvasGroup.alpha = Mathf.Lerp (0, 1, delta*smoothTimeMultipler);

		delta += Time.deltaTime;

		if (canvasGroup.alpha == 1) {
			canvasGroup.blocksRaycasts = true;
			canvasGroup.interactable = true;

			IsVisible = true;
			isShowing = false;
		}
	}

	void Hiding(){
		
		if (!isHiding)
			return;

		canvasGroup.alpha = Mathf.Lerp (1, 0, delta*smoothTimeMultipler);

		delta += Time.deltaTime;

		if (canvasGroup.alpha == 0) {
			IsVisible = false;
			isHiding = false;
		}
	}

	public void Hide(){
		if (!IsVisible)
			return;

		canvasGroup.blocksRaycasts = false;
		canvasGroup.interactable = false;

		delta = 0;
		isShowing = false;
		isHiding = true;
	}

	void Update(){
		Showing ();
		Hiding ();
	}

}
