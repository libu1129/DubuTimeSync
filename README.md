# DubuTimeSync


Windows-based

The system time must match exactly in seconds
If you get the time from one ntp server, the time can be wrong for a few seconds
To reduce time errors
If you get time from NTP

Gets the time from multiple ntp servers and sets the time as an intermediate value

By default, you get the time value from all of the time servers below and set the system time as the middle value

"time.google.com",
"time.windows.com",
"time.bora.net",
"kr.pool.ntp.org",
"time.facebook.com",
"time.apple.com",

Attempting to renew the time every minute.
The server that fails to get the time within a second ignores the value
