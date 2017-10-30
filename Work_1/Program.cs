using System;
using MatrixLibrary;

namespace Work_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix1 = new Matrix(2,3);
            matrix1[0, 0] = 1;
            matrix1[0, 1] = 2;
            matrix1[0, 2] = 3;
            matrix1[1, 0] = 3;
            matrix1[1, 1] = 2;
            matrix1[1, 2] = 1;

            Matrix matrix2 = new Matrix(2, 3);
            matrix2[0, 0] = 1;
            matrix2[0, 1] = 2;
            matrix2[0, 2] = 3;
            matrix2[1, 0] = 4;
            matrix2[1, 1] = 5;
            matrix2[1, 2] = 6;
            //matrix2[2, 0] = 7;
            //matrix2[2, 1] = 8;
            //matrix2[2, 2] = 9;

            Matrix matrix3 = matrix2.Clone();

            Console.WriteLine(matrix3.GetHashCode() == matrix2.GetHashCode());
            Console.WriteLine();

            double[,] masiv = matrix3.ToArray();
            double[,] masiv2 = Matrix.ToArray(matrix3);

            masiv[0,1] = 0;

            Matrix matrix4 = matrix1+matrix2;
            for (int i = 0; i < matrix4.GetLength(0); i++)
            {
                for (int j = 0; j < matrix4.GetLength(1); j++)
                {
                    Console.Write("{0} ",matrix4[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            matrix1.Serialize("D:/mat1.dat");

            Matrix matrix5 = Matrix.Deserialize("D:/mat1.dat");

            for (int i = 0; i < matrix5.GetLength(0); i++)
            {
                for (int j = 0; j < matrix5.GetLength(1); j++)
                {
                    Console.Write("{0} ", matrix5[i, j]);
                }
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
