#r @"./packages/FAKE/tools/FakeLib.dll"
#r @"./packages/Suave/lib/net40/Suave.dll"

open Fake
open Suave
open System
open System.IO

Target "Start" (fun _ ->
  startWebServer defaultConfig (Successful.OK "Hi!")
)

RunTargetOrDefault "Start"
