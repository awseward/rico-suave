#r @"./packages/FAKE/tools/FakeLib.dll"
#r @"./packages/Suave/lib/net40/Suave.dll"

open System
open System.IO

open Fake
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.Net

let port = Sockets.Port.Parse <| getBuildParamOrDefault "port" "8083"
let config =
  { defaultConfig with
      bindings = [ HttpBinding.mk Protocol.HTTP IPAddress.Loopback port ]
  }

let app =
  choose
    [ GET >=> choose
        [
          path "/" >=> OK "GET /"
          path "/hello" >=> OK "GET /hello"
          path "/goodbye" >=> OK "GET /goodbye" ]
      POST >=> choose
        [
          path "/" >=> OK "POST /"
          path "/hello" >=> OK "POST /hello"
          path "/goodbye" >=> OK "POST /goodbye" ] ]

startWebServer config app
