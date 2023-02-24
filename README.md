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
RossBot2000 is written in .NET 7 using the Discord.Net package. It will run on any system supported by 
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