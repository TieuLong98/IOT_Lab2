using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using Newtonsoft.Json.Linq;
using DG.Tweening;

public class HumidController : MonoBehaviour
{
	Slider slider;
	GameObject Yo;
	string Humid;
	int hud_next;
	int hud_cur;
	
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Yo = GameObject.Find("M2MQTT");
    }

    // Update is called once per frame
    void Update()
    {
        Humid = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>().humid;
        bool successfullyParsed = int.TryParse(Humid, out hud_next);
        if(successfullyParsed){
        	if(hud_cur < hud_next)hud_cur++;
        	else if (hud_cur > hud_next)hud_cur--;
        	else hud_cur = hud_next;
        		slider.value = hud_cur;
    	}
    }
}
