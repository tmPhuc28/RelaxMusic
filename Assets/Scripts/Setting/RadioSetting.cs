using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public partial class RadioSetting : MonoBehaviour
{
    public PlayListData listData;

    [Header("Album")]
    public AlbumSetting albumSetting;
    public ToggleGroup albumGroup;
    public Toggle albumPrefab;
    public List<Toggle> albumToggleList = new List<Toggle>();

    [Header("Songs")]
    public SongSetting songSetting;
    public ScrollRect songScrollRect;
    public ToggleGroup playListView;
    public GameObject songContentPrefab;
    public Toggle songPrefab;
    public List<RectTransform> songContentList = new List<RectTransform>();

    public List<Toggle> songToggleList = new List<Toggle>();

    [Header("Choose Album")]
    public List<Playlist> playlistQueues = new List<Playlist>();

    [Header("Playing Album")]
    public List<Playlist> playlistPlaying = new List<Playlist>();

    void Start()
    {
        Init();
        EventSetting();
        volumeSlider.value = valueVolume;
    }
    private void EventSetting()
    {
        AlbumToggleSetting();
        EventUISetting();
        GUISetting();
    }

    private void Init()
    {
        if (listData.albumList.Count > 0)
        {
            for (int i = 0; i < listData.albumList.Count; i++)
            {
                if (listData.albumList[i].songList.Count > 0)
                {
                    albumSetting.GetNameAlbum(listData.albumList[i]);
                    Toggle albumToggle = Instantiate(albumPrefab, albumGroup.transform);
                    albumToggle.group = albumGroup;
                    albumToggleList.Add(albumToggle);

                    GameObject songContent = Instantiate(songContentPrefab, playListView.transform);
                    songContentList.Add(songContent.GetComponent<RectTransform>());

                    for (int j = 0; j < listData.albumList[i].songList.Count; j++)
                    {
                        if (listData.albumList[i].songList[j].songClip != null)
                        {
                            songSetting.GetData(listData.albumList[i].songList[j]);
                            Toggle songToggle = Instantiate(songPrefab, songContent.transform);
                            songToggle.group = playListView;
                            songToggleList.Add(songToggle);
                        }
                    }
                }
            }
        }
    }

    public void AlbumToggleSetting()
    {
        foreach (Toggle toggle in albumToggleList)
        {
            toggle.onValueChanged.AddListener((isOn) => OnAlbumToggleValueChanged(toggle, isOn));
        }
    }
    public void SongToggleSetting()
    {
        if (playlistPlaying.Count > 0)
        {
            foreach (Playlist playlist in playlistPlaying)
            {
                playlist.songToggle.onValueChanged.AddListener((isOn) => OnSongToggleValueChanged(playlist, isOn));
            }
        }
    }
    private void OnSongToggleValueChanged(Playlist playlist, bool isOn)
    {
        int index = playlistPlaying.IndexOf(playlist);

        if (isOn)
        {
            OnOffPlayPauseButton(isOn);
            PlayClipInToggle(index);

            TurnOffAllSongTogglesExcept(playlist.songToggle);
        }
        else
            OnOffPlayPauseButton(isOn);
        GetCurrentPlayList();
    }
    private void OnAlbumToggleValueChanged(Toggle toggle, bool isOn)
    {
        if (isOn)
        {
            int index = albumToggleList.IndexOf(toggle);

            if (index >= 0 && index < listData.albumList.Count)
            {
                GUIContentPlayList(index);
                GetSongInAlbum(index);
                SongToggleSetting();
            }
        }
    }
    private void GetSongInAlbum(int index)
    {
        if (playlistQueues.Count > 0)
            playlistQueues.Clear();

        Toggle[] songToggle = songContentList[index].GetComponentsInChildren<Toggle>();


        for (int i = 0; i < listData.albumList[index].songList.Count; i++)
        {
            if (listData.albumList[index].songList[i].songClip != null)
            {
                Playlist playListQueue = new Playlist();

                playListQueue.songToggle = songToggle[i];
                playListQueue.songData = listData.albumList[index].songList[i];

                playlistQueues.Add(playListQueue);
            }     
        }
    }
    public void TurnOffAllSongTogglesExcept(Toggle exceptToggle)
    {
        foreach (Toggle songToggle in songToggleList)
        {
            if (songToggle != exceptToggle)
            {
                songToggle.isOn = false;
            }
        }
    }
    public void GUIContentPlayList(int index)
    {
        for (int i = 0; i < songContentList.Count; i++)
        {
            if (i == index)
            {
                songContentList[i].gameObject.SetActive(true);
                songScrollRect.content = songContentList[i];
            }
            else
                songContentList[i].gameObject.SetActive(false);
        }
    }
    public void GetCurrentPlayList()
    {
        playlistPlaying.Clear();
        for (int i = 0; i < playlistQueues.Count; i++)
        {
            playlistPlaying.Add(playlistQueues[i]);
        }
    }

    [System.Serializable]
    public class Playlist
    {
        public Toggle songToggle;
        public songData songData;
    }
}
