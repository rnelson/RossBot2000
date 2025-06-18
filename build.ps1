dotnet build src/RossBot2000.sln
Remove-Item -Recurse -Force dist
New-Item -ItemType Directory -Path dist
Copy-Item -Path src/RossBot2000/bin/Debug/net9.0/* -Destination dist -Recurse
Copy-Item -Path src/RossBot2000.Abstractions/bin/Debug/net9.0/* -Destination dist
Copy-Item -Path src/RossBot2000.Modules/bin/Debug/net9.0/* -Destination dist
Get-ChildItem src/Modules | ForEach-Object {
	Copy-Item -Path (Get-ChildItem -Path (Join-Path -Path $_.FullName -ChildPath "bin/Debug/net9.0") -Filter "*Module.dll").FullName -Destination dist
}