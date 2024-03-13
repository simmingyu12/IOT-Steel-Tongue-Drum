using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSample : MonoBehaviour
{
    
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header("Scroll View Events")]
    [SerializeField] private ItemButtonEvent _eventItemClicked;

    [SerializeField] private SelectMenu _selectMenu;

    [SerializeField] private MusicSheet _musicSheetMenu;

    void Start()
    {
        if (Application.isPlaying)
        {
            _selectMenu = FindObjectOfType<SelectMenu>();

            if (_selectMenu != null)
            {
                TestCreateItems(_selectMenu.songList.Length);
            }
        }
    }

    private void TestCreateItems(int count)
    {
        if (_selectMenu != null)
        {
            for (int i = 0; i < _selectMenu.songList.Length; i++)
            {
                CreateItem(_selectMenu.songList[i].name);
            }
        }
        else
        {
            Debug.LogError("SelectMenu가 찾아지지 않았거나 songList가 null입니다.");
        }
    }

    private ItemButton CreateItem(string strName)
    {
        GameObject gObj = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);

        if (gObj != null)
        {
            gObj.transform.SetParent(_content.transform);
            gObj.transform.localScale = Vector3.one;
            gObj.transform.localPosition = Vector3.zero;
            gObj.transform.localRotation = Quaternion.identity;
            gObj.name = strName;

            ItemButton item = gObj.GetComponent<ItemButton>();

            if (item != null)
            {
                Song song = Array.Find(_selectMenu.songList, s => s.name == strName);

                if (song != null)
                {
                    item.songNameValue = song.name;
                    item.ComposerNameValue = song.composer;
                    item.AudioClip = song.audioClip;

                    if (item.SongImage != null)
                        item.SongImage.sprite = song.sprite;

                    if (item.OnClickEvent != null)
                        item.OnClickEvent.AddListener((ItemButton) => { HandleEventItemOnClick(item, song); });

                    return item;
                }
                else
                {
                    Debug.LogError("Song 정보를 찾을 수 없습니다.");
                }
            }
            else
            {
                Debug.LogError("Prefab에서 ItemButton 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Prefab이 인스턴스화되지 않았습니다.");
        }

        return null;
    }

    private void HandleEventItemOnClick(ItemButton item, Song song)
    {
        _eventItemClicked?.Invoke(item);
        _selectMenu.ShowSelectedSongInfo(song);
        if (_musicSheetMenu != null)
        {
            _musicSheetMenu.ShowMusicSheetSongInfo(song);
        }
    }

}