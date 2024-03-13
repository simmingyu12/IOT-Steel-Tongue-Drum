using System.Xml;
using UnityEngine;

public class XMLParser : MonoBehaviour
{
    void Start()
    {
        // XML 파일 경로 지정 (Assets 폴더 내에 있다고 가정)
        string filePath = Application.dataPath + "/고향의 봄.musicxml";

        // XML 파일 로드
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        // "note" 요소를 모두 추출
        XmlNodeList notes = xmlDoc.GetElementsByTagName("note");

        foreach (XmlNode note in notes)
        {
            string step = note.SelectSingleNode("step").InnerText;
            string octave = note.SelectSingleNode("octave").InnerText;
            string duration = note.SelectSingleNode("duration").InnerText;

            // 추출한 데이터를 출력하거나 다른 처리를 수행할 수 있습니다.
            Debug.Log("Step: " + step + ", Octave: " + octave + ", Duration: " + duration);
        }
    }
}