using System;
using System.Linq;

namespace ALS
{
    public static class MatrixOps
    {
        public static double[,] IdentityMatrix(int size)
        {
            double[,] matrix = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                }
            }
            return matrix;
        }

        public static double[,] Transpose(this double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        public static void Fill(this double[,] matrix, Random rnd)

        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rnd.NextDouble();
                }
            }
        }
        public static double[,] Add(this double[,] matrix1, double[,] matrix2)
        {

            var m = matrix1.GetLength(0);
            var n = matrix1.GetLength(1);
            var y = matrix2.GetLength(0);
            var z = matrix2.GetLength(1);

            double[,] matrix = new double[m, n];
            if (!(m == y && n == z))
            {
                throw new Exception("Matrices cannot be added");
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return matrix;

        }

        public static double[,] Multiply(this double[,] matrix1, double[,] matrix2)
        {
            int n = matrix1.GetLength(0);
            int m = matrix1.GetLength(1);
            int p = matrix2.GetLength(1);
            if (m != matrix2.GetLength(0))
            {
                throw new Exception("Matrices cannot be multiplied");
            }
            double[,] result = new double[n, p];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
        public static double MultiplyVectors(this double[,] matrix1, double[,] matrix2)
        {
            int n = matrix1.GetLength(0);
            int m = matrix1.GetLength(1);
            int p = matrix2.GetLength(1);
            if (m != matrix2.GetLength(0))
            {
                throw new Exception("Matrices cannot be multiplied");
            }
            double[,] result = new double[n, p];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            double number = result[0, 0];
                return number;
        }
        public static double[] Multiply(this double[] vector, double number)
        {
            for (int i = 0; i < vector.GetLength(0); i++)
            {
                vector[i] *= number;
            }
            return vector;
        }
        public static double[,] Multiply(this double[,] matrix, double number)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            double[,] result = new double[n, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = matrix[i, j] * number;
                }
            }
            return result;

        }
        public static void PrintDouble(this double[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write($"{matrix[i, j].ToString("0.00")} ");
                }
                Console.Write("\n");
            }
        }
        public static void Print(this double[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.Write("\n");
            }
        }
        public static double Norm(this double[] vector)
        {
            double sum = 0.0;
            foreach(double v in vector)
            {
                sum += v*v;
            }
            return Math.Sqrt(sum);
        }
        public static void PrintDouble(this double[] vector)
        {
            foreach (var v in vector)
            {
                Console.Write(v+ " ");
            }
            Console.Write("\n");
        }

        public static double[] GetColumn(this double[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }
        public static double[] GetRow(this double[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        public static double[,] GetColumnAsMatrix(this double[,] matrix, int columnNumber)
        {
            var vector=Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();

            double[,] mat = new double[vector.Length, 1];
            
            for(int i=0;i<vector.Length;i++)
            {
                mat[i, 0] = vector[i];
            }
            return mat;
        }

      
        public static void ReplaceRow(this double[,] matrix, double[] row, int rowNumber)
        {
            for (int i = 0; i < row.Length; i++)
            {
                matrix[rowNumber, i] = row[i];
            }

        }
        public static void ReplaceColumn(this double[,] matrix, double[] column, int columnNumber)
        {
            for (int i = 0; i < column.Length; i++)
            {
                matrix[i, columnNumber] = column[i];
            }
        }
        public static  int CountValues(this double[] row)
        {
            int count = 0;
            foreach (var r in row)
            {
                if (r != 0)
                    count++;
            }
            return count;
        }
        public static double[,] TrimArray(this double[,] originalArray,int rowToRemove, int columnToRemove)
        {
           double[,] result = new double[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }
        
        public static double[] Solve(this double[,] A, double[] b)
        {
            int n = b.Length;


            for (int p = 0; p < n; p++)
            {


                int max = p;
                for (int i = p + 1; i < n; i++)
                {
                    if (Math.Abs(A[i, p]) > Math.Abs(A[max, p]))
                    {
                        max = i;
                    }
                }
                double[] temp = GetRow(A, p);
                double[] maxx = GetRow(A, max); ;
                ReplaceRow(A, maxx, p);
                ReplaceRow(A, temp, max);

                double t = b[p];
                b[p] = b[max];
                b[max] = t;



                if (A[p, p] == 0)
                {
                    throw new ArithmeticException("");
                }


                for (int i = p + 1; i < n; i++)
                {

                    double alpha = A[i, p] / A[p, p];
                    b[i] -= alpha * b[p];
                    for (int j = p; j < n; j++)
                    {

                        A[i, j] -= alpha * A[p, j];
                    }
                }


            }

            double[] x = new double[n];

            for (int i = n - 1; i >= 0; i--)
            {

                double sum = 0.0;




                for (int j = i + 1; j < n; j++)
                {
                    sum += A[i, j] * x[j];
                }
                x[i] = (b[i] - sum) / A[i, i];

            }

            return x;
        }

    }



}
