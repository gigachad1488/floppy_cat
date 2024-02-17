using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace floppy_cat;

public static class UpdateHandler
{
    public delegate void Update();

    public static event Update? updateEvent;

    public const int frameTime = 10;

    public static int time = 0;

    public static bool end = false;

    public static int timeScale = 1;

    public async static void Init()
    {
        updateEvent = null;
        end = false;
        time = 0;
        timeScale = 1;
        await UpdateInvoke();
    }

    private async static Task UpdateInvoke()
    {
        while (!end)
        {
            Console.WriteLine("NEW FRAME " + time);
            time += frameTime;
            updateEvent?.Invoke();

            await Task.Delay(frameTime * timeScale);
        }
        
    }
}