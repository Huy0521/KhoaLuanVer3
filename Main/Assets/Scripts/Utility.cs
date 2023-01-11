using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using System.IO;

public static class Vector3Extensions
{
    public static Rect GetWorldRect(this RectTransform rt)
    {
        // Convert the rectangle to world corners and grab the top left
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 topLeft = corners[0];
        Vector2 scale = rt.lossyScale;
        // Rescale the size appropriately based on the current Canvas scale
        Vector2 scaledSize = new Vector2(scale.x * rt.rect.size.x, scale.y * rt.rect.size.y);

        return new Rect(topLeft, scaledSize);
    }

    public static Rect GetScreenRect(this RectTransform rt)
    {
        Vector3 botLeft = rt.GetScreenBotLeftPoint();
        Vector3 topRight = rt.GetScreenTopRightPoint();
        Vector2 size = new Vector2(topRight.x - botLeft.x, topRight.y - botLeft.y);
        Rect rect = Rect.MinMaxRect(botLeft.x, botLeft.y, topRight.x, topRight.y);

        return rect;
    }

    public static Vector3 GetScreenBotLeftPoint(this RectTransform rt)
    {
        // Convert the rectangle to world corners and grab the top left
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = Camera.main.WorldToScreenPoint(corners[i]);
        }
        Vector3 topLeft = corners[2];

        return topLeft;
    }

    public static Vector3 GetScreenTopRightPoint(this RectTransform rt)
    {
        // Convert the rectangle to world corners and grab the top left
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        //corners.PrintDebug();
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = Camera.main.WorldToScreenPoint(corners[i]);
        }
        //corners.PrintDebug();
        Vector3 topRight = corners[0];

        return topRight;
    }

    public static Vector2 GetWorldsize(this RectTransform rt)
    {
        return rt.sizeDelta * rt.lossyScale;
    }

    public static Vector3 Round(this UnityEngine.Vector3 vector3, int roundDecimal)
    {
        vector3.x = (float)Math.Round(vector3.x, roundDecimal);
        vector3.y = (float)Math.Round(vector3.y, roundDecimal);
        vector3.z = (float)Math.Round(vector3.z, roundDecimal);
        return vector3;
    }

    public static Rect RectScreenSpace(this RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        //size.PrintDebug();
        Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
        rect.x -= (transform.pivot.x * size.x);
        rect.y -= ((1.0f - transform.pivot.y) * size.y);
        return rect;
    }
}

public class Utility
{
    public static FieldInfo[] GetFieldInfo(Type t, BindingFlags bindingFlags)
    {
        return t.GetFields(bindingFlags);
    }

    public static T GetCustomAttribute<T>(Enum e) where T : Attribute
    {
        return GetCustomAttributes<T>(e).FirstOrDefault();
    }

    public static List<T> GetCustomAttributes<T>(Enum e) where T : Attribute
    {
        List<T> attrs = new List<T>();
        FieldInfo fi = e.GetType().GetField(e.ToString());
        object[] objs = fi.GetCustomAttributes(typeof(T), false);
        foreach (object a in objs)
            if (a is T variable) attrs.Add(variable);
        return attrs;
    }

    public static string ConvertDayName(DayOfWeek dow)
    {
        List<string> vnDayName = new List<string>()
        {
            "Chủ nhật","Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy",
        };
        return vnDayName[(int)dow] ?? string.Empty;
    }

    public static TimeSpan TimeOffset(string offsetString)
    {
        if (string.IsNullOrEmpty(offsetString)) return DateTimeOffset.Now.Offset;
        bool isNegative = offsetString.StartsWith("-");
        string format = isNegative ? "\\-hh:mm" : "\\+hh:mm";
        var tss = isNegative ? TimeSpanStyles.AssumeNegative : TimeSpanStyles.None;
        return TimeSpan.TryParseExact(offsetString, format, null, tss, out var ts) ? ts : DateTimeOffset.Now.Offset;
    }

    public static int ClearTransform(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
        return transform.childCount;
    }

    public static string ConvertToString(Enum eff)
    {
        return Enum.GetName(eff.GetType(), eff);
    }

    public static T NextEnum<T>(T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");
        T[] arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(arr, src) + 1;
        return (arr.Length == j) ? arr[0] : arr[j];
    }

    public static T ToEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static DateTime UnixTimestampToDateTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }

    public static bool IsScreen4_3 => UnityEngine.Screen.width / 3 == UnityEngine.Screen.height / 4;

    public static bool IsScreen16_9 => UnityEngine.Screen.width / 9 == UnityEngine.Screen.height / 16;

    public static string ConvertMilisecondsToString(float ms)
    {
        string result = string.Empty;
        if (!float.IsInfinity(ms))
        {
            System.TimeSpan t = System.TimeSpan.FromMilliseconds(ms);
            //Debug.LogWarning(t.ToString());
            if (t.TotalHours < 1)
            {
                result = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            }
            else
            {
                if (t.TotalHours < 100)
                {
                    result = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)t.TotalHours, t.Minutes, t.Seconds);
                }
                else
                {
                    result = string.Format("{0:D3}:{1:D2}:{2:D2}", (int)t.TotalHours, t.Minutes, t.Seconds);
                }
            }
        }
        else
        {
            result = "00:00";
        }
        return result;
    }

    public static string convertSecondToString(double second)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(second);
        string result = string.Empty;
        result = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

        return result;
    }

    public static string convertPageStr(int page, int totalPage)
    {
        string result = string.Empty;
        result = string.Format("{0:D2}/{1:D2}", page, totalPage);
        return result;
    }

    public static string convertNumber(int page)
    {
        string result = string.Empty;
        result = string.Format("{0:D2}", page);
        return result;
    }

    public static string convert2Number(string page)
    {
        int variable = 0;
        int.TryParse(page, out variable);

        string result = string.Empty;
        result = string.Format("{0:D2}", variable);
        return result;
    }

    public static float DistancePointToRectangle(Vector2 point, Rect rect)
    {
        //  Calculate a distance between a point and a rectangle.
        //  The area around/in the rectangle is defined in terms of
        //  several regions:
        //
        //  O--x
        //  |
        //  y
        //
        //
        //        I   |    II    |  III
        //      ======+==========+======   --yMin
        //       VIII |  IX (in) |  IV
        //      ======+==========+======   --yMax
        //       VII  |    VI    |   V
        //
        //
        //  Note that the +y direction is down because of Unity's GUI coordinates.

        if (point.x < rect.xMin)
        { // Region I, VIII, or VII
            if (point.y < rect.yMin)
            { // I
                Vector2 diff = point - new Vector2(rect.xMin, rect.yMin);
                return diff.magnitude;
            }
            else if (point.y > rect.yMax)
            { // VII
                Vector2 diff = point - new Vector2(rect.xMin, rect.yMax);
                return diff.magnitude;
            }
            else
            { // VIII
                return rect.xMin - point.x;
            }
        }
        else if (point.x > rect.xMax)
        { // Region III, IV, or V
            if (point.y < rect.yMin)
            { // III
                Vector2 diff = point - new Vector2(rect.xMax, rect.yMin);
                return diff.magnitude;
            }
            else if (point.y > rect.yMax)
            { // V
                Vector2 diff = point - new Vector2(rect.xMax, rect.yMax);
                return diff.magnitude;
            }
            else
            { // IV
                return point.x - rect.xMax;
            }
        }
        else
        { // Region II, IX, or VI
            if (point.y < rect.yMin)
            { // II
                return rect.yMin - point.y;
            }
            else if (point.y > rect.yMax)
            { // VI
                return point.y - rect.yMax;
            }
            else
            { // IX
                return 0f;
            }
        }
    }

    //tungvt4
    public static String UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        unixTimeStamp = unixTimeStamp / 1000;
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;
    }

    public static string CalculateMD5Hash(string input)
    {
        // step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    public static void Shuffle<T>(T[] decklist)
    {
        T tempGO;

        for (int i = 0; i < decklist.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, decklist.Length);
            tempGO = decklist[rnd];
            decklist[rnd] = decklist[i];
            decklist[i] = tempGO;
        }
    }

    public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        //Sprite NewSprite = new Sprite();
        Texture2D SpriteTexture = LoadTexture(FilePath);

        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        //string[] names = FilePath.Split('/');
        //string lastValue = names[names.Length - 1];
        //string fName = lastValue.Split('.')[0];

        string firstValue = FilePath.Split('.')[0];
        string fName = firstValue.Substring(firstValue.Length - 2);

        NewSprite.name = fName;
        return NewSprite;
    }

    public static Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    public static string ReadString(string filePath)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(filePath);
        string content = reader.ReadToEnd();
        Debug.Log(reader.ReadToEnd());
        reader.Close();
        return content;
    }

    public static void WriteString(string filePath, string content)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(filePath, true);
        writer.WriteLine(content);
        writer.Close();
        ////Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(filePath);
        //TextAsset asset = Resources.Load("test");
        ////Print the text from the file
        //Debug.Log(asset.text);
    }

    public static string getImagePath(string path)
    {
        var url = path;
#if UNITY_IOS
        url = "file:///" + path;

#elif UNITY_ANDROID
        url = "file:///" + path;
#else
 
#endif

        return url;

    }

    public static Sprite loadEmoji(string emoName)
    {
        return Resources.Load<Sprite>("emoji/" + emoName);
    }
}

class AcceptAllCertificates : UnityEngine.Networking.CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
            return true;
    }
}