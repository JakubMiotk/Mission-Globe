using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset countryInfoJSON;
    public TextAsset countryScaleJSON;
    public TextAsset territoryInfoJSON;

    [System.Serializable]
    public class CountryInfo
    {
        public string name;
        public string capital;
        public string area;
        public string population;
        public string languages;
        public string religion;
        public string currency;
    }

    [System.Serializable]
    public class CountryInfoList
    {
        public CountryInfo[] countryInfo;
    }

    public CountryInfoList countryInfoList = new CountryInfoList();

    [System.Serializable]
    public class TerritoryInfo
    {
        public string name;
        public string capital;
        public string area;
        public string population;
        public string languages;
        public string religion;
        public string currency;
        public string overlord;
    }

    [System.Serializable]
    public class TerritoryInfoList
    {
        public TerritoryInfo[] territoryInfo;
    }

    public TerritoryInfoList territoryInfoList = new TerritoryInfoList();

    [System.Serializable]
    public class CountryScale
    {
        public string name;
        public float scalex;
    }

    [System.Serializable]
    public class CountryScaleList
    {
        public CountryScale[] countryScales;
    }

    public CountryScaleList countryScaleList = new CountryScaleList();

    public static CountryInfo SearchforCountryInfo(CountryInfoList countryInfoListtemp, string countryName)
    {
        int i = 0;
        CountryInfo countryInfoTemp = null;
        while (countryInfoTemp == null)
        {
            if (countryInfoListtemp.countryInfo[i].name == countryName)
                countryInfoTemp = countryInfoListtemp.countryInfo[i];
            else i++;
        }
        return countryInfoTemp;
    }
    public static CountryInfo SearchforCountryInfoCapital(CountryInfoList countryInfoListtemp, string capitalName)
    {
        int i = 0;
        CountryInfo countryInfoTemp = null;
        while (countryInfoTemp == null)
        {
            if (countryInfoListtemp.countryInfo[i].capital == capitalName)
                countryInfoTemp = countryInfoListtemp.countryInfo[i];
            else i++;
        }
        return countryInfoTemp;
    }
    public static TerritoryInfo SearchforTerritoryInfo(TerritoryInfoList territoryInfoListtemp, string territoryName)
    {
        int i = 0;
        TerritoryInfo territoryInfoTemp = null;
        while (territoryInfoTemp == null)
        {
            if (territoryInfoListtemp.territoryInfo[i].name == territoryName)
                territoryInfoTemp = territoryInfoListtemp.territoryInfo[i];
            else i++;
        }
        return territoryInfoTemp;
    }
    public static TerritoryInfo SearchforTerritoryInfoCapital(TerritoryInfoList territoryInfoListtemp, string capitalName)
    {
        int i = 0;
        TerritoryInfo territoryInfoTemp = null;
        while (territoryInfoTemp == null)
        {
            if (territoryInfoListtemp.territoryInfo[i].capital == capitalName)
                territoryInfoTemp = territoryInfoListtemp.territoryInfo[i];
            else i++;
        }
        return territoryInfoTemp;
    }

    public static CountryScale SearchForCountryScale(CountryScaleList countryScaleListTemp, string countryName)
    {
        CountryScale countryScaleTemp = null;
        for(int i = 0; i<countryScaleListTemp.countryScales.Length; i++)
        {
            if(countryScaleListTemp.countryScales[i].name.Equals(countryName))
            {
                countryScaleTemp = countryScaleListTemp.countryScales[i];
            }
        }
        return countryScaleTemp;
    }
    void Start()
    {
        countryInfoList = JsonUtility.FromJson<CountryInfoList>(countryInfoJSON.text);
        territoryInfoList = JsonUtility.FromJson<TerritoryInfoList>(territoryInfoJSON.text);
    }
    public void LoadCountryScaleList()
    {
        countryScaleList = JsonUtility.FromJson<CountryScaleList>(countryScaleJSON.text);
    }
}
