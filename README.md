FunScript.MVC
=====================

FunScript doesn't compile from HttpHandler while compiles the same .fsx script in a test. No idea why.

If it compiles, then it is very easy to use .fsx directly in Razor views with this IHttpHandler,
which compiles a .fsx file using `FSharp.Compiler.Sevice` and then generates JavaScript out of it.
