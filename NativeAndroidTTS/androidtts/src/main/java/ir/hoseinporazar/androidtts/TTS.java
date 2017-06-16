package ir.hoseinporazar.androidtts;

import android.content.Context;
import android.speech.tts.TextToSpeech;
import android.widget.Toast;

import java.util.Locale;
import com.unity3d.player.UnityPlayer;


public class TTS {

    private Context context;
    private TextToSpeech t1;
    private  String textToSpeak="hello";
    private static TTS instance;
    public float Speed=1f;
    public float Pitch=1f;

    private String _gameObject;
    private String _callback;
    public TTS() {
        this.instance = this;
    }

    public static TTS instance() {
        if(instance == null) {
            instance = new TTS();
        }
        return instance;
    }

    public void setContext(Context context) {
        this.context = context;
    }

    public void showMessage(String message) {
        Toast.makeText(this.context, message, Toast.LENGTH_SHORT).show();
    }
     String Error="";
    public  void TTSME(String text) {
        textToSpeak=text;

        t1=new TextToSpeech(context, new TextToSpeech.OnInitListener() {
            @Override
            public void onInit(int status) {
             if(status==TextToSpeech.SUCCESS){
                int result=t1.setLanguage(Locale.US);
                 if(result==TextToSpeech.LANG_MISSING_DATA||result==TextToSpeech.LANG_NOT_SUPPORTED){
                     Error="This language is not supported!";
                 }
                 t1.setSpeechRate(Speed);
                 t1.setPitch(Pitch);
                 t1.speak(textToSpeak,TextToSpeech.QUEUE_FLUSH,null);
             }else{
                 Error="TTS Initialization failed!";

             }

            }
        });

    }
    public  void TTSMEWithCallBack(String text,String gameobject,String callback) {
        textToSpeak=text;
         this._gameObject=gameobject;
        this._callback=callback;
        t1=new TextToSpeech(context, new TextToSpeech.OnInitListener() {
            @Override
            public void onInit(int status) {
                if(status==TextToSpeech.SUCCESS){
                    int result=t1.setLanguage(Locale.US);
                    if(result==TextToSpeech.LANG_MISSING_DATA||result==TextToSpeech.LANG_NOT_SUPPORTED){
                        Error="This language is not supported!";
                    }
                    t1.setSpeechRate(Speed);
                    t1.setPitch(Pitch);
                    t1.speak(textToSpeak,TextToSpeech.QUEUE_FLUSH,null);
                }else{
                    Error="TTS Initialization failed!";

                }

            }
        });

         UnityPlayer.UnitySendMessage(_gameObject, _callback,Error);
    }
    public void SetLang(String loc){
        switch (loc){
            case "UK":
                if(t1!=null)
                t1.setLanguage(Locale.UK);
                break;
            case "US":
                if(t1!=null)
                t1.setLanguage(Locale.US);
                break;
        }
    }

}
