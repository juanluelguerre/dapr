$WebApp1Dapr = 'dapr.exe run --app-id webapp1 --app-port 5001 --dapr-http-port 3502  dotnet run -- --urls=https://localhost:5001/ -p WebApp1/WebApp1.csproj'
$WebApp1 = '--title "WebApp1" -- pwsh.exe -Interactive -NoExit -Command ' + "$WebApp1Dapr"

$FunctionsDapr = 'dapr.exe run --app-id hello-functions --dapr-http-port 3501 -p 3001 --components-path .\dapr\components -- func host start'
$Functions = '--title "Hello-Functions" -- pwsh.exe -Interactive -NoExit -Command ' + "$FunctionsDapr"

$cmd = '-M -w -1 nt -d . ' + $WebApp1 + '; split-pane -d . ' + $Functions

Start-Process wt $cmd
