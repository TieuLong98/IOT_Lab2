																																					﻿/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

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

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class M2MqttUnityTest : M2MqttUnityClient
    {
        //[Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        //public bool autoTest = true; 
        //[Header("User Interface")]
        //public InputField consoleInputField;
        //public Toggle encryptedToggle;
        //public InputField addressInputField;
        //public InputField portInputField;
        //public Button connectButton;
        //public Button disconnectButton;
        //public Button testPublishButton;
        //public Button clearButton;
        //public string Topic;
        public string Btn1;
        public string Btn2;
        public Text temperature;
        public Text humidity;
        public string temp;
        public string humid;
        //public string Machine_Id;
        public string Topic_to_Subcribe="";
        //public string Topic_to_Publish="";
        public string msg_received_from_topic="";
        public Text text_display;
        public CanvasGroup _canvasLayer1;
        public CanvasGroup _canvasLayer2;
        public CanvasGroup _canvasLayer3;
        bool check = false;
        private List<string> eventMessages = new List<string>();
        private bool updateUI = false;
        GameObject toggle1;
        GameObject toggle2;
        
        public Tween twenFade;
        
        public void Toggle1(){
        toggle1 = GameObject.Find("SwitchToggle");
        if(toggle1.GetComponent<Toggle>().isOn){
        client.Publish("/bkiot/1652350/led", System.Text.Encoding.UTF8.GetBytes("{\"device\":\"LED\",\"status\":\"ON\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    	}
    	else{
    	client.Publish("/bkiot/1652350/led", System.Text.Encoding.UTF8.GetBytes("{\"device\":\"LED\",\"status\":\"OFF\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    	}
    	}
    	
    	 public void Toggle2(){
        toggle2 = GameObject.Find("SwitchToggle1");
        if(toggle2.GetComponent<Toggle>().isOn){
        client.Publish("/bkiot/1652350/pump", System.Text.Encoding.UTF8.GetBytes("{\"device\":\"PUMP\",\"status\":\"ON\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    	}
    	else{
    	client.Publish("/bkiot/1652350/pump", System.Text.Encoding.UTF8.GetBytes("{\"device\":\"PUMP\",\"status\":\"OFF\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    	}
    	}

        //private void Awake()
        //{  
		//Topic_to_Subcribe = "v1/devices/me/rpc/request/+" ;
        //Topic_to_Publish = "v1/devices/me/attributes" ;
        //}
//        public void TestPublish()
//        {
//            client.Publish(Topic_to_Publish, System.Text.Encoding.UTF8.GetBytes("{\"R1\": \"false\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
//            client.Publish(Topic_to_Publish, System.Text.Encoding.UTF8.GetBytes("{\"R2\": \"false\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
//            Debug.Log("Test message published");
//            AddUiMessage("Test message published.");
//        }

        //public void SetBrokerAddress(string brokerAddress)
        //{
        //    if (addressInputField && !updateUI)
        //    {
        //        this.brokerAddress = brokerAddress;
        //    }
        //}

        //public void SetBrokerPort(string brokerPort)
        //{
        //    if (portInputField && !updateUI)
        //    {
        //        int.TryParse(brokerPort, out this.brokerPort);
        //    }
        //}

        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }


        //public void SetUiMessage(string msg)
        //{
        //    if (consoleInputField != null)
        //    {
        //        consoleInputField.text = msg;
        //        updateUI = true;
        //    }
        //}

        public void AddUiMessage(string msg)
        {
            //if (consoleInputField != null)
            //{
            //    consoleInputField.text += msg + "\n";
            //    updateUI = true;
            //}
        }

        protected override void OnConnecting()
        {
            base.OnConnecting();
            //SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            //SetUiMessage("Connected to broker on " + brokerAddress + "\n");

            //if (autoTest)
            //{
            //    TestPublish();
            //}
            SubscribeTopics();
            //TestPublish();
        }

        protected override void SubscribeTopics()
        {
            if (Topic_to_Subcribe!="")
            {
            	if(check == false){
            	client.Publish("/bkiot/1652350/led", System.Text.Encoding.UTF8.GetBytes("{\"device\":\"LED\",\"status\":\"OFF\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            	client.Publish("/bkiot/1652350/pump", System.Text.Encoding.UTF8.GetBytes("{\"device\":\"PUMP\",\"status\":\"OFF\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            	}
				check = true;	
                client.Subscribe(new string[] { Topic_to_Subcribe }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                //client.Subscribe(new string[] { "v1/devices/me/attributes/humidity" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                //client.Subscribe(new string[] {"v1/devices/me/attributes"}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
   				//client.Subscribe(new string[] {"v1/devices/me/attributes/response/+"}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { Topic_to_Subcribe });
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            AddUiMessage("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            AddUiMessage("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            AddUiMessage("CONNECTION LOST!");
        }

        private void UpdateUI()
        {
            //if (client == null)
            //{
            //    if (connectButton != null)
            //    {
            //        connectButton.interactable = true;
            //        disconnectButton.interactable = false;
            //        testPublishButton.interactable = false;
            //    }
            //}
            //else
            //{
            //    if (testPublishButton != null)
            //    {
            //        testPublishButton.interactable = client.IsConnected;
            //    }
            //    if (disconnectButton != null)
            //    {
            //        disconnectButton.interactable = client.IsConnected;
            //    }
            //    if (connectButton != null)
            //    {
            //        connectButton.interactable = !client.IsConnected;
            //    }
            //}
            //if (addressInputField != null && connectButton != null)
            //{
            //    addressInputField.interactable = connectButton.interactable;
            //    addressInputField.text = brokerAddress;
            //}
            //if (portInputField != null && connectButton != null)
            //{
            //    portInputField.interactable = connectButton.interactable;
            //    portInputField.text = brokerPort.ToString();
            //}
            //if (encryptedToggle != null && connectButton != null)
            //{
            //    encryptedToggle.interactable = connectButton.interactable;
            //    encryptedToggle.isOn = isEncrypted;
            //}
            //if (clearButton != null && connectButton != null)
            //{
            //    clearButton.interactable = connectButton.interactable;`
            //}
            //updateUI = false;
        }
        

        protected override void Start()
        {
            //SetUiMessage("Ready.");
            //Topic_to_Publish = "" ;
            Topic_to_Subcribe = "/bkiot/1652350/status";
            //int requestI
            updateUI = true;
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            msg_received_from_topic = msg;
            Debug.Log("Received: " + msg);
            StoreMessage(msg);
            text_display.text = msg;
            
          	var layjsonfile = JObject.Parse(msg_received_from_topic);
          	temperature.text = layjsonfile["temperature"].ToString();
        	humidity.text = layjsonfile["humidity"].ToString();
        	temp = layjsonfile["temperature"].ToString();
        	humid = layjsonfile["humidity"].ToString();
        }

        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(string msg)
        {
            AddUiMessage("Received: " + msg);
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()

            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
            if (updateUI)
            {
                UpdateUI();
            }
//            toggle1 = GameObject.Find("SwitchToggle");
//            //toggle2 = GameObject.Find("SwitchToggle1");
//            if(Btn1 == "true"){
//            	toggle1.GetComponent<Toggle>().isOn = true ;
//       		}
//       		else if (Btn1 == "false"){
//       			toggle1.GetComponent<Toggle>().isOn = !toggle1.GetComponent<Toggle>().isOn;
//       		}
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            //if (autoTest)
            //{
            //    autoConnect = true;
            //}
        }
        public void Fade(CanvasGroup _canvas, float endValue, float duration, TweenCallback onFinish)
        {
            if (twenFade != null)
            {
                twenFade.Kill(false);
            }

            twenFade = _canvas.DOFade(endValue, duration);
            twenFade.onComplete += onFinish;
        }

        public void FadeIn(CanvasGroup _canvas, float duration)
        {
            Fade(_canvas, 1f, duration, () =>
            {
                _canvas.interactable = true;
                _canvas.blocksRaycasts = true;
            });
        }

        public void FadeOut(CanvasGroup _canvas, float duration)
        {
            Fade(_canvas, 0f, duration, () =>
            {
                _canvas.interactable = false;
                _canvas.blocksRaycasts = false;
            });
        }
         IEnumerator _IESwitchLayer()
        {
            if (_canvasLayer1.interactable == true)
            {
                FadeOut(_canvasLayer1, 0.25f);
                yield return new WaitForSeconds(0.5f);
                FadeIn(_canvasLayer2, 0.25f);
            }
            else if(_canvasLayer2.interactable == true)
            {
                FadeOut(_canvasLayer2, 0.25f);
                yield return new WaitForSeconds(0.5f);
                FadeIn(_canvasLayer3, 0.25f);
            }
            else{
            	FadeOut(_canvasLayer3, 0.25f);
                yield return new WaitForSeconds(0.5f);
                FadeIn(_canvasLayer2, 0.25f);
        	}
        }
        public void SwitchLayer()
        {
            StartCoroutine(_IESwitchLayer());
        }
    }
}
