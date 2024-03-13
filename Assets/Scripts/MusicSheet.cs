using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class MusicSheet : MonoBehaviour
{
    private int measure = 0;
    public float measureTime = 0f;
    private int maxMeasureNumber = 0;
    private int bpm = 0;
    private bool isPaused = false;
    public bool isNoteMoving = true;
    private Vector3 initialMusicSheetPosition; //악보 초기 위치를 저장하는 변수
    private Vector3 initialMusicSheetPosition2; //악보 초기 위치를 저장하는 변수
    private Vector3 initialNotePosition = new Vector3(-800f, 0f, 0f); //노트 초기 위치를 저장하는 변수

    private int maxAttributeNumber = 0;
    private float noteSpeed;
    private Song song;
    private int currentSheetIndex = 0;

    [SerializeField] private SelectMenu selectMenu;

    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtComposerName = null;

    [SerializeField] Image backgroundSongImage = null;
    [SerializeField] Image musicSheetImage = null;
    [SerializeField] Image musicSheetImage2 = null;
    [SerializeField] GameObject goPauseUi = null;
    [SerializeField] Image note = null;

    public void ShowMusicSheetSongInfo(Song song)
    {
        this.song = song;
        txtSongName.text = song.name;
        txtComposerName.text = song.composer;
        backgroundSongImage.sprite = song.sprite;
        musicSheetImage.sprite = song.musicSheetSprite;
        musicSheetImage2.sprite = song.musicSheetSprite2;
        musicSheetImage2.gameObject.SetActive(false);
        if (song.musicXMLFile != null)
        {
            ParseMusicXML(song.musicXMLFile.text);
        }
        else
        {
            Debug.LogError("MusicXML file is not assigned.");
        }
    }

    // xml파일에서 bpm 및 노트 정보를 가져옴
    void ParseMusicXML(string xmlContent)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        XmlNode readBpm = xmlDoc.SelectSingleNode("//direction-type/metronome");
        if (readBpm != null)
        {
            string beatUnit = readBpm.SelectSingleNode("beat-unit")?.InnerText;
            this.bpm = (int)float.Parse(readBpm.SelectSingleNode("per-minute")?.InnerText ?? "0");

            Debug.Log("BPM: " + this.bpm + " (beat unit: " + beatUnit + ")");

        if (bpm > 0)
        {
            float beatsPerSecond = bpm / 60f; // 분당 박자 수를 초당 박자 수로 변환
            noteSpeed = beatsPerSecond * 10f; // 상수를 조절하여 노트 이동 속도를 조절
        }
        else
        {
            Debug.LogError("BPM 정보가 유효하지 않습니다.");
        }
    }
    else
    {
        Debug.LogError("bpm정보가 없습니다.");
    }

    XmlNodeList readMeasureNumber = xmlDoc.SelectNodes("//measure");
    foreach (XmlNode measureNode in readMeasureNumber)
    {
        XmlAttribute attribute = measureNode.Attributes["number"];
        if (attribute != null)
        {
            int measureNumber;
            if (int.TryParse(attribute.Value, out measureNumber))
            {
                maxMeasureNumber = Mathf.Max(maxMeasureNumber, measureNumber);
            }
        }
    XmlNode timeNode = measureNode.SelectSingleNode(".//time");
        if (timeNode != null)
        {
            string beats = timeNode.SelectSingleNode("beats")?.InnerText;
            string beatType = timeNode.SelectSingleNode("beat-type")?.InnerText;
            Debug.Log("Measure " + attribute.Value + ": Beats - " + beats + ", Beat Type - " + beatType);
        }
    }
    Debug.Log("가장 큰 measure 번호: " + maxMeasureNumber);
    }

    void Update()
    {
        measureTime += Time.deltaTime;
        if (bpm == 60)
        {
            Invoke("DelayedBeat4Bpm60", 3f);
        }
        else if (bpm == 100)
        {
            Invoke("DelayedBeat4Bpm100", 3f);
        }
        else if(bpm == 92)
        {
            Invoke("DelayedBeat4Bpm92", 3f);
        }
    }

    void OnEnable()
    {
        // 초기 위치를 저장
        initialMusicSheetPosition = musicSheetImage.transform.position;
        initialMusicSheetPosition2 = musicSheetImage2.transform.position;

        initialNotePosition = new Vector3(-800f, 199f, 0f);
    }

    void DelayedBeat4Bpm60()
    {
        MoveNote();
        beat4bpm60();
    }

    void DelayedBeat4Bpm100()
    {
        MoveNote();
        beat4bpm100();
    }

    void DelayedBeat4Bpm92()
    {
        MoveNote();
        beat4bpm92();
    }

    public void beat4bpm60()
    {
        {
            measureTime = 0f;
            measure++;
            Debug.Log("Measure :" + measure);
            if (measure == 32 && musicSheetImage2.sprite != null)
            {
                musicSheetImage.gameObject.SetActive(false);
                musicSheetImage2.gameObject.SetActive(true);
                ResetNotePosition();
                musicSheetImage2.transform.position = initialMusicSheetPosition2;
            }
            if (measure % 4 == 0 && measure != 8) // Check if measure is a multiple of 4
            {
                musicSheetImage.transform.position += new Vector3(0f, 390f, 0f);
                musicSheetImage2.transform.position += new Vector3(0f, 390f, 0f);
                ResetNotePosition();
            }
        }
    }

    public void beat4bpm100()
    {
        if (measureTime >= 2.4f)
        {
            measureTime = 0f;
            measure++;
            Debug.Log("Measure :" + measure);
            if (measure == 32 && musicSheetImage2.sprite != null)
            {
                musicSheetImage.gameObject.SetActive(false);
                musicSheetImage2.gameObject.SetActive(true);
                ResetNotePosition();
                musicSheetImage2.transform.position = initialMusicSheetPosition2;
            }
            if (measure % 4 == 0 && measure != 32) // Check if measure is a multiple of 4
            {
                musicSheetImage.transform.position += new Vector3(0f, 390f, 0f);
                ResetNotePosition();
            }
        }
    }

    public void beat4bpm92()
    {
        if (measureTime >= 2.61f)
        {
            measureTime = 0f;
            measure++;
            Debug.Log("Measure :" + measure);
            if (measure == 32 && musicSheetImage2.sprite != null)
            {
                musicSheetImage.gameObject.SetActive(false);
                musicSheetImage2.gameObject.SetActive(true);
                ResetNotePosition();
                musicSheetImage2.transform.position = initialMusicSheetPosition2;
            }
            if (measure % 4 == 0 && measure != 32) // Check if measure is a multiple of 4
            {
                musicSheetImage.transform.position += new Vector3(0f, 390f, 0f);
                musicSheetImage2.transform.position += new Vector3(0f, 390f, 0f);
                ResetNotePosition();
            }
        }
    }


    public void MoveNote()
    {
        if (isNoteMoving)
        {
            RectTransform noteRectTransform = note.rectTransform;

            // 노트를 이동합니다.
            noteRectTransform.localPosition += Vector3.right * noteSpeed * 10f * Time.deltaTime;
        }
    }

    public void PauseButton()
    {
        isPaused = true; // 게임 일시정지 상태로 변경
        goPauseUi.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지
        AudioManager.instance.PauseBGM();
        isNoteMoving = false;
    }

    public void Restart()
    {
        measure = 0;
        measureTime = 0f;
        isPaused = false;
        Time.timeScale = 1f;
        musicSheetImage.transform.position = initialMusicSheetPosition;
        isNoteMoving = true;
        RectTransform noteRectTransform = note.rectTransform;
        noteRectTransform.localPosition = initialNotePosition;
        AudioManager.instance.RestartBGM();
    }

    public void ResumePlaying()
    {
        isPaused = false; // 게임 일시정지 상태 종료
        goPauseUi.SetActive(false); // 일시정지 UI 비활성화
        Time.timeScale = 1f; // 게임 재개
        AudioManager.instance.ResumeBGM(); // BGM 다시 재생
        isNoteMoving = true;
    }

public void GoSelect()
{
    // 게임 값을 초기화하고 기본 상태로 돌아가기
    measure = 0;
    measureTime = 0f;
    isPaused = false;
    Time.timeScale = 1f;
    musicSheetImage.transform.position = initialMusicSheetPosition;
    isNoteMoving = false;

    // 초기 노트 위치로 설정
    RectTransform noteRectTransform = note.rectTransform;
    noteRectTransform.localPosition = initialNotePosition;

    // 노래 정지
    AudioManager.instance.StopBGM();
}

    // 노트를 저장된 초기위치로 되돌림
    public void ResetNotePosition()
    {
        RectTransform noteRectTransform = note.rectTransform;
        noteRectTransform.localPosition = initialNotePosition;
    }

    // 음악이 종료되면 악보이미지를 초기위치로 되돌림
    public void ResetMusicSheetImagePosition()
    {
        musicSheetImage.transform.position = initialMusicSheetPosition; 
        if (!gameObject.activeSelf)
        {
            musicSheetImage.transform.position = initialMusicSheetPosition;
        }
    }
    public void ResetMeasure()
    {
        measure = 0;
    measureTime = 0f;
    isPaused = false;
    }
}
