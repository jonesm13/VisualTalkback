# VisualTalkback
A visual talkback system for radio stations.

# Overview

The project consists of two projects; a studio  application and a producer application.

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

# Installation

The studio client needs to be run with elevated permissions at first use, or any time the endpoint is changed. This is because the url needs to be added to Windows ACL to give Nancy permissions to listen.

