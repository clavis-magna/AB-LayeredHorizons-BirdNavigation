﻿// loading in and displaying location based data
// trying to be as generic and flexible as possible
// csv created from a google sheet

// madatory data and column headings
//
// latitude and longitude

// optional data 

// display a word or short text
// - check 'diplay word' in inspector 
// - specify column heading for text data in inpector
// - drag on game object to represent text to 'Text marker' in inpector
// - also specify a layer parent game object to keep things tidy in the editor

// 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;

public class GenericLocationBasedData : MonoBehaviour
{
    // list to hold data
    List<Dictionary<string, object>> data;

    // csv filename
    // in streaming assets (include .csv extension)
    public string CSVFileName = "data.csv";

    public string latitudeColumnHeader;
    public string longitudeColumnHeader;

    public GameObject marker;

    public bool displayWord;
    public string wordColumnHeader;
    public float fontSize = 12;
    public float yHeight = 5;
    public Color textColor;

    public bool hasAudio;
    public string audioColunmHeader;

    public GameObject layerParent;

    public bool runTest = false;

    // holders for the map scale set in start method
    private int scaleX;
    private int scaleY;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("runsetup");

        // grab world scale from the commonData script
        // set in the inspector
        scaleX = 400000; // (int)commonData.mapScale.x;
        scaleY = 200000; // (int)commonData.mapScale.y;

        // create the file path to the json data as a string
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, CSVFileName);
        // error check (make sure there actually is a file
        if (File.Exists(dataFilePath))
        {
            // need some error checking on this.
            // currently failing silently
            data = CSVReader.Read(dataFilePath);

            // test read of data
            for (var i = 0; i < data.Count; i++)
            {
                // display short arbitrary text at a lat/lon location
                //

                // convert from lat/long to world units
                // using the helper method in the 'helpers' script
                float[] thisXY = helpers.getXYPos(float.Parse(data[i][latitudeColumnHeader].ToString()), float.Parse(data[i][longitudeColumnHeader].ToString()), scaleX, scaleY);
                GameObject thisMarker = Instantiate(marker, new Vector3(thisXY[0], 0.05f, thisXY[1]), Quaternion.identity);

                if (displayWord)
                {
                    // creating from scratch version
                    // create empty gameobject and name it the text we want to display
                    GameObject TextObj = new GameObject((string)data[i][wordColumnHeader]);
                    // create a text label as child
                    GameObject TextLabel = new GameObject("label");
                    TextMeshPro tmp = TextLabel.AddComponent<TextMeshPro>();
                    tmp.text = (string)data[i][wordColumnHeader];
                    tmp.fontSize = fontSize;
                    //tmp.color = textColor;
                    tmp.alignment = TextAlignmentOptions.Center;
                    // add lookat script
                    TextLabel.AddComponent<lookAtCamera>();
                    TextLabel.transform.parent = TextObj.transform;
                    TextLabel.transform.localPosition = TextLabel.transform.localPosition + new Vector3(0, yHeight, 0);
                    // now move the whole things to its correct location
                    TextObj.transform.position = new Vector3(thisXY[0], 0.05f, thisXY[1]);
                    TextObj.transform.parent = layerParent.transform;

                }
            }
        }

        // this is a test for reading in data file in using webrequest from the streaming assets folder
        if (runTest)
        {
            StartCoroutine("getData");
        }
    }

    IEnumerator runsetup()
    {
        yield return 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator getData()
    {
        // test for text asset from streaming assets
        string myFileURI = Path.Combine(Application.streamingAssetsPath, "cat.txt");

        UnityWebRequest www = UnityWebRequest.Get(myFileURI);
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            // Get text content like this:
            Debug.Log(www.downloadHandler.text);
        }

        // test for audio asset from streaming assets
        string audioFileURI = Path.Combine(Application.streamingAssetsPath, "bird.ogg");

        UnityWebRequest audiowww = UnityWebRequestMultimedia.GetAudioClip(audioFileURI, AudioType.OGGVORBIS);
        yield return audiowww.SendWebRequest();

        if (!audiowww.isNetworkError && !audiowww.isHttpError)
        {
            AudioClip myClip = DownloadHandlerAudioClip.GetContent(audiowww);
            print("audioFileName --------------" + myClip);
        }

        // test for image asset from streaming assets
        string imageFileURI = Path.Combine(Application.streamingAssetsPath, "dog.jpg");

        UnityWebRequest imagewww = UnityWebRequestTexture.GetTexture(imageFileURI);
        yield return imagewww.SendWebRequest();

        if (!imagewww.isNetworkError && !imagewww.isHttpError)
        {
            var texture = DownloadHandlerTexture.GetContent(imagewww);
            print("image --------------" + texture);
        }
    }
}
