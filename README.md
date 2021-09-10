# SmartTheme

[![NuGet Version](https://img.shields.io/nuget/v/SmartTheme.Core.svg?style=flat)](https://www.nuget.org/packages/SmartTheme.Core/)
[![NuGet Version](https://img.shields.io/nuget/v/SmartTheme.Tags.svg?style=flat)](https://www.nuget.org/packages/SmartTheme.Tags/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/Cyrus-Sushiant/SmartTheme/master/LICENSE)

A Serilog sink that writes events as messages to [Telegram](https://telegram.org/).

All you need is bot token and chat id. To manage bots go to [Bot Father](https://telegram.me/botfather). After you got bot token add bot to contacts and start it (`/start`). To get your id navigate to https://api.telegram.org/botTOKEN/getUpdates

## Available for
* .Net 5.0

# Install
```
Install-Package SmartTheme.Core
Install-Package SmartTheme.Tags
```

## Usage:
```csharp
services.AddSmartThemeCoreServices();
```