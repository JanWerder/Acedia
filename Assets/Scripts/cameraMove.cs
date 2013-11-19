using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
	
	int qSamples  = 1024;  // array size
	float refValue  = 0.1f; // RMS value for 0 dB
	float threshold = 0.02f;      // minimum amplitude to extract pitch
	float rmsValue;   // sound level - RMS
	float dbValue;    // sound level - dB
	float pitchValue; // sound pitch - Hz
	
	public float DBMulti = 10; //Multiplier for the camera movement
	private float[] samples ; // audio samples
	private float[] spectrum; // audio spectrum
	
	private GameObject txtIngameScore;
	public bool isRunning;
	SceneData sc;
	
	void Start () {
	isRunning = true;
	samples = new float[qSamples];
    spectrum = new float[qSamples];
	txtIngameScore = (GameObject)Instantiate(Resources.Load("txtIngameScore"),new Vector3(1,0,0),Quaternion.identity);
	sc = SceneData.GetInstance();
	}
	
	void AnalyzeSound(){
    audio.GetOutputData(samples, 0); // fill array with samples
    int i;
    float sum = 0;
    for (i=0; i < qSamples; i++){
       sum += samples[i]*samples[i]; // sum squared samples
    }
    rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
    dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
    if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
    // get sound spectrum (references audiosource of camera)
    audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    float maxV = 0;
    int maxN = 0;
    for (i=0; i < qSamples; i++){ // find max 
       if (spectrum[i] > maxV && spectrum[i] > threshold){
         maxV = spectrum[i];
         maxN = i; // maxN is the index of max
       }
    }
    double freqN = maxN; // pass the index to a float variable
    if (maxN > 0 && maxN < qSamples-1){ // interpolate index using neighbours (fucked up sound shit)
       double dL = spectrum[maxN-1]/spectrum[maxN]; 
       double dR = spectrum[maxN+1]/spectrum[maxN];
       freqN += 0.5*(dR*dR - dL*dL);
    }
    pitchValue = (float)freqN*24000/qSamples; // convert index to frequency
	}
	
	// Update is called once per frame
	void Update () {
	AnalyzeSound();
	

	//Update score
	if (isRunning){
	sc.roundScore += (int)(Time.timeSinceLevelLoad/3);
	txtIngameScore.guiText.text = sc.roundScore.ToString();
	}
	
	//Hard Y-Value to bypass shacky camera when moving 
	Vector3 playerPos = SceneData.GetInstance().goPlayer.transform.position;
	playerPos.y = -5;
	
	//Focus camera on the player and move the camera to the beat
	transform.LookAt(playerPos);
    transform.Translate(Vector3.right * Time.deltaTime * (rmsValue*DBMulti));
	}
}
