// SongSelectButton.cs
using UnityEngine;
using UnityEngine.UI;

public class SongSelectButton : MonoBehaviour
{
    public void Initialize(string buttonText, int index, System.Action<int> onClickCallback)
    {
        // 초기화 코드
        Text buttonTextComponent = GetComponentInChildren<Text>();
        if (buttonTextComponent != null)
        {
            buttonTextComponent.text = buttonText;
        }

        // 클릭 시 콜백 함수 등록
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => onClickCallback?.Invoke(index));
        }
    }
}
