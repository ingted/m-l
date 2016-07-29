﻿module ML.Regressions.LogisticRegression

open MathNet.Numerics.LinearAlgebra
open ML.Core.Utils
open ML.Core.LinearAlgebra

open GLM
open Theta

let sigmoid (z: Theta) = 
    1. / (1. + (-1. * z).exp())

let sigmoidScalar (z: float) = 
    1. / (1. + System.Math.Exp(-1. * z))

let logisticMSECost  (x : float Matrix) (y : float Vector) (theta: Theta) =
    let s = x * theta |> sigmoid
    let p = y * s.log()
    let n = (1. - y) * (1. - s).log()
    - 1. * ( p + n ) / float x.RowCount
        
let logisticMSEGradient (x : float Matrix) (y : float Vector) (theta: Theta) =
    x.Transpose() * (sigmoid(x * theta) - y) / float x.RowCount 

let logisticHyp (x: float Vector) (theta: Theta) = 
     theta * x |> sigmoidScalar |> System.Math.Round

(*
let predict : float Vector -> float Vector -> float = GLMPredict logisticHyp

let predictNorm : NormParams -> float Vector -> float Vector -> float = GLMPredictNorm logisticHyp
*)
