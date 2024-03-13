using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject stopMusicSheet = null;
    private bool isPaused = false;
    public MusicSheet musicSheet; // Inspector에서 MusicSheet를 연결해줘야 함

    public void RestartButton()
    {
        if (musicSheet != null)
        {
            AudioManager.instance.StopDisableCoroutine(); // 중지
            musicSheet.Restart();
        }
        else
        {
            Debug.LogError("MusicSheet가 연결되지 않았습니다.");
        }
        this.gameObject.SetActive(false);
    }

    public void StartButton()
    {
        this.gameObject.SetActive(false); 

        if (musicSheet != null)
        {
            AudioManager.instance.StopDisableCoroutine(); // 중지
            musicSheet.ResumePlaying(); 
        }
        else
        {
            Debug.LogError("MusicSheet가 연결되지 않았습니다.");
        }
    }

    public void GoSelectButton()
    {
        this.gameObject.SetActive(false);
        stopMusicSheet.SetActive(false);        
       if (musicSheet != null)
        {
            AudioManager.instance.StopDisableCoroutine(); // 중지
            musicSheet.GoSelect(); 
        }
    }
}
