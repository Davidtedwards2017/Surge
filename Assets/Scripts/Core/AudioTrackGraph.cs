using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

namespace Surge.Core
{
     
    public class AudioTrackGraph : MonoBehaviour {

        public Color GraphColor = Color.green;
        public AudioSource source;
        public Material TimeSliderMat;

        public float SliderPercent;

        public Vector2 graphLocation;
        public float GraphHeight;
        public float GraphYPlacement;
        public float WidthPadding;

        private float LeftEdge;
        private float RightEdge;
        private float TopEdge;
        private float BottomEdge;

        private float SliderPosX;
        private VectorLine TimeSlider;

      
    	// Use this for initialization
    	

        void Start () {
            InitalizeGraph();
       	}
    	void InitalizeGraph()
        {

            TimeSlider = VectorLine.SetLine(Color.blue, new Vector2(LeftEdge, TopEdge), new Vector2(LeftEdge,TopEdge));

            RightEdge = Screen.width - WidthPadding;
            LeftEdge = WidthPadding;
            TopEdge = GraphYPlacement + (GraphHeight /2);
            BottomEdge = GraphYPlacement - (GraphHeight /2);


            VectorLine.SetLine(GraphColor,new Vector2(LeftEdge,TopEdge),new Vector2(RightEdge, TopEdge));
            VectorLine.SetLine(GraphColor,new Vector2(RightEdge,TopEdge),new Vector2(RightEdge, BottomEdge));
            VectorLine.SetLine(GraphColor,new Vector2(RightEdge,BottomEdge),new Vector2(LeftEdge, BottomEdge));
            VectorLine.SetLine(GraphColor,new Vector2(LeftEdge,BottomEdge),new Vector2(LeftEdge, TopEdge));

        }

    	// Update is called once per frame
    	void Update () {
            SliderPercent = GetAudioSourcePercent();
            SetSlider(SliderPercent);
    	}

        public float GetAudioSourcePercent()
        {
            if(source == null)
                return 0.0f;

            float length = source.clip.length;
            float current = source.time;

            return (current / length) * 100;

        }

        public void SetSlider(float percent)
        {
            if(percent < 0 || percent > 100)
                return;

            float offset = RightEdge - LeftEdge;
            offset *= (percent / 100);
            offset += LeftEdge;

            TimeSlider.points2[0] = new Vector2(offset, TopEdge);
            TimeSlider.points2[1] = new Vector2(offset, BottomEdge);
            TimeSlider.Draw();

        }
    }
}