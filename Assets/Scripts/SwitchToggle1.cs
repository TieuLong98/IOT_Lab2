using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;
using System;
using System.Collections;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class SwitchToggle1 : MonoBehaviour {
   [SerializeField] RectTransform uiHandleRectTransform ;
   [SerializeField] Color background1ActiveColor ;
   [SerializeField] Color handle1ActiveColor ;
   	string button2_next;
	string button2_current;
	GameObject Yo;

   Image background1Image, handle1Image ;

   Color background1DefaultColor, handle1DefaultColor ;

   Toggle toggle ;

   Vector2 handle1Position ;

   void Awake ( ) {
      toggle = GetComponent <Toggle> ( ) ;

      handle1Position = uiHandleRectTransform.anchoredPosition ;

      background1Image = uiHandleRectTransform.parent.GetComponent <Image> ( ) ;
      handle1Image = uiHandleRectTransform.GetComponent <Image> ( ) ;

      background1DefaultColor = background1Image.color ;
      handle1DefaultColor = handle1Image.color ;

      toggle.onValueChanged.AddListener (OnSwitch) ;

      if (toggle.isOn)
	  {
         OnSwitch (true) ;
      }
      Yo = GameObject.Find("M2MQTT");
      //button2_next = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>().Btn2;
      //button2_current = button2_next;
   }

   void OnSwitch (bool on) {
      uiHandleRectTransform.DOAnchorPos (on ? handle1Position * -1 : handle1Position, .4f).SetEase (Ease.InOutBack) ;
      background1Image.DOColor (on ? background1ActiveColor : background1DefaultColor, .6f) ;
      handle1Image.DOColor (on ? handle1ActiveColor : handle1DefaultColor, .4f) ;
   }

   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }
   void Update(){
      
      button2_next = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>().Btn2;
     
      
   	  if(button2_next!= button2_current){
   	  button2_current = button2_next;
   	  	if(button2_current == "False"){
      	toggle.isOn = false ;}
      	else if(button2_current == "True"){
      	toggle.isOn = true ;}
      }
   	}
}
