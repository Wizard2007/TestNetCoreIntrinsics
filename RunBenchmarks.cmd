dotnet clean .\XorShift.Intrinsics
dotnet clean .\XorShift.BenchmarckDotNet
dotnet build .\XorShift.Intrinsics
dotnet build .\XorShift.BenchmarckDotNet
dotnet run --project .\XorShift.BenchmarckDotNet\XorShift.BenchmarckDotNet.csproj -c Release