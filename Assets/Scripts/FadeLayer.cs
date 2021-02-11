using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeLayer : MonoBehaviour {

	public GameObject spinEffect;
	public GameObject message;
	public float fadeDuration = 0.3f;
	public float fadeInValue = 0.5f;
	public float fadeOutValue = 0.0f;

	private TweenCallback fadeComplite;

	public void FadeOnEffect(Sprite slotSprite, TweenCallback fadeComplite) {
		this.fadeComplite = fadeComplite;
		gameObject.SetActive(true);
		spinEffect.SetActive(true);
		GetComponent<Image>()
			.DOFade(fadeInValue, fadeDuration)
			.OnComplete(FadeOutEffect);

		Image spinSprite = spinEffect.GetComponent<Image>();
		spinSprite.sprite = slotSprite;
		spinSprite.DOFade(fadeInValue, fadeDuration);
	}

	private void FadeOutEffect() {
		GetComponent<Image>()
			.DOFade(fadeOutValue, fadeDuration)
			.OnComplete(Deactivate);
		Image spinSprite = spinEffect.GetComponent<Image>();
		spinSprite.DOFade(fadeOutValue, fadeDuration)
			.OnComplete(fadeComplite);
	}
	
	public void FadeWinMessage() {
		gameObject.SetActive(true);
		GetComponent<Image>()
			.DOFade(fadeInValue, fadeDuration);
		TextMeshProUGUI text = message.GetComponent<TextMeshProUGUI>();
		message.SetActive(true);
		DOTween.ToAlpha(() => text.color, y => text.color = y, fadeInValue, fadeDuration);
	}
	private void Deactivate() {
		gameObject.SetActive(false);
		spinEffect.SetActive(false);
		message.SetActive(false);
	}
}
