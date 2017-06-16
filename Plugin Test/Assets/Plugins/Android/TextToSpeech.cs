using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextToSpeech :MonoBehaviour
{
    void Start()
    {
       

    }
    public enum Locale
    {
        UK = 0,
        US = 1

    }
    private AndroidJavaObject TTSExample = null;
    private AndroidJavaObject activityContext = null;
    private Locale _lang;
    public Locale Language { get { return _lang; } set { SetLanguage(value); } }
    private float _pitch, _speed;
    public float Pitch { get{return _pitch;} set { SetPitch(value); } }
    public float Speed { get{return _speed;} set { SetSpeed(value); } }

    public delegate void OnErrorCallbackHandler(string error);
    private OnErrorCallbackHandler _callback;
    public TextToSpeech()
    {
        //Initialize();
    }
    public TextToSpeech(Locale language)
    {
        Initialize();
        this.Language = language;
        SetLanguage(this.Language);
    }
    public TextToSpeech(Locale language,float speed,float pitch)
    {
        Initialize();
        this.Language = language;
        this.Pitch = pitch;
        this.Speed = speed;
        SetLanguage(this.Language);
        SetSpeed(this.Speed);
        SetPitch(this.Pitch);
    }
    public void Speak(string toSay,OnErrorCallbackHandler callback)
    {
        if (TTSExample == null)
        {
            Initialize();
        }
        this._callback = callback;


        TTSExample.Call("TTSMEWithCallBack", toSay, gameObject.name, "OnError");
       
    }
    public void OnError(string error)
    {
        if (_callback != null)
        {
            if (error.Length > 0)
            {
                _callback.Invoke(error);
            }
        }
        ShowToast(error);
    }
    public void Speak(string toSay)
    {
        if (TTSExample == null)
        {
            Initialize();
        }

        TTSExample.Call("TTSME", toSay);

    }
    public void SetLanguage(Locale lan)
    {
        this._lang = lan;
        string[] Language = new string[] {"UK","US" };
        if (TTSExample == null)
        {
            Initialize();
        }
        TTSExample.Call("SetLang", Language[(int)lan]);
    }
    public void SetSpeed(float speed)
    {
        this._speed = speed;
        if (TTSExample == null)
        {
            Initialize();
        }
        TTSExample.Set<float>("Speed", speed);
    }
    public void SetPitch(float pitch)
    {
        this._pitch = pitch;
        if (TTSExample == null)
        {
            Initialize();
        }
        TTSExample.Set<float>("Pitch", pitch);
    }
    private void Initialize()
    {
        if (TTSExample == null)
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            using (AndroidJavaClass pluginClass = new AndroidJavaClass("ir.hoseinporazar.androidtts.TTS"))
            {
                if (pluginClass != null)
                {
                    TTSExample = pluginClass.CallStatic<AndroidJavaObject>("instance");
                    TTSExample.Call("setContext", activityContext);

                  

                }
            }
        }


    }
    public void ShowToast(string msg)
    {

        if (TTSExample == null)
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            using (AndroidJavaClass pluginClass = new AndroidJavaClass("ir.hoseinporazar.androidtts.TTS"))
            {
                if (pluginClass != null)
                {
                    TTSExample = pluginClass.CallStatic<AndroidJavaObject>("instance");
                    TTSExample.Call("setContext", activityContext);
                    activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        TTSExample.Call("showMessage", msg);
                    }));
                }
            }
        }
        else
        {
            activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                TTSExample.Call("showMessage", msg);
            }));
        }
    }


}
