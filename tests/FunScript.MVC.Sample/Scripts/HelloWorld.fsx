#r "../bin/Funscript.dll"
#r "../bin/Funscript.Interop.dll"
#r "../bin/FunScript.TypeScript.Binding.lib.dll"
#r "../bin/FunScript.TypeScript.Binding.jquery.dll"



[<ReflectedDefinition>]
module Page =
    open FunScript
    open FunScript.TypeScript

    let hello () =
      Globals.window.alert("Hello world!")

    // Allows writing jq?name for element access
    let jq(selector : string) = Globals.Dollar.Invoke selector
    let (?) jq name = jq("#" + name)

    let main() = 1+1
//        jq?helloWorld.click(fun _ -> hello() :> obj)
//        ()