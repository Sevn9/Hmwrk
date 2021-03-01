open System
open System.Text.RegularExpressions;
open System.Net.Http
open System.Net
open System.IO
open FSharp.Data

type AsyncMaybeBuilder () =
  member this.Bind(x, f) =
      async {
          let! x' = x
          match x' with
          | Some s -> return! f s
          | None -> return None
          }

  member this.Return x =
      async{return x}

let asyncMaybe = new AsyncMaybeBuilder()

//let GetOper op = 
//  match op with
//  | "+" -> "%2B"
//  | "*" -> "*"
//  | "/" -> "%2F"
//  | "-" -> "-"
//  | _ -> ""

let public StatusCode(responce:HttpWebResponse) = 
  async{
      return 
          match Convert.ToInt32(responce.StatusCode) with
          |200->let s = responce.GetResponseStream()
                let rdr = new StreamReader(s)
                rdr.ReadToEnd()|>Some
          |400 -> None
          |500 ->None
          |_ -> None
  }

//--------------------------------
let private GiveRequest (s:string) =
  async{
      let req = HttpWebRequest.Create(s)
      let rsp = req.GetResponse() :?> HttpWebResponse
      //let! statusAns = StatusCode(rsp)     
      return Some rsp
  }

  //----------------
//let public Calculate(s:string)=
//    async{
//    let url = s.Replace("+", "%2B").Replace("*", "%2A").Replace("/", "%2F")
//    let! result = GiveRequest url
///    return Some result
//    }
//------------------
let public Calculate(s:string) =
  Async.RunSynchronously (asyncMaybe{
      let url = s.Replace("+", "%2B").Replace("*", "%2A").Replace("/", "%2F")
      let address = "http://localhost:53881/calculate?expression=" + url
      let! a = GiveRequest address
      let! b = StatusCode a
      return Some b
  })

let output (result : string option) =
  match result with
      | None -> Console.WriteLine("Bad Request")
      | Some result -> Console.WriteLine(result)


let calc calculateFunction expression =
  calculateFunction expression

[<EntryPoint>]
let main argv =
    let proxyCalc = calc Calculate
    let expression = proxyCalc (Console.ReadLine())
    output expression
    0
