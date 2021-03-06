﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour {

	public float timeToOpaque = 2.0f;
	public delegate void simpleDel();
	public simpleDel Callback = delegate {};

	private Image Image2Fade;

	// Use this for initialization
	void Start () {
		if (Image2Fade == null)
			Image2Fade = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		Color tempColor = Image2Fade.color;
		tempColor.a = tempColor.a + (Time.deltaTime / timeToOpaque);
		Image2Fade.color = tempColor;
		if(Image2Fade.color.a >= 1.0f)
		{
			tempColor.a = 1.0f;
			Image2Fade.color = tempColor;
			Destroy(this);
		}
	}
}
