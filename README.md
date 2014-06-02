FunScript.MVC
=====================

FunScript doesn't compile from HttpHandler while compiles the same .fsx script in a test. No idea why.

In the Tests.fs the test ``FunScript works`` works with F# code defined in the same file,
 but the test ``Could compile script`` doesn't work with the same F# code in HelloWorld.fsx.

If it compiles, then it is very easy to use .fsx directly in Razor views with this IHttpHandler,
which compiles a .fsx file using `FSharp.Compiler.Sevice` and then generates JavaScript out of it.


