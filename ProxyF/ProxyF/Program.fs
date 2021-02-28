open System
open System.Text.RegularExpressions;
open System.Net.Http
open System.Net
open System.IO

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

let GetOper op = 
  match op with
  | "+" -> "%2B"
  | "*" -> "*"
  | "/" -> "%2F"
  | "-" -> "-"
  | _ -> ""

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

let private GiveRequest(a,b,oper) =
  async{
           let req = HttpWebRequest.Create("http://localhost:53881?a="+a+"&b="+b+"&oper="+oper+"", Method = "GET", ContentType = "text/plain")
           let rsp = req.GetResponse() :?> HttpWebResponse
           let! statusAns = StatusCode(rsp)
           return Some(statusAns)
  }

module Calculator=
  let public Calculate(a,b,oper)=
      async{
      let! result = GiveRequest(a,b,oper)
      return result
      }


let output (result : string option) =
  match result with
      | None -> Console.WriteLine("Bad Request")
      | Some result -> Console.WriteLine(result)

[<EntryPoint>]
let main argv =
    let a = Console.ReadLine()
    let op = GetOper(Console.ReadLine())
    let b = Console.ReadLine()
    let result = Async.RunSynchronously(Calculator.Calculate(a,b,op))
    output result.Value
    0
