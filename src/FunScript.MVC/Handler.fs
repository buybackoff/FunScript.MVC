namespace FunScript.MVC

open System.Web
open FunScript
open Microsoft.FSharp.Compiler.SimpleSourceCodeServices
open System.IO
open System.Reflection
open Microsoft.FSharp.Quotations
open System.Text.RegularExpressions


module Helper =
    let getReferences flag source =
        let pattern = "^\s*"+flag+"\s+\"((.+))\"\s*$"
        let regex = Regex(pattern, RegexOptions.Multiline)
        let res = 
            regex.Matches source
            |> Seq.cast<Match>
            |> Seq.map (fun m -> m.Groups.[1].Value)
            |> Seq.toArray
        res

type public Handler() =
    member private this.DoProcess(ctx : HttpContext) =
        let req, resp = ctx.Request, ctx.Response
        let relativePath = req.Url.LocalPath
        let fullPath = ctx.Server.MapPath(relativePath)
        let directory = Path.GetDirectoryName(fullPath)
        //resp.Write(fullPath)
        let inputSource = File.ReadAllText(fullPath)
        let references = Helper.getReferences "#r" inputSource
        let flags = 
            seq { 
                yield! [| "-o"; relativePath + ".dll"; "-a"; fullPath |]
                for r in references do
                    yield "-r"
                    let fullRef = Path.Combine(directory, r) 
                    yield fullRef
                    //resp.Write(fullRef)
            }
            |> Seq.toArray

        let scs = SimpleSourceCodeServices()
        let errors, exitCode, dynAssembly = scs.CompileToDynamicAssembly(flags, None)
        //resp.Write("Compiled")

        // TODO error handling
                
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

        resp.AddFileDependency(ctx.Server.MapPath(relativePath))
        resp.Cache.SetCacheability(HttpCacheability.Server)
        resp.Cache.SetLastModifiedFromFileDependencies()
        resp.Cache.SetETagFromFileDependencies()
        resp.ContentType <- "text/javascript"
        resp.Write(source)
    
    interface IHttpHandler with
        member this.ProcessRequest(context : HttpContext) = this.DoProcess(context)
        member this.IsReusable with get() = false
