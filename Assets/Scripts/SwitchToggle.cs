using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;
using System;
using System.Collections;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;



public class SwitchToggle : MonoBehaviour {
   [SerializeField] RectTransform uiHandleRectTransform ;
   [SerializeField] Color backgroundActiveColor ;
   [SerializeField] Color handleActiveColor ;
   GameObject Yo;
  	//M2MqttUnity.Examples.M2MqttUnityTest YoBro;
	string button1_next;
	string button1_current;
	//bool init = false;
	

   Image backgroundImage, handleImage ;

   Color backgroundDefaultColor, handleDefaultColor ;

   Toggle toggle ;

   Vector2 handlePosition ;

   void Awake ( ) {
   	  
      toggle = GetComponent <Toggle> ( ) ;

      handlePosition = uiHandleRectTransform.anchoredPosition ;

      backgroundImage = uiHandleRectTransform.parent.GetComponent <Image> ( ) ;
      handleImage = uiHandleRectTransform.GetComponent <Image> ( ) ;

      backgroundDefaultColor = backgroundImage.color ;
      handleDefaultColor = handleImage.color ;

      toggle.onValueChanged.AddListener (OnSwitch) ;
      
      //Update();

      if (toggle.isOn)
	  {
         OnSwitch (true) ;
      }
      Yo = GameObject.Find("M2MQTT");
      //button1_next = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>().Btn1;
      //button1_current = button1_next;
      
      //M2MqttUnity.Examples.M2MqttUnityTest YoBro = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>();
   }

   void OnSwitch (bool on) {
      uiHandleRectTransform.DOAnchorPos (on ? handlePosition * -1 : handlePosition, .4f).SetEase (Ease.InOutBack) ;
      backgroundImage.DOColor (on ? backgroundActiveColor : backgroundDefaultColor, .6f) ;
      handleImage.DOColor (on ? handleActiveColor : handleDefaultColor, .4f) ;
   }

   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }
   void Update(){
      
      button1_next = Yo.GetComponent< M2MqttUnity.Examples.M2MqttUnityTest>().Btn1;
     
      
   	  if(button1_next!= button1_current){
   	  button1_current = button1_next;
   	  	if(button1_current == "False"){
      	toggle.isOn = false ;}
      	else if(button1_current == "True"){
      	toggle.isOn = true ;}
      }
   	}
}
