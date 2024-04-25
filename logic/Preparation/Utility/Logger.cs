﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Timothy.FrameRateTask;

namespace Preparation.Utility.Logging;

public class LogQueue
{
    public static LogQueue Global { get; } = new();
    private static uint logNum = 0;
    private static uint logCopyNum = 0;
    private static readonly object queueLock = new();

    private readonly Queue<string> logInfoQueue = new();

    public async Task Commit(string info)
    {
        await Task.Run(() =>
        {
            lock (queueLock) logInfoQueue.Enqueue(info);
        });
    }

    private LogQueue()
    {
        new Thread(() =>
        {
            new FrameRateTaskExecutor<int>(
                loopCondition: () => Global != null,
                loopToDo: () =>
                {
                    lock (queueLock)
                    {
                        while (logInfoQueue.Count != 0)
                        {
                            var info = logInfoQueue.Dequeue();
                            File.AppendAllText(LoggingData.ServerLogPath, info + Environment.NewLine);
                            logNum++;
                            if (logNum >= LoggingData.MaxLogNum)
                            {
                                File.Copy(LoggingData.ServerLogPath,
                                          $"{LoggingData.ServerLogPath}-copy{logCopyNum}.txt");
                                logCopyNum++;
                                File.Delete(LoggingData.ServerLogPath);
                                logNum = 0;
                            }
                        }
                    }
                },
                timeInterval: 200,
                finallyReturn: () => 0
                ).Start();
        })
        { IsBackground = true }.Start();
    }
}

public class Logger(string module)
{
    public readonly string Module = module;

    public void ConsoleLog(string msg, bool Duplicate = true)
    {
        var info = $"[{Module}]{msg}";
        Console.WriteLine(info);
        if (Duplicate)
            _ = LogQueue.Global.Commit(info);
    }
    public void ConsoleLogDebug(string msg, bool Duplicate = true)
    {
#if DEBUG
        var info = $"[{Module}]{msg}";
        Console.WriteLine(info);
        if (Duplicate)
            _ = LogQueue.Global.Commit(info);
#endif
    }
    public static string TypeName(object obj)
        => obj.GetType().Name;
    public static string TypeName(Type tp)
        => tp.Name;
    public static string ObjInfo(object obj, string msg = "")
        => msg == "" ? $"<{TypeName(obj)}>"
                     : $"<{TypeName(obj)} {msg}>";
    public static string ObjInfo(Type tp, string msg = "")
        => msg == "" ? $"<{TypeName(tp)}>"
                     : $"<{TypeName(tp)} {msg}>";
}

public static class LoggingData
{
    public const string ServerLogPath = "log.txt";
    public const uint MaxLogNum = 5000;
}
