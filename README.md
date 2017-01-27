# VisualTalkback
A visual talkback system for radio stations.

# Build status

[![Build status](https://ci.appveyor.com/api/projects/status/bk73lfoxbetb97p1/branch/master?svg=true)](https://ci.appveyor.com/project/jonesm13/visualtalkback/branch/master)

# Overview

This project is a way to provide visual talkback between a producer and a presenter/operator in one or many studios, typically for the purposes of providing information about phone-ins, sports scores and so on.

The project consists of two applications; a studio application and a producer application. As suggested, the studio application runs on the studio machine; the producer application on a machine in a production area.

The two applications communicate via simple HTTP POST requests; the producer sets text on the studio client by POSTing a blob of plain text in the body of the request.

![Producer client](/wiki/producer.png)
![Studio client](/wiki/studio.png)

# Configuration
## Studio

In app.config...

```
<appSettings>
  <add key="studio.http.listenPort" value="9926" /> 
</appSettings>
```

## Producer

In app.config, set the ```producer.studios.addresses``` app settings key to a comma-delimited list of studios. Each member in the comma-delimited list is itself a pipe-delimited list of name|URL.

```
<appSettings>
   <add key="producer.studios.addresses" value="Test|http://localhost:9926/talkback/,A|http://192.168.7.123:9926/talkback,B|http://192.168.7.124:9926/talkback" />
</appSettings>

```

To test the Producer application, add a LOOPBACK into the above key. This will add a Loopback destination in the list of available studios.

# Installation

The studio client needs to be run with elevated permissions at first use, or any time the endpoint is changed. This is because the url needs to be added to Windows ACL to give Nancy permissions to listen on a given URL.

# Backlog

[X] As a producer, I want the program to tell me if it can't send the text to one of the studios, so that I know that the studio has got the message.

[ ] Tech story - Make the SimpleHttpClientStudio perform the POST in a thread, so as not to block the main UI thread.

[ ] As a presenter, I want to be able to type messages back to the producer.

[ ] As a producer, I want the presenter to see the changes I make as I type them, rather than having to press the Send button.
