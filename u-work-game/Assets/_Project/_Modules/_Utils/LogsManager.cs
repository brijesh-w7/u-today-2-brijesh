using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LogsManager : MonoBehaviour
{

    private static string DEVIDER = "  ||  ";

    /*
    * Use this method for important message like response and so on
    */
    [System.Diagnostics.Conditional("Brijesh_ENABLE_LOGS")]
    public static void Log(params object[] messages)
    {

        string printableMessage = "  ";
        try
        {

            foreach (object element in messages)
            {
                if (element is int)
                {
                    printableMessage += (int)element;
                }
                else if (element is float)
                {
                    printableMessage += (float)element;
                }

                else if (element is double)
                {
                    printableMessage += (double)element;
                }

                else if (element is long)
                {
                    printableMessage += (long)element;
                }

                else if (element is string)
                {
                    printableMessage += (string)element;
                }

                else if (element is object)
                {
                    printableMessage += element;
                }

                else if (element is UnityEngine.Object)
                {
                    printableMessage += element;
                }
                printableMessage += DEVIDER;

            }


        }
        catch (Exception ex)
        {

        }
        MonoBehaviour.print(printableMessage);
    }



}

