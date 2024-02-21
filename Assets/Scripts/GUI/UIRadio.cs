using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public partial class RadioSetting : MonoBehaviour
{
    [Header("Radio")]
    public Toggle ShuffleTg;
    public Button playBtn, pauseBtn, nextBtn, previousBtn, soundBtn;
    public Slider volumeSlider, seekSlider;
    public Text currentTimeTxt, totalTimeTxt, songNameTxt, authorTxt;
    public Button albumBtn;
    public GameObject albumPanel;
    public RadioMN radioMN;
    bool isShowAlbum = true;
    [Range(0f, 1f)]
    [SerializeField] private float valueVolume = 0f;

    void Update()
    {
        /* if (radioMN.audioSource.clip != null)
         {
             seekSlider.value = radioMN.audioSource.time / radioMN.audioSource.clip.length;
         }*/
    }
    public void EventUISetting()
    {
        playBtn.onClick.AddListener(delegate { OnOffPlayPauseButton(true); radioMN.PlayAudioClip(); seekSlider.interactable = true; });
        pauseBtn.onClick.AddListener(delegate { OnOffPlayPauseButton(false); radioMN.PauseAudioClip(); seekSlider.interactable = true; });
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        nextBtn.onClick.AddListener(delegate { radioMN.NextPlayClip(); });
        previousBtn.onClick.AddListener(delegate { radioMN.PreviousPlayClip(); });


        albumBtn.onClick.AddListener(delegate { ActiveAlbum(); SongToggleSetting(); });

        seekSlider.onValueChanged.AddListener(delegate { ChangeSeek(); });
    }

    public void ActiveAlbum()
    {
        isShowAlbum = !isShowAlbum;  
        albumPanel.SetActive(isShowAlbum);
    }
    public void OnOffPlayPauseButton(bool isOn)
    {
        if (isOn)
        {
            pauseBtn.gameObject.SetActive(true);
            playBtn.gameObject.SetActive(false);
        }
        else
        {
            pauseBtn.gameObject.SetActive(false); 
            playBtn.gameObject.SetActive(true);
        }
    }
    public void GetNameSong(string NameSong, string Author)
    {
        songNameTxt.text = NameSong;
        authorTxt.text = Author;
    }
    public void GUISetting()
    {
        playBtn.enabled = true;
        //nextBtn.enabled = false;
        //previousBtn.enabled = false;
        seekSlider.interactable = false;
    }
    private void UpdateTimeText()
    {
        currentTimeTxt.text = ConvertFloatToTime.FormatTime(radioMN.audioSource.time);
        totalTimeTxt.text = ConvertFloatToTime.FormatTime(radioMN.audioSource.clip.length);
    }
    public void PlayClipInToggle(int index)
    {
        radioMN.PlayClip(index);
    }
    private void ChangeVolume()
    {
        valueVolume = Mathf.Clamp01(valueVolume);
        valueVolume = volumeSlider.value;
        radioMN.GetVolumeValue(valueVolume);
    }
    private void ChangeSeek()
    {
        radioMN.GetTimeAudio(seekSlider);
    }
}
