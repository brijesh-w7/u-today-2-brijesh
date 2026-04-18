using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class DeviceTypeChecker
{
    public enum ENUM_Device_Type
    {
        Tablet,
        Phone,
        UnknownDevice
    }
    static bool isTablet;
    //public static float PhoneDeviceDiagonalInches = 4.7f;
    public static float TabletDeviceDiagonalInches = 6.5f;
    //public static float TallDeviceDiagonalInches = 5.8f;
    public static bool isDeviceTypeChecked = false;

    private static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        return diagonalInches;
    }

    private static ENUM_Device_Type GetDeviceType()
    {
        float _screenWidth = Mathf.Min(Screen.width, Screen.height);
        float _screenHeight = Mathf.Max(Screen.width, Screen.height);
        float aspectRatio = _screenHeight / _screenWidth;
        bool isTablet = (DeviceDiagonalSizeInInches() > TabletDeviceDiagonalInches && aspectRatio < 2f);
        if (isTablet) return ENUM_Device_Type.Tablet;
        else return ENUM_Device_Type.Phone;
    }

    public static bool IsTabOrIpad
    {
        get
        {
            //return true;
            //if (isDeviceTypeChecked) return isTablet;
            //else
            //{

            if (DeviceTypeChecker.GetDeviceType() == ENUM_Device_Type.Tablet)
            {
                //if (DeviceTypeChecker.DeviceDiagonalSizeInInches() > DeviceTypeChecker.TabletDeviceDiagonalInches)
                //{
                //isDeviceTypeChecked = true;
                return true;
                //}
                //isDeviceTypeChecked = true;
            }
            //isDeviceTypeChecked = true;
            //}
            return false;
        }

    }
}
