using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prima;

namespace PrimaVisual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Edge> leftEdges = new List<Edge>();
        private int numberOfV = 5;
        List<VisualPoint> vps1 = new List<VisualPoint>();
        List<VisualPoint> vps2 = new List<VisualPoint>();
        private void DefaultGraph_Click(object sender, RoutedEventArgs e)
        {            
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


            Random rnd = new Random();
          
            for (int i = 0; i < numberOfV; i++)
            {
                vps1.Add(new VisualPoint(rnd.Next(1,10)*30,rnd.Next(1,10)*30));
                vps2.Add(vps1[i]);
            }
            for (int i = 0; i < numberOfV; i++)
            {
                foreach (var le in leftEdges)
                {

                    if (le.V1 == i || le.V2 == i)
                    {
                        vps1[i].Edges.Add(le);
                    }

                }
            }
            foreach (var vp in vps1)
            {
                foreach (var vp1 in vps1)
                {
                    foreach (var e0 in vp.Edges)
                    {
                        foreach (var e1 in vp1.Edges)
                        {
                                                   
                            if (e0 == e1)
                            {
                                Line l = new Line();
                                l.X1 = vp.X;
                                l.Y1 = vp.Y;
                                l.X2 = vp1.X;
                                l.Y2 = vp1.Y;
                                l.Stroke = Brushes.Red;
                                DefaulCanvas.Children.Add(l);
                            }

                        }
                    }
                }
            }
            foreach (var vp in vps1)
            {
                TextBlock tb = new TextBlock();
                tb.Text = vps1.IndexOf(vp).ToString();
                tb.FontSize = 14;               
                Ellipse elipse = new Ellipse();
                elipse.Fill = new SolidColorBrush(Colors.Pink);
                elipse.StrokeThickness = 1;
                elipse.Stroke = Brushes.Black;
                elipse.Width = 15;
                elipse.Height = 15;
                Canvas.SetTop(elipse, vp.Y-7);
                Canvas.SetLeft(elipse, vp.X-7);
                Canvas.SetTop(tb, vp.Y-9 );
                Canvas.SetLeft(tb, vp.X -4);
                DefaulCanvas.Children.Add(elipse);
                DefaulCanvas.Children.Add(tb);
            }

          
                                             
        }

        private void PrimaGraph_Click(object sender, RoutedEventArgs e)
        {
            //IMPORTANT
            int numberOfPoints = 5;
            List<int> treePoints = new List<int>();
            List<int> leftPoints = new List<int>();
            List<Edge> result = new List<Edge>();
            for (int i = 0; i < numberOfPoints; i++)
            {
                leftPoints.Add(i);
            }
            Random rnd = new Random();
            int pointToStart = rnd.Next(0, numberOfPoints);
            //int pointToStart = 5;
            treePoints.Add(pointToStart);
            leftPoints.Remove(pointToStart);

            while (leftPoints.Count > 0)
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

          

          //VISUAL
            foreach (var vp in vps2)
            {
                vp.Edges = new List<Edge>();
            }
            for (int i = 0; i < numberOfPoints; i++)
            {
                foreach (var le in result)
                {

                    if (le.V1 == i || le.V2 == i)
                    {
                        vps2[i].Edges.Add(le);
                    }

                }
            }


            foreach (var vp in vps2)
            {
                foreach (var vp1 in vps2)
                {
                    foreach (var e0 in vp.Edges)
                    {
                        foreach (var e1 in vp1.Edges)
                        {

                            if (e0 == e1)
                            {
                                Line l = new Line();
                                l.X1 = vp.X;
                                l.Y1 = vp.Y;
                                l.X2 = vp1.X;
                                l.Y2 = vp1.Y;
                                l.Stroke = Brushes.Red;
                                PrimaCanvas.Children.Add(l);
                            }

                        }
                    }
                }
            }
            foreach (var vp in vps2)
            {
                TextBlock tb = new TextBlock();
                tb.Text = vps2.IndexOf(vp).ToString();
                tb.FontSize = 14;
                Ellipse elipse = new Ellipse();
                elipse.Fill = new SolidColorBrush(Colors.Pink);
                elipse.StrokeThickness = 1;
                elipse.Stroke = Brushes.Black;
                elipse.Width = 15;
                elipse.Height = 15;
                Canvas.SetTop(elipse, vp.Y - 7);
                Canvas.SetLeft(elipse, vp.X - 7);
                Canvas.SetTop(tb, vp.Y - 9);
                Canvas.SetLeft(tb, vp.X - 4);
                PrimaCanvas.Children.Add(elipse);
                PrimaCanvas.Children.Add(tb);
            }
        }
    }
}
