# RossBot2000

## About
Someone added Mee6 to a Discord server and 
created a custom command. When asked to create 
a second, they pointed out that you are limited 
to just one. After pointing out that they could 
easily make something to handle multiple commands, 
I figured I should probably make sure that it's 
actually easy. And thus RossBot2000 was born.

## Requirements
RossBot2000 is written in .NET 8 using the Discord.Net package. It will run on any system supported by 
.NET 7. You will need to have an active Discord account to be able to create the application/bot and to 
add it to a server.

## Usage
1. Create [a new Discord application](https://discord.com/developers/applications).
2. Under Settings > Bot, add a bot and give your bot a cool name.
3. Under Settings > OAuth2 > URL Generator, select the `bot` scope, then under *General Permissions* select "Read Messages/View Channels" and select everything under Text Permissions.
4. Click the Copy button, and visit that URL.
5. Add your bot to your server.
6. Customize the `appsettings.json` file to your needs.
7. Run the bot with `dotnet run`.

### SystemD
The contents of my `/etc/systemd/system/rossbot2000` are as follows:

```ini
[Unit]
Description=RossBot2000
DefaultDependencies=false
Before=basic.target shutdown.target
Conflicts=shutdown.target

[Service]
Type=notify
User=rnelson
Group=rnelson
ExecStart=/bin/sh /opt/rossbot.sh

[Install]
WantedBy=multi-user.target
```

The script it calls simply runs `dotnet run` with the specific environment I want:

```shell
#!/bin/sh
(cd /opt/RossBot2000 && dotnet run --environment RossBot2000)
```

## License

RossBot2000 is released under the [MIT License](http://rnelson.mit-license.org).
