using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class AlbumSetting : MonoBehaviour
{
    public Text albumNameTxt;
    public string notification;
    public void SetAlbum(string nameAlbum)
    {
        albumNameTxt.text = nameAlbum.ToString();
    }
    public void GetNameAlbum(albumData albumData)
    {
        string nameAlbum;

        if (!string.IsNullOrWhiteSpace(albumData.albumName))
            nameAlbum = albumData.albumName.ToString();
        else
            nameAlbum = notification;

        SetAlbum(nameAlbum);
    }
}
