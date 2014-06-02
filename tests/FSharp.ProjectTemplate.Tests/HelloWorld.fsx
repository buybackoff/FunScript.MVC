#r "../FunScript.MVC.Sample/bin/Funscript.dll"
#r "../FunScript.MVC.Sample/bin/Funscript.Interop.dll"
#r "../FunScript.MVC.Sample/bin/FunScript.TypeScript.Binding.lib.dll"
//#r "../FunScript.MVC.Sample/bin/FunScript.TypeScript.Binding.jquery.dll"


open FunScript
open FunScript.TypeScript

[<ReflectedDefinition>]
type TestRecord =
    {First : string; Second : string}

[<ReflectedDefinition>]
let main() = 
    let a = { First = "a"; Second = "b"}
    ()


//[<ReflectedDefinition>]
//module Page =
//    
//    let hello () =
//      Globals.window.alert("Hello world!")
//
//    // Allows writing jq?name for element access
//    let jq(selector : string) = Globals.Dollar.Invoke selector
//    let (?) jq name = jq("#" + name)
//
//    [<ReflectedDefinition>]
//    let main() = 1 + 1
//        //jq?helloWorld.click(fun _ -> hello() :> obj)
