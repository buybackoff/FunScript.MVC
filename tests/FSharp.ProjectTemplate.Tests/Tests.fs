module FunScript.MVC.Tests

open FunScript.MVC
open NUnit.Framework
open System
open System.IO
open System.Reflection
open Microsoft.FSharp.Quotations

open Microsoft.FSharp.Compiler.SimpleSourceCodeServices

open System.Text.RegularExpressions


let getReferences flag source =
        let pattern = "^\s*"+flag+"\s+\"((.+))\"\s*$"
        let regex = Regex(pattern, RegexOptions.Multiline)
        let res = 
            regex.Matches source
            |> Seq.cast<Match>
            |> Seq.map (fun m -> m.Groups.[1].Value)
            |> Seq.toArray
        res

[<Test>]
let ``Could extract references from script`` () =
    let file = "../../HelloWorld.fsx"
    let input = File.ReadAllText(file)

    let res = getReferences "#r" input
    Assert.IsTrue((Array.length res) > 0)
    ()

[<Test>]
let ``Could compile script`` () =
    let file = "../../HelloWorld.fsx"
    let input = File.ReadAllText(file)

    let directory = Path.GetDirectoryName(Path.GetFullPath(file))
    

    let scs = SimpleSourceCodeServices()

    let references = getReferences "#r" input

    let flags = 
        seq { 
            yield! [| "-o"; file + ".dll"; "-a"; file |]
            for r in references do
                yield "-r"
                yield Path.Combine(directory, r)
        }
        |> Seq.toArray

    let errors, exitCode, dynAssembly = 
        scs.CompileToDynamicAssembly(flags, None)

    // TODO we could get dependencies from fsx
        
    Assert.IsTrue((Array.length errors) > 0)
    Assert.IsTrue(dynAssembly.IsSome)
    ()





