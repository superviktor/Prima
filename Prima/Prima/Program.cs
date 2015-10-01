using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Prima
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //---------------------------------------------------------
            double ro = 0;
            double alpha = 0;
            double lambda = 0;
            float h;
            List<double> Ro = new List<double>();
            List<double> Alpha = new List<double>();
            List<double> Lambda = new List<double>();
            List<float> H = new List<float>();
            //---------------------------------------------------------
            List<Edge> leftEdges = new List<Edge>();            

            String input = File.ReadAllText("inputFile.txt");
            int ii = 0;
            int jj = 0;
            int[,] resultArray = new int[6, 3];
            foreach (string row in input.Split('\n'))
            {
                jj = 0;
                foreach (string col in row.Trim().Split(','))
                {
                    resultArray[ii, jj] = Int32.Parse(col, CultureInfo.InvariantCulture);
                    jj++;
                }
                ii++;
            }

            for (int k = 0; k < resultArray.GetLength(0); k++)
            {
                for (int l = 0; l < resultArray.GetLength(1); l++)
                {
                    leftEdges.Add(new Edge(resultArray[k, l], resultArray[k, l + 1], resultArray[k, l + 2]));
                    break;
                }
            }

     

            //IMPORTANT
            int numberOfPoints = 6;
            List<int> treePoints= new List<int>();
            List<int> leftPoints= new List<int>();
            List<Edge> result = new List<Edge>();
            for (int i = 1; i < numberOfPoints; i++)
            {
                leftPoints.Add(i);
            }
            Random rnd = new Random();
            int pointToStart = rnd.Next(1, numberOfPoints);
            //int pointToStart = 5;
            treePoints.Add(pointToStart);
            leftPoints.Remove(pointToStart);

            while (leftPoints.Count>0)
            {               
                //find incedent edge
                List<Edge> incedentEdges = new List<Edge>();
                foreach (var point in treePoints)
                {
                    foreach (var edge in leftEdges)
                    {
                        if ((edge.V1 == point) || edge.V2 == point)
                        {
                            incedentEdges.Add(edge);
                        }
                    }
                }
                //find edege with min weigth
                Edge edgeWithMinWeight = incedentEdges[0];
                int minWeigth = edgeWithMinWeight.Weight;              
                foreach (var edge in incedentEdges)
                {
                    if (edge.Weight < minWeigth)
                    {
                        minWeigth = edge.Weight;
                        edgeWithMinWeight = edge;
                    }
                }

                //adding point and edge to the tree
                foreach (var point in treePoints)
                {
                    if (edgeWithMinWeight.V1 == point)
                    {
                        treePoints.Add(edgeWithMinWeight.V2);
                        leftPoints.Remove(edgeWithMinWeight.V2);
                        break;
                    }
                    else if (edgeWithMinWeight.V2 == point)
                    {
                        treePoints.Add(edgeWithMinWeight.V1);
                        leftPoints.Remove(edgeWithMinWeight.V1);
                        break;
                    }                  
                }
                result.Add(edgeWithMinWeight);
                leftEdges.Remove(edgeWithMinWeight);
            }

            foreach (var e in result)
            {
                Console.WriteLine(e.V1+" - "+e.V2);
            }
            Console.WriteLine(new string('*',20));
          



            // Krab is starting here
//#region KRAB{ 
//            List<Edge> krabSet = new List<Edge>(result);
//            List<Edge> taxon1 = new List<Edge>();
//            List<Edge> taxon2 = new List<Edge>();

//            //GET TAXONS
//                foreach (var cutted in krabSet)
//                {
//                    Console.WriteLine(cutted.V1 + " " + cutted.V2);
//                    List<Edge> krabSet0 = new List<Edge>(krabSet);
//                    krabSet0.Remove(cutted);

//                    foreach (var ee in krabSet0)
//                    {
//                        List<Edge> krabSet1 = new List<Edge>(krabSet0);

//                        if (cutted.V1 == ee.V1 || cutted.V1 == ee.V2)
//                        {
//                            if (!taxon1.Contains(ee))
//                            {
//                                taxon1.Add(ee);
//                            }
//                            krabSet1.Remove(ee);
//                            List<Edge> krabSet2 = new List<Edge>(krabSet1);
//                            foreach (var e in krabSet2)
//                            {
//                                if ((ee.V1 == e.V1) || (ee.V2 == e.V2) || (ee.V1 == e.V2) || (ee.V2 == e.V1))
//                                {
//                                    if (!taxon1.Contains(e))
//                                    {
//                                        taxon1.Add(e);
//                                    }
//                                }
//                            }
//                        }
//                        if (cutted.V2 == ee.V1 || cutted.V2 == ee.V2)
//                        {
//                            if (!taxon2.Contains(ee))
//                            {
//                                taxon2.Add(ee);
//                            }
//                            krabSet1.Remove(ee);
//                            List<Edge> krabSet2 = new List<Edge>(krabSet1);
//                            foreach (var e in krabSet2)
//                            {
//                                if ((ee.V1 == e.V1) || (ee.V2 == e.V2) || (ee.V1 == e.V2) || (ee.V2 == e.V1))
//                                {
//                                    if (!taxon2.Contains(e))
//                                    {
//                                        taxon2.Add(e);
//                                    }
//                                }
//                            }
//                        }
//                    }

//                    //GET BEST RESULT 
//                    List<List<Edge>> taxons = new List<List<Edge>>();
//                    taxons.Add(taxon1);
//                    taxons.Add(taxon2);
//                    double preRo = 0;
//                    double minWeight = 0;
//                    foreach (var taxon in taxons)
//                    {

//                        if (taxon.Count > 0)
//                        {
//                            minWeight = taxon[0].Weight;
//                            foreach (var e in taxon)
//                            {

//                                if (e.Weight < minWeight)
//                                {
//                                    minWeight = e.Weight;
//                                }
//                                preRo += e.Weight;
//                            }

//                            //average weight of edge in taxon 
//                            ro = preRo/taxon.Count;
//                            Ro.Add(ro);
//                            //weight of edge between taxons
//                            alpha = cutted.Weight;
//                            Alpha.Add(alpha);
//                            //min weight div cutted 
//                            foreach (var e in taxon)
//                            {
//                                lambda += minWeight/e.Weight;

//                            }
//                            Lambda.Add(lambda);
//                            //
//                            h = (float) taxon.Count/result.Count;
//                            H.Add(h);
//                        }
//                    }


//                    //CALCULATIONS
//                    double temp = 0;
//                    foreach (var r in Ro)
//                    {
//                        temp += r;
//                    }
//                    //var RO = temp;
//                    var RO = temp/result.Count;
//                    double temp1 = 0;
//                    foreach (var a in Alpha)
//                    {
//                        temp1 += a;
//                    }
//                    var A = temp1/(result.Count - 1);

//                    double temp2 = 0;
//                    foreach (var l in Lambda)
//                    {
//                        temp2 += l;
//                    }
//                    var L = temp2/(result.Count - 1);
//                    double HH = 0;
//                    if (taxon1.Count == 0)
//                    {
//                        HH = (H[0]/taxon2.Count);
//                    }
//                    else if (taxon2.Count == 0)
//                    {
//                        HH = (H[0]/taxon1.Count);
//                    }
//                    else
//                    {
//                        HH = (H[0]/taxon1.Count)*(H[1]/taxon2.Count);
//                    }
//                    //HH = 0.6;
//                    var F = Math.Log(((RO*A)/(L*HH)), Math.E);
//                    Console.WriteLine(F);
//                }
//            }  
//#endregion        
            }                        
        }
    }
