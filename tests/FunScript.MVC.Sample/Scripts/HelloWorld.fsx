#r "../bin/Funscript.dll"
#r "../bin/Funscript.Interop.dll"
#r "../bin/FunScript.TypeScript.Binding.lib.dll"
//#r "../bin/FunScript.TypeScript.Binding.jquery.dll"

open FunScript
open FunScript.TypeScript

[<ReflectedDefinition>]
type TestRecord =
    {First : string; Second : string}

[<ReflectedDefinition>]
let main() = 
    let a = { First = "a"; Second = "b"}
    ()
//
//[<ReflectedDefinition>]
//module Page =
//    
//    [<ReflectedDefinition>]
//    let hello () =
//      Globals.window.alert("Hello world!")
//
//    // Allows writing jq?name for element access
//    [<ReflectedDefinition>]
//    let jq(selector : string) = Globals.Dollar.Invoke selector
//    [<ReflectedDefinition>]
//    let (?) jq name = jq("#" + name)
//
//    [<ReflectedDefinition>]
//    let main() = 
//        jq?helloWorld.click(fun _ -> hello() :> obj)
