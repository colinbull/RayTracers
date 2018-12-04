namespace FSharpBenchMarks

open BenchmarkDotNet.Attributes 
open BenchmarkDotNet.Running 

[<MemoryDiagnoser>]
type RayTracerBenchmarks() = 
     
    let paras = 1234, 100, 50, 50 

    [<Benchmark>]
    member x.Classes() = Classes.Executor.run(paras)
    
    [<Benchmark>]
    member x.Records() = Recds.Executor.run(paras)

    [<Benchmark>]
    member x.StructRecords() = StructRcds.Executor.run(paras)

    [<Benchmark>]
    member x.StructRecords32() = StructRecds32.Executor.run(paras)

    [<Benchmark>]
    member x.StructRecordsNoMethods() = StructRcdsNoMethods.Executor.run(paras)

module Program = 

    [<EntryPoint>]
    let main(args) = 
        let _ = BenchmarkRunner.Run<RayTracerBenchmarks>()
        0