#r "../FunScript.MVC.Sample/bin/Funscript.dll"
#r "../FunScript.MVC.Sample/bin/Funscript.Interop.dll"
#r "../FunScript.MVC.Sample/bin/FunScript.TypeScript.Binding.lib.dll"

[<ReflectedDefinition>]
module Page =
    open FunScript
    open FunScript.TypeScript

    let hello () =
      Globals.window.alert("Hello world!")


