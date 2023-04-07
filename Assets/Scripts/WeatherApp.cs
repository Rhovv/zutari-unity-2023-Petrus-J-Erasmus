using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class WeatherApp : MonoBehaviour
{
    [Serializable]
    public struct city
    {
        public TMP_Text txtName;
        public TMP_Text txtTemp;
        public TMP_Text txtDescriptionMain;
        public TMP_Text txtDescriptionDetail;
    }

    public List<city> cityNames;

    public Dictionary<string, WeatherData> cities;

    private string appid = "b16a05c469c0fb598f044cfc1bb13baf";

    float Kelvin = 273.15f;

    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Use a boolean in the Controller script to allow the single instance setup where required
        if (!Controller.bWeatherInit)
        {
            Controller.bWeatherInit = true;
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        }

        // Set a dictionary with the city names and their weather data
        cities = new Dictionary<string, WeatherData>();
        foreach (city c in cityNames)
        {
            cities.Add(c.txtName.text, new WeatherData());
        }

        counter = 0;

        // Run a co-routine to get the weather data for each city
        foreach(KeyValuePair<string, WeatherData> c in cities)
        {
            StartCoroutine(getWeatherData(c.Key));
        }
    }

    private IEnumerator getWeatherData(string c)
    {
        // The web request is set up and executed
        DownloadHandlerBuffer downloadHandler;
        var www = new UnityWebRequest("https://api.openweathermap.org/data/2.5/weather?q=" + c + "&appid=" + appid)
        {
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return www.SendWebRequest();

        if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.Log(c + " - error");
            counter++;
            yield break;
        }

        // The returned result is cast to the correct format and saved
        cities[c] = JsonUtility.FromJson<WeatherData>(www.downloadHandler.text);
        counter++;
    }

    private void Update()
    {
        // Once all the cities have their data, the data can be displayed
        if (counter >= cityNames.Count)
        {
            counter = 0;

            foreach (city c in cityNames)
            {
                c.txtTemp.text = "Temperature: " + (cities[c.txtName.text].main.temp - Kelvin).ToString("#0.##") + "°C";
                c.txtDescriptionMain.text = "Info: " + cities[c.txtName.text].weather[0].main;
                c.txtDescriptionDetail.text = "Description: " + cities[c.txtName.text].weather[0].description;
            }
        }
    }

    // Return to the main screen
    public void btnMenu_click(string s)
    {
        Controller.loadScene(s);
    }
}



// The weather data received through the API is in a certain format
[Serializable]
public class weather
{
    public string main;
    public string description;
}

[Serializable]
public class main
{
    public float temp;
}

[Serializable]
public class WeatherData
{
    public List<weather> weather;
    public main main;
}
