// Learn more about F# at http://fsharp.org

open System

type MaybeBuilder() =
  member this.Bind(x,f)=
      match x with
      |None -> None
      |Some a -> f a
  member this.Return(x)=
      Some x

//создаем экземпляр типа
let maybe = new MaybeBuilder()

let Sum (x:int) (y:int) = x + y

let Subtraction (x:int) (y:int) = x - y

let Multiplication (x:int) (y:int) = x * y

let Division x y = 
  match y with
  | 0 -> None
  | _ -> Some (x / y)


let print (result : int option)  =
  match result with
      | None -> Console.WriteLine("Error")
      | Some result -> Console.WriteLine(result)


let Calculation op (x:int) (y:int) =
  maybe{
  let! result =
      match op with 
      |"+" -> Some(Sum x y)
      |"-" -> Some(Subtraction x y)
      |"*" -> Some(Multiplication x y)
      |"/" -> Division x y
      |_ -> None
  return result
  }
  
  //----------------------------------
[<EntryPoint>]
let main argv =
    let x  = Console.ReadLine() |> Int32.Parse
    let op  = Console.ReadLine() 
    let y  = Console.ReadLine() |> Int32.Parse
    let answer = Calculation op x y

    print answer
    
    0 // return an integer exit code
