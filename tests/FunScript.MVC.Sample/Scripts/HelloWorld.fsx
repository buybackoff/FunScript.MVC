#r "../bin/Funscript.dll"
#r "../bin/Funscript.Interop.dll"
#r "../bin/FunScript.TypeScript.Binding.lib.dll"



[<ReflectedDefinition>]
module Page =
    open FunScript
    open FunScript.TypeScript

    let hello () =
      Globals.window.alert("Hello world!")

