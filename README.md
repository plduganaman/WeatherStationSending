# WeatherStationSending
Windows App in System Tray to inform if your station is sending data or not
You will need to have an account setup in Weather Underground to get your station ID and API

Green up arrow is good, everything is working as expected.
Red flashing arrow is bad, something is wrong in the settings.  (Bad API or bad station id or connectivity issues)

Be sure to have the settings.xml in the same folder as the exe.  Launch the exe and it should start up with default values.
Right click the tray icon and select "Options" so you can modify the settings.xml with the correct station id and api.

"How Often to Check in Minutes" is just that. How often do we poll the WU site.
"Overdue Time in Minutes" compares the latest observed time from WU to the current time. If greater than this value flag as a problem.
"Display Result" checkbox throws a window every time wee check and the results of that check.
"Result logging" writes to a WeatherStationSending.log file with any errors or result messages.  That file will be located in the same folder as the exe.

"Save Settings" simply saves the changes to the settings and applies them to the exe. 




