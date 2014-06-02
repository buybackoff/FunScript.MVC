//[<ReflectedDefinition>]
module FunScript.MVC.Tests

open FunScript.MVC
open NUnit.Framework
open System
open System.IO
open System.Reflection
open Microsoft.FSharp.Quotations

open Microsoft.FSharp.Compiler.SimpleSourceCodeServices

open System.Text.RegularExpressions


open FunScript
open FunScript.TypeScript

[<ReflectedDefinition>]
type TestRecord =
    { First : string; Second : string}

[<ReflectedDefinition>]
let main() = 
    let a = { First = "a"; Second = "b"}
    ()

[<Test>]
let ``FunScript works`` () =
    let main =
        let types = Assembly.GetExecutingAssembly().GetTypes()
        let flags = BindingFlags.NonPublic ||| BindingFlags.Public ||| BindingFlags.Static
        let mains = 
            [ for typ in types do
                for mi in typ.GetMethods(flags) do
                    if mi.Name = "main" then yield mi ]
        let main = 
            match mains with
            | [it] -> it
            | _ -> failwith "Main function not found!"
        Expr.Call(main, [])

    let source = FunScript.Compiler.Compiler.Compile(main)
    Console.WriteLine(source)
    ()

[<Test>]
let ``Could extract references from script`` () =
    let file = "../../HelloWorld.fsx"
    let input = File.ReadAllText(file)

    let res = Helper.getReferences "#r" input
    Assert.IsTrue((Array.length res) = 3)
    ()


[<Test>]
let ``Could compile script`` () =
    let file = "../../HelloWorld.fsx" // relative to bin/[Debug/Release]
    let input = File.ReadAllText(file)

    let directory = Path.GetDirectoryName(Path.GetFullPath(file))

    let scs = SimpleSourceCodeServices()

    let references = Helper.getReferences "#r" input

    let flags = 
        seq { 
            yield! [| "-o"; file + ".dll"; "-a"; file |]
            for r in references do
                yield "-r"
                yield Path.Combine(directory, r)
        }
        |> Seq.toArray

    let errors, exitCode, dynAssembly = scs.CompileToDynamicAssembly(flags, None)

    // TODO we could get dependencies from fsx
    let main =
        let types = dynAssembly.Value.GetTypes()
        let flags = BindingFlags.NonPublic ||| BindingFlags.Public ||| BindingFlags.Static
        let mains = 
            [ for typ in types do
                for mi in typ.GetMethods(flags) do
                    if mi.Name = "main" then yield mi ]
        let main = 
            match mains with
            | [it] -> it
            | _ -> failwith "Main function not found!"
        Expr.Call(main, [])

    let source = FunScript.Compiler.Compiler.Compile(main)

    Console.WriteLine(source)

    //Assert.IsTrue((Array.length errors) > 0)
    //Assert.IsTrue(dynAssembly.IsSome)
    ()





