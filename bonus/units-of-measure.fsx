
[<Measure>]
type mi
[<Measure>]
type gal
[<Measure>]
type mpg = mi/gal //derived units

[<Measure>]
type km
[<Measure>]
type L
[<Measure>]
type Lkm = L/km

//we need the type annotations for the sake enforcing the units
//its important to be consistent in the which units you use (remember, mpg is equivalent mi/gal)
let toLitersPer100Km (mpg:float<mpg>) = (235.0<(Lkm)/(1/mpg)>) / mpg
let toLiters (lKm:float<L/km>) (km:float<km>) =
    (lKm / 100.0) * km
let toKm (mi:float<mi>) = 
    mi * 1.609344<km/mi>

let miles = 400.0<mi>
let gallons = 8.0<gal>
let mpg = miles / gallons
let lPer100Km = mpg |> toLitersPer100Km
let kilometers = miles |> toKm
let liters = kilometers |> toLiters lPer100Km



