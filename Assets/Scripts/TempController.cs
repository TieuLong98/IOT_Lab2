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

public class TempController : MonoBehaviour
{
	Slider slider;
	GameObject Yo;
	string Temp;
	int tmp_next;
	int tmp_cur;
	
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Yo = GameObject.Find("M2MQTT");
    }

    // Update is called once per frame
    void Update()
    {
        Temp = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>().temp;
        bool successfullyParsed = int.TryParse(Temp, out tmp_next);
        if(successfullyParsed){
        	if(tmp_cur < tmp_next)tmp_cur++;
        	else if (tmp_cur > tmp_next)tmp_cur--;
        	else tmp_cur = tmp_next;
        		slider.value = tmp_cur;
    	}
    }
}
