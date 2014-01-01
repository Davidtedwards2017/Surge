using UnityEngine;
using System.Collections;

namespace Surge.Controllers
{

	public class MusicController : MonoBehaviour {

        public delegate void BeatDetectedEventHandler(int subband);
        public event BeatDetectedEventHandler BeatDetectedEvent;

		//public members
		public int SubbandSize = 32;
		public int SampleSize = 1024;
		public AudioSource audioSource;

		public float BeatDetectionConstant;

		//private members
		private float[] subbands;
		private float[,] subbandHistory;
		private float[] frequencyData;


        void onBeatDetected(int subband)
        {
            if (BeatDetectedEvent != null)
                BeatDetectedEvent(subband);
        }

		// Use this for initialization
		void Start () {
			InitalizeFrequencyData();
		}
		
		// Update is called once per frame
		void Update () {
			CheckSubband();
		}

		private void InitalizeFrequencyData()
		{
			frequencyData = new float[SampleSize];
			int n = frequencyData.Length;
			int k = 0;
			
			for( int j = 0; j < frequencyData.Length; j++)
			{
				n /= 2;
				if(n == 0) break;
				k++;
			}
			subbands = new float[k+1];
			subbandHistory = new float[k+1,43];
			
			Debug.Log("Number of subbands " + subbands.Length);
		}

		private void CheckSubband()
		{
			audioSource.GetSpectrumData(frequencyData,0, FFTWindow.Rectangular);
			
			int k = 0;
			int crossover = 2;
			
			for( int i = 0; i < frequencyData.Length; i++)
			{
				var d = frequencyData[i];
				var b = subbands[k];
				
				subbands[k] = (d>b)? d:b; //find the max as the peak value in that frequencey band.
				if(i > crossover-3)
				{
					k++;
					crossover *= 2;
					
					if(CheckForBeat(k, subbands[k]))
					{
						//Debug.Log("Beat on subband " + k);	
                        onBeatDetected(k);
					}
					
					subbands[k] = 0;
				}
			}
		}
		
		
		private bool CheckForBeat(int subband, float value)
		{
			float EnergyAvg = 0.0f;
			
			//push new value into history buffer
			
			for( int i = 42; i > 0; i--)//shift history buffer over 1 spot
			{
				EnergyAvg += subbandHistory[subband,i];
				subbandHistory[subband, i] = subbandHistory[subband, i-1];
			}
			
			subbandHistory[subband,0] = value;
			EnergyAvg /= 42;
			
			return ( value > EnergyAvg * BeatDetectionConstant) ? true : false;
			
		}
	}
}