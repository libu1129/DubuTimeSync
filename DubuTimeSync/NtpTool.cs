using GuerrillaNtp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static DubuTimeSync.Form1;

namespace DubuTimeSync;

public class NtpTool
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetSystemTime(ref SYSTEMTIME st);



    [DllImport("libc", SetLastError = true)]
    private static extern int clock_settime(int clockId, ref timespec tp);

    public event Action<string> on_log;


    public async Task sync(List<string> ntpServers)
    {
        // Fetch the time from NTP servers
        var ntpTimes = await GetNtpTimesAsync(ntpServers);

        // Calculate the median time
        var medianTime = ntpTimes.Where(p => p != null).Median(p => p.UtcNow.ToUnixTimeMilliseconds()); //GetMedianTime(ntpTimes);
        Console.WriteLine($"Median Time: {medianTime}");

        // Set system time
        SetSystemTime(medianTime.Now);
    }

    private static async Task<IEnumerable<NtpClock>> GetNtpTimesAsync(List<string> ntpServers)
    {
        var tasks = ntpServers
            .Select(async server =>
            {
                try
                {
                    var client = new NtpClient(server);
                    return await client.QueryAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            });

        return await Task.WhenAll(tasks);
    }

    private static void SetSystemTime(DateTimeOffset medianTime)
    {
        // Windows
        var sysTime = new SYSTEMTIME
        {
            wYear = (ushort)medianTime.Year,
            wMonth = (ushort)medianTime.Month,
            wDay = (ushort)medianTime.Day,
            wHour = (ushort)medianTime.Hour,
            wMinute = (ushort)medianTime.Minute,
            wSecond = (ushort)medianTime.Second,
            wMilliseconds = (ushort)medianTime.Millisecond
        };

        SetSystemTime(ref sysTime);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }


    private struct timespec
    {
        public long tv_sec;
        public long tv_nsec;
    }


}