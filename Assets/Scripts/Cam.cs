using UnityEngine;
using Cinemachine;
using System.Collections;

public class Cam : MonoBehaviour {

	
	private float lastHeight = 0;
	private CinemachineVirtualCamera cam;
	// Use this for initialization
	void Start () {
		cam = GetComponent<CinemachineVirtualCamera>();
        if (cam) {
            Debug.Log("GEFUNDEN!");

            // Shake
            CinemachineBasicMultiChannelPerlin test = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (test) {
                Debug.Log("Gefunden Component!");
                //test.m_AmplitudeGain = 0.05f;
                //test.m_FrequencyGain = 0.1f;
            }
        }
        
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
			
			Debug.Log("Erg:" + erg);

            cam.m_Lens.OrthographicSize = erg;
            

            //Camera.main.orthographicSize = erg;
            //Debug.Log("Act:" + Camera.main.orthographicSize);

            //Camera.main.orthographicSize = 5.73f;
        }	
	}
	

}
