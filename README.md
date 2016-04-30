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
