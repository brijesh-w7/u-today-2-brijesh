using System.Diagnostics;

public class BaseClass
{
    public string GetClassName()
    {
        return GetType().Name;
    }
    public static string GetMethodName()
    {

        return new StackTrace().GetFrame(1).GetMethod().Name;
    }

    public string GetClassMethodName()
    {

        return GetType().Name + " " + new StackTrace().GetFrame(1).GetMethod().Name + " ";
    }

}

