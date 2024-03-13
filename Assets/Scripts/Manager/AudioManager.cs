using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private Sound[] sfx = null;
    [SerializeField] private Sound[] bgm = null;

    [SerializeField] private AudioSource bgmPlayer = null;
    [SerializeField] private AudioSource[] sfxPlayer = null;

    private AudioClip currentBGM;

    private Coroutine disableCoroutine;

    void Start()
    {
        instance = this;
    }

    public void PlaySelectBGM(AudioClip clip)
    {
        StopBGM(); // 현재 재생 중인 BGM 중지
        bgmPlayer.clip = clip;
        currentBGM = clip; // 현재 재생 중인 BGM 갱신
        bgmPlayer.Play();
        StartCoroutine(DisableMusicSheetAfterBGMEnds(clip.length));
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void ReplayCurrentBGM()
{
    if (currentBGM != null)
    {
        PlaySelectBGM(currentBGM);
        StartDisableCoroutine(currentBGM.length);
    }
}

public void RestartBGM()
{
    if (currentBGM != null)
    {
        // 이전에 시작한 코루틴을 중지
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
        }

        // 현재 BGM을 멈추고 재생
        PlaySelectBGM(currentBGM);

        // DisableMusicSheetAfterBGMEnds 코루틴 시작
        StartDisableCoroutine(currentBGM.length);
    }
}
    public void PauseBGM()
    {
        if (bgmPlayer.isPlaying)
        {
            bgmPlayer.Pause();
        }
    }

    public void ResumeBGM()
    {
        bgmPlayer.UnPause(); // BGM 재개
    }
    
    private IEnumerator DisableMusicSheetAfterBGMEnds(float duration)
    {
        yield return new WaitForSeconds(duration + 3f); // BGM 재생 시간 + 3초 대기

        MusicSheet musicSheet = FindObjectOfType<MusicSheet>();
        if (musicSheet != null)
        {
            musicSheet.ResetNotePosition();
            musicSheet.ResetMusicSheetImagePosition();
            musicSheet.ResetMeasure(); 
            musicSheet.gameObject.SetActive(false);
        }
    }

    

    // 코루틴을 시작하는 함수
    private void StartDisableCoroutine(float duration)
    {
        // 기존의 코루틴이 실행 중이라면 중지
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
        }

        // 새로운 코루틴 시작
        disableCoroutine = StartCoroutine(DisableMusicSheetAfterBGMEnds(duration));
    }

    public void StopDisableCoroutine()
    {
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
        }
    }
}
