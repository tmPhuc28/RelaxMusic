using UnityEngine;
using UnityEngine.UI;

public class SongSetting : MonoBehaviour
{
    public Text songNameTxt;
    public Text authorTxt;
    public Text totalTimeTxt;
    [SerializeField] private Toggle tg;
    [SerializeField] private GameObject nonCheck;
    public songData songData;
    public string notification;
    private void Start()
    {
        tg.onValueChanged.AddListener(ToggleSetting);
    }

    public void SetSong(string name, string author, string totalTime)
    {
        songNameTxt.text = name.ToString();

        authorTxt.text = author.ToString();

        totalTimeTxt.text = totalTime.ToString();
    }public void GetData(songData songData)
    {
        this.songData = songData;
        SongSettingData();
    }
    public void SongSettingData()
    {
        string nameSong, author, totalTime;

        if (!string.IsNullOrWhiteSpace(songData.songName))
            nameSong = songData.songName.ToString();
        else
            nameSong = notification;

        if (!string.IsNullOrWhiteSpace(songData.author))
            author = songData.author.ToString();
        else
            author = "...";

        totalTime = ConvertFloatToTime.FormatTime(songData.songClip.length);
        SetSong(nameSong, author, totalTime);
    }
   
    public void ToggleSetting(bool isOn)
    {
        if (tg.isOn)
            nonCheck.SetActive(false);
        else
            nonCheck.SetActive(true);
    }
}
