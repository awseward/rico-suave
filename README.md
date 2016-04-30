# rico-suave
Trying out the suave web framework

#### Start
```sh
./fake.bat build.fsx
```

#### Stop
Just `Ctrl` + `c`

If you started it from cygwin, the FAKE.exe process probably won't just die
willingly.

This should work for that case
```sh
taskkill /im fake.exe /f
```

#### Configuring IIS
* Install the [HttpPlatformHandler plugin](http://www.iis.net/downloads/microsoft/httpplatformhandler)
* Need to set "Handler Mappings" to read/write (described [here](http://stackoverflow.com/a/12343141))

* Clone the repo on to the server
* Add the site to IIS (just follow the wizard)
