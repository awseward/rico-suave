@echo off

set fake_args=%*

if not exist paket.lock (
  call "paket.bat" "install"
) else (
  call "paket.bat" "restore"
)

".\packages\FAKE\tools\FAKE.exe" "build.fsx" %fake_args%
