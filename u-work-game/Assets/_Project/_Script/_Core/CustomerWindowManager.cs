using UnityEngine;

public class CustomerWindowManager : SingletonMono<CustomerWindowManager>
{

    public CustomerWindow[] windows = new CustomerWindow[4];

    public void InitializeWindows()
    {
        foreach (var win in windows)
        {
            win?.StopOrder();
            win?.SpawnOrder();
        }

        // Also reset cooking stations
        foreach (var table in FindObjectsOfType<ChoppingTable>())
            table.ForceReset();
        foreach (var stove in FindObjectsOfType<Stove>())
            stove.ForceReset();
    }

    public void StopAll()
    {
        foreach (var win in windows)
            win?.StopOrder();
    }


}
