// See https://aka.ms/new-console-template for more information



using GuerrillaNtp;

using System.Diagnostics;

var ntpServers = new List<string>() {
                "time.google.com",
                "time.windows.com",
                "time.bora.net",
                "kr.pool.ntp.org",
                "time.facebook.com",
                "time.apple.com",
            };



List<Task<item_vm>> t = new();
foreach (var a in ntpServers)
{
    var server = a;
    var tt = Task.Run(async () =>
    {
        var ret = new item_vm()
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
            Console.WriteLine($"poll {server}\t 시간 수신 성공\t ({rcv.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")})");
            ret.clock = rcv;
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine($"poll {server}\t 시간 내 수신 실패\t");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"poll {server}\t 시간 수신 실패\t");
        }
        return ret;
    });
    t.Add(tt);
    await Task.Delay(500);
}

var ntpTimes = await Task.WhenAll(t).ConfigureAwait(false);


// Calculate the median time
var medianTime = ntpTimes
    .Where(p => p.clock != null)
    .OrderByDescending(p => p.clock.Now)
    .Median(p => p.clock.UtcNow.ToUnixTimeMilliseconds()); //GetMedianTime(ntpTimes);

var dtm = medianTime.clock; //이게 로컬타임임





Console.WriteLine($"중간 시간 : {medianTime.server} UTC({dtm.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}) Local({dtm.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")})");

//Console.WriteLine(medianTime.clock.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

var process = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "/bin/bash",
        Arguments = "-c \"" + $"date --set=\"{dtm.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\"  && date --rfc-3339=ns" + "\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true,
    }
};
process.Start();

string output = process.StandardOutput.ReadToEnd();
await process.WaitForExitAsync();
Console.WriteLine(output);
Console.WriteLine("시스템 시간 설정 완료");

public class item_vm
{
    public string server;
    public NtpClock? clock;
}
