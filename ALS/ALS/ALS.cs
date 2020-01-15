using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS
{
    public static class ALS
    {
            public static double Optimisation(double[,] ratings,double[,] CurrentMatrix,double reg,double[,] U, double[,] P)
            {
            double Sum_r_u_p = 0;
            double Sum_U_norm = 0;
            double Sum_P_norm = 0;
            int n = ratings.GetLength(0);
            int m = ratings.GetLength(1);

            for (int u = 0; u < n; u++)
            {
                var Uu = U.GetColumn(u).Norm();
                Sum_U_norm += Uu*Uu;
                for (int p = 0; p < m; p++)
                {
                    var Pp = P.GetColumn(p).Norm();
                    Sum_P_norm += Pp*Pp;
                    if (ratings[u, p] != 0.0)
                    {
                        var diff = ratings[u, p] - CurrentMatrix[u, p];
                        Sum_r_u_p += diff * diff;
                    }                   
                }               
            }
            return Sum_r_u_p+ reg*(Sum_U_norm+Sum_P_norm);
            }
            public static double[,] Solve(double[,] ratings,int d,double reg,int iterations)
            {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Random rnd = new Random();


            int n = ratings.GetLength(0);
            int m = ratings.GetLength(1);
             double[,] U = new double[d, n];
             double[,] P = new double[d, m];

            
            P.Fill(rnd);
            U.Fill(rnd);
            for (int iter = 0; iter < iterations; iter++)
            {
                var currentMatrix = U.Transpose().Multiply(P);
                
                for (int u = 0; u < n; u++)
                {
                    

                    List<int> indexes = new List<int>();

                    for (int i = 0; i < ratings.GetLength(1); i++)
                    {
                        if (ratings[u, i] != 0.0)
                        {
                            indexes.Add(i);
                        }
                    }
                    double[,] P_I_U = new double[P.GetLength(0), indexes.Count];


                    int j = 0;
                    foreach (var ind in indexes)
                    {
                        var column = P.GetColumn(ind);
                        P_I_U.ReplaceColumn(column, j);
                        j++;
                    }


                    var P_I_U_T = P_I_U.Transpose();

                    var E = MatrixOps.IdentityMatrix(d);


                    var A_U = P_I_U.Multiply(P_I_U_T).Add(E.Multiply(reg));

                    j = 0;

                    var V_U = new double[d];


                    for (j = 0; j < V_U.Length; j++)
                    {
                        foreach (var i in indexes)
                        {
                            V_U[j] += ratings[u, i] * P.GetColumn(i)[j];
                        }
                    }


                    var solution = A_U.Solve(V_U);


                    U.ReplaceColumn(solution, u);

                }

                for (int p = 0; p < m; p++)
                {
                    List<int> indexes = new List<int>();

                    for (int i = 0; i < ratings.GetLength(0); i++)
                    {
                        if (ratings[i, p] != 0.0)
                        {
                            indexes.Add(i);
                        }
                    }


                    double[,] U_I_p = new double[U.GetLength(0), indexes.Count];

                   

                    int j = 0;
                    foreach (var col in indexes)
                    {
                        var column = U.GetColumn(col);
                        U_I_p.ReplaceColumn(column, j);
                        j++;
                    }

                    var U_I_p_T = U_I_p.Transpose();

                    var E = MatrixOps.IdentityMatrix(d);

                    var B_U = U_I_p.Multiply(U_I_p_T).Add(E.Multiply(reg));

                    j = 0;

                    var V_p = new double[d];


                    for (j = 0; j < V_p.Length; j++)
                    {
                        foreach (var i in indexes)
                        {
                            V_p[j] += ratings[i, p] * U.GetColumn(i)[j];
                        }
                    }


                    var solution = B_U.Solve(V_p);

                    P.ReplaceColumn(solution, p);
                }
                stopWatch.Stop();
                
           
            }
            var result = U.Transpose().Multiply(P);



            return result;
            
        }
    }
}
