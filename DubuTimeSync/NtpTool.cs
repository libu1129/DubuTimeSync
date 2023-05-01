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
        var ntpTimes = await GetNtpTimesAsync(ntpServers).ConfigureAwait(false);

        // Calculate the median time
        var medianTime = ntpTimes.Where(p => p.clock != null).OrderByDescending(p => p.clock.Now).Median(p => p.clock.UtcNow.ToUnixTimeMilliseconds()); //GetMedianTime(ntpTimes);
        on_log?.Invoke($"중간 시간 : {medianTime.server} {medianTime.clock.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

        // Set system time
        SetSystemTime(medianTime.clock.UtcNow);
        on_log?.Invoke($"시스템 시간 변경 성공");
    }


    public class vm
    {
        public string server;
        public NtpClock? clock;
    }
    private async Task<IEnumerable<vm>> GetNtpTimesAsync(List<string> ntpServers)
    {
        List<Task<vm>> t = new();
        foreach (var a in ntpServers)
        {
            var server = a;
            var tt = Task.Run(async () =>
            {
                var ret = new vm()
                {
                    server = server,
                };
                using CancellationTokenSource ct = new CancellationTokenSource();
                try
                {
                    var client = new NtpClient(server);
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                        try
                        {
                            ct.Cancel();
                            ct.Dispose();
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                    var rcv = await client.QueryAsync(ct.Token);
                    on_log?.Invoke($"poll {server} 시간 수신 성공 ({rcv.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")})");
                    ret.clock = rcv;
                }
                catch (OperationCanceledException ex)
                {
                    on_log?.Invoke($"poll {server} 지정된 시간 내 수신 실패");
                }
                catch (Exception ex)
                {
                    on_log?.Invoke($"poll {server} 시간 수신 실패");
                }
                return ret;
            });
            t.Add(tt);
            await Task.Delay(500);
        }


        return await Task.WhenAll(t).ConfigureAwait(false);
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