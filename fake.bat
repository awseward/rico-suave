@echo off

set fake_args=%*

call "paket.bat" "install"

".\packages\FAKE\tools\FAKE.exe" "build.fsx" %fake_args%
