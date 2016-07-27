﻿module ML.Regressions.SGD

open MathNet.Numerics.LinearAlgebra
open ML.Core.Utils
open ML.Core.LinearAlgebra
open GLM
open GD 

type SGDHyperParams = {
    Alpha: float
    BatchSize: int
}

let calcGradientBatch<'iter, 'hyper> (batchSize: int) (prms: CalcGradientParams<'hyper>) (iter: GradientDescentIter<'iter>) (grad: ClacGradientFunc<'iter, 'hyper>) =
    let x = prms.X
    let y = prms.Y

    let mutable iter = iter
    genRanges batchSize x.RowCount           
    |> Seq.map (fun (start, len) -> 
        (spliceRows start len x), (spliceVector start len y)
    )
    |> Seq.iter (fun (sx, sy) ->
        iter <- grad prms iter        
    )
    iter


let private calcGradient (prms: CalcGradientParams<SGDHyperParams>) (iter: GradientDescentIter<Unit>) =
    let theta = iter.Theta
    let gradients = prms.Gradient theta prms.X prms.Y
    { Theta  = theta - prms.HyperParams.Alpha * gradients; Params = ()}
    

let private calcGradient2 (prms: CalcGradientParams<SGDHyperParams>) (iter: GradientDescentIter<Unit>) =
    calcGradientBatch prms.HyperParams.BatchSize prms iter calcGradient
    

let private initIter (initialTheta: float Vector) = { Theta  = initialTheta; Params = () }
    
let SGD : GradientDescentFunc<SGDHyperParams> = 
    GD<Unit, SGDHyperParams> initIter calcGradient2

