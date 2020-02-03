using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class helpers
{

    //
    // getXYPos (should actually be get XZPos
    // todo: maybe refactor with a better method name at some stage
    // see: https://stackoverflow.com/questions/1369512/converting-longitude-latitude-to-x-y-on-a-map-with-calibration-points
    //
    public static float[] getXYPos(float lat, float lon, float scaleX, float scaleY)
    {
        float[] xy = new float[2];
        
        lon = lon - 140;
        float x = (scaleY * lon / 180); //- 180;
        float y = (scaleX * lat / 360);


        //float x = (scaleX / 360.0f) * (180 + lon);
        //float y = (scaleY / 180.0f) * (90 - lat);

        // float x = (scaleX) *(180 + lat) / 360;
        // float y = (scaleY) *(90 - lon) / 180;

        xy[0] = x;
        xy[1] = y;
        return xy;
    }

    // int x =  (int) ((MAP_WIDTH/360.0) * (180 + lon));
    // int y = (int)((MAP_HEIGHT / 180.0) * (90 - lat));

    // x = (total width of image in px) * (180 + latitude) / 360
    // y = (total height of image in px) * (90 - longitude) / 180


    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

//
// JsonHelper class for extending Unity's JsonUtility to work with json with multiple data objects (like we have)
// see: https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
// 
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}