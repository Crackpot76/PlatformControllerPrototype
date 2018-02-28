using UnityEngine;
using Cinemachine;
using System.Collections;

public class Cam : MonoBehaviour {

    const float TEXTURE_SIZE = 24f;
	private float lastHeight = 0;
	private CinemachineVirtualCamera cam;
    public AnimationCurve curveShake;

    // Use this for initialization
    void Start () {
		cam = GetComponent<CinemachineVirtualCamera>();

        UpdateOrthographicSize();
        
	}

    public IEnumerator ShakeCoroutine(float amplitude, float frequency, float time) {
        CinemachineBasicMultiChannelPerlin noiseSettings = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noiseSettings) {
            noiseSettings.m_AmplitudeGain = amplitude;
            noiseSettings.m_FrequencyGain = frequency;
            yield return new WaitForSeconds(time);
            noiseSettings.m_AmplitudeGain = 0.0f;
            noiseSettings.m_FrequencyGain = 0.0f;
        }
       
    }


	
	// Update is called once per frame
	void Update () {
        UpdateOrthographicSize();

    }


    private void UpdateOrthographicSize() {

        if (lastHeight != Screen.height) {
            lastHeight = Screen.height;

            float scale = 4f;
            if (Screen.height < 1080) {
                scale = 3f;
            }
            if (Screen.height < 768) {
                scale = 2f;
            }
            if (Screen.height < 512) {
                scale = 1f;
            }
            
            float erg = (Screen.height / (TEXTURE_SIZE * 2f)) / scale;

            cam.m_Lens.OrthographicSize = erg;
        }
    }
	

}
