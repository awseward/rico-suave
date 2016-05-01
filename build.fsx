#r @"./packages/FAKE/tools/FakeLib.dll"
#r @"./packages/Suave/lib/net40/Suave.dll"
#r @"./packages/Newtonsoft.Json/lib/net40/Newtonsoft.Json.dll"

open System
open System.IO

open Fake
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.Net
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

let port = Sockets.Port.Parse <| getBuildParamOrDefault "port" "8083"
let config =
  { defaultConfig with
      bindings = [ HttpBinding.mk Protocol.HTTP IPAddress.Loopback port ]
  }

type Something =
  {
    Id : int
    Name : string
  }

let someInstance =
  {
    Id = 123
    Name = "This is an example thing"
  }

let toJson v =
  let serializerSettings = new JsonSerializerSettings()
  serializerSettings.ContractResolver <-
    new CamelCasePropertyNamesContractResolver()

  JsonConvert.SerializeObject(v, serializerSettings)
  |> OK
  >=> Writers.setMimeType "application/json; charset=utf-8"

let fromJson<'a> json =
  JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

let jsonToSomething (req: HttpRequest) =
  let getString rawForm =
    System.Text.Encoding.UTF8.GetString(rawForm)

  let something =
    req.rawForm
    |> getString
    |> fromJson<Something>

  sprintf "id is %d and name is %s" something.Id something.Name
  |> OK

let app =
  choose
    [ GET >=> choose
        [
          path "/" >=> OK "GET /"
          path "/hello" >=> OK "GET /hello"
          path "/goodbye" >=> OK "GET /goodbye"
          path "/json" >=> toJson someInstance ]
      POST >=> choose
        [
          path "/" >=> OK "POST /"
          path "/hello" >=> OK "POST /hello"
          path "/goodbye" >=> OK "POST /goodbye"
          path "/json" >=> request jsonToSomething ] ]

startWebServer config app
