using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeCounter {

    public static float miliseconds, seconds, minutes, hours, startTimer;
    public static void AddTime(float t) {
        miliseconds += t;
        if (miliseconds >= 1) {
            miliseconds -= 1;
            seconds += 1;
        }
        else if(seconds >= 60) {
            seconds -= 60;
            minutes += 1;
        }
        else if(minutes >= 60) {
            minutes -= 60;
            hours += 1;
        }
    }
    public static float TotalTime() {
        return hours * 3600 + minutes * 60 +seconds + miliseconds * 0.01f;
    }
    public static string GetTime() {
        return string.Format(
            "{0}:{1}:{2}{3}",
            hours.ToString("00.##"), minutes.ToString("00.##"), seconds.ToString("00.##"), miliseconds.ToString("#.##"));
    }
    public static void TimeReset() {
        hours = 0;
        minutes = 0;
        seconds = 0;
        miliseconds = 0;

        startTimer = 0;
    }
}
