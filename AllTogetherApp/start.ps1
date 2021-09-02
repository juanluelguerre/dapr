$WebSite = '--title "WebSite" -- pwsh.exe -Interactive -NoExit -WorkingDirectory ./WebSite -Command dotnet run'

$WeatherService = '--title "WeatherService" -- pwsh.exe -Interactive -NoExit -WorkingDirectory ./WeatherService -Command dotnet run'

$CounterService = '--title "CounterService" -- pwsh.exe -Interactive -NoExit -WorkingDirectory ./CounterService -Command dotnet run'

$cmd = '-M -w -1 nt -d . ' + $WebSite + '; split-pane -d . ' + $WeatherService + '; split-pane -d . ' + $CounterService

Start-Process wt $cmd
Start-Process http://localhost:16686/search
Start-Process https://localhost:5001/
