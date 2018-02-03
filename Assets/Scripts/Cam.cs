using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	
	private float lastHeight = 0;
	//private Camera cam;
	// Use this for initialization
	void Start () {
		//cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastHeight != Screen.height)
		{
			lastHeight = Screen.height;
			//Debug.Log("ScreenHeight=" + lastHeight);
			float textureSize = 25f;
            //float unitsPerPixel = 1f/textureSize;
            //Debug.Log("UnitsPerPixel:" + unitsPerPixel);
            float erg = (lastHeight / (textureSize * 2f)) / 3f;
			
			erg = erg * 100;
			if (erg % 2 != 0) {
				erg ++;
			}
			erg = erg / 100f;
//			erg = erg * 10;
//			erg = Mathf.Round(erg);
//			erg = erg / 10f;
			
			//Debug.Log("Erg:" + erg);
			Camera.main.orthographicSize = erg;
			
			
			//Camera.main.orthographicSize = 5.73f;
		}	
	}
	

}
