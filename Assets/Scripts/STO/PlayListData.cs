using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadioData", menuName = "ScriptableObjects/RadioData")]
public class PlayListData : ScriptableObject
{
    public List<albumData> albumList = new List<albumData>();
}
[System.Serializable]
public class albumData
{
    public string albumName;
    public List<songData> songList;

}

[System.Serializable]
public class songData
{
    public string songName;
    public string author;
    public AudioClip songClip;
}