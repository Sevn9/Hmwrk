//namespace FLibraryMaldebrot
module Library
open System.Windows.Forms
open System.Drawing
open System

type Complex (atmp, btmp) = class
  let mutable a = atmp
  let mutable b = btmp
  member x.aValue = a
  member x.bValue = b

  member public x.square() =
          let mutable tmp = (a * a) - (b * b)
          let mutable tmp2 = 2.0 * a*b
          b <- tmp2
          a <- tmp

  member public x.magnitude() =
          let mutable tmp = sqrt((a*a) + (b*b))
          tmp
  member public x.add(t:Complex) =
          let mutable t1 = a + t.aValue
          let mutable t2 = b + t.bValue
          a <- t1
          b <- t2
end

let Draw (xw:double) (yw: double) (width: int) (height:int) (zm:double)= 
 let image = new Bitmap(width, height)
 for x in 0..image.Width-1 do
   for y in 0..image.Height-1 do
    let mutable a = (((double)x + (xw / zm)) - (double)(image.Width / 2)) / ((double)image.Width / zm / (double)(1.777))
    let mutable b = (((double)y + (yw / zm)) - (double)(image.Height / 2)) /((double)image.Height / zm)

    let mutable c = new Complex(a, b)

    let mutable z = new Complex(0.0, 0.0)


    let mutable it = 0;
    let mutable exit = true 
    while (exit && it < 100) do
     it <-it + 1
     z.square()
     z.add(c)

     if (z.magnitude() > 2.0) then exit <- false
 
    image.SetPixel(x, y, Color.FromArgb(255, it % 8 * 16, it % 4 * 32, it % 2 * 64)) 
 image



