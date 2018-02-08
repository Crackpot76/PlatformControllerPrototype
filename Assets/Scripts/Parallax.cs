﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Transform[] backgrounds;    
    public float smoothing;

    private float[] parallaxScales;
    private Transform cam;
    private Vector3 previousCamPosition;

	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
        previousCamPosition = cam.position;

        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;

            Debug.Log(parallaxScales[i]);
        }
    }

    // Update is called once per frame
    void LateUpdate() {
       // Debug.Log("In LateUpdate!" + previousCamPosition.x + " - " + cam.position.x);
        for (int i = 0; i < backgrounds.Length; i++) {
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];
           // Debug.Log("Parallax: " + parallax);

            float newBackgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 newBackgroundPos = new Vector3(newBackgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, newBackgroundPos, smoothing * Time.deltaTime);
        }
        previousCamPosition = cam.position;
    }

 }