using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    // Use this for initialization
    public Text score;

    public void SetScore(int value)
    {
        score.text = value.ToString();
    }
	
}
