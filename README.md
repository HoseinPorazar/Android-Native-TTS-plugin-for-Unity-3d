Android native Text To Speech plugin for Unity 3d

Simple Implementation of android Text To Speech to use in Unity 3d.
with abilty to change TTS speed and pitch and Language and callback for errors.


if you are creating a Unity 3d project for Android devices , and you want to implement Text To Speech in your project , you can use this plugin.
This plugin is written in Android Studio V2.3 , plugin source code in Android Studio is Available to download, you can download and modify based on your needs.

How this works :
we use plugin to implement Text To Speech of Android device, we create a script in Unity 3d to call methods of plugin and plugin returns error ( if error happens ).  
How to use this plugin in Unity 3d :
1-import AndroidNativeTTS.unitypackage into your project
2-create an empty game object and rename it to tts.
3-attach test script and TextToSpeech script to tts game object.
4-add a button and set the on click event to test.Speak().
5-build project for Android platform.

Methods :
```cs
1-Convert Text To Speech:

 public void Speak(string toSay) 

 //for example Speak("hello world")

 public void Speak(string toSay,OnErrorCallbackHandler callback) 

 // this returns errors of Text To Speech result. use this to detect    errors and fix them .
 //for example error happens when device haven't installed Text to speech engine.
 //or error happens when specified language is not supported or not installed.
 
 2-Set text to speech Language:

  public void SetLanguage(Locale lan)

  //setting language of text to speech for example : SetLanguage(Locale.UK) 
  // I have included 2 languages (UK,and US) if you want to use other languages you will have to modify plugin ( with Android Studio).
 
 3-Setting Text To Speech Speed :

public void SetSpeed(float speed)  

//calling this method you can set speed of text to speech float between(0-1)

 3-Setting Text To Speech Pitch :
 
 public void SetPitch(float pitch)
   
calling this method you can set Pitch of text to speech float between(0-1)

4-Show Toast message

 public void ShowToast(string msg)

 //simple implementation of showing message using Toast.
      ```
