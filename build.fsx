#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing

let buildDir = "./build/"
let testDir  = "./test/"

let srcProjects  = "**/*.csproj"

let testDlls = buildDir + "*.Tests.dll"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "BuildAll" (fun _ ->
    !! srcProjects
        |> MSBuildRelease buildDir "Build"
        |> Log "AppBuild-Output: "
)

"Clean"
    ==> "BuildAll"

RunTargetOrDefault "BuildAll"
