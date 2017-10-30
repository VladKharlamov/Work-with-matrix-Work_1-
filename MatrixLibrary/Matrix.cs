using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MatrixLibrary
{
    [Serializable]
    public class Matrix
    {
        private double[,] array;

        public double this[int index1, int index2]
        {
            get
            {
                if ((index1 < 0) || (index2 < 0))
                    throw new ArgumentException("The index is less than zero");
                if ((index1 >= array.GetLength(0)) || (index2 >= array.GetLength(1)))
                    throw new ArgumentOutOfRangeException("Index outside the matrix");

                return array[index1, index2];
            }
            set
            {
                    if ((index1 < 0) || (index2 < 0))
                        throw new ArgumentOutOfRangeException("The index is less than zero");
                    if ((index1 >= array.GetLength(0)) || (index2 >= array.GetLength(1)))
                        throw new ArgumentOutOfRangeException("Index outside the matrix");
                    if ((double.MaxValue < value) || (double.MinValue > value))
                        throw new ArgumentException("The index is less/more than double format");

                    array[index1, index2] = value;
            }
        }

        public Matrix(double[,] matrix)
        {
            if ((object)matrix == null)
                throw new ArgumentNullException();
            this.array = matrix;
        }

        public Matrix(int size1, int size2)
        {
            if ((size1 < 0) || (size2 < 0))
                throw new ArgumentException("The index is less than zero");

            this.array = new double[size1, size2];
        }

        public int GetLength(int dimension)
        {
            if ((dimension < 0) || (dimension > 1))
                throw new ArgumentException("Dimension has a value other than 0 or 1");
            return array.GetLength(dimension);
        }

        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            if ((object)mat1 == null || (object)mat2 == null)
                throw new ArgumentNullException();
            if ((mat1.GetLength(0) != mat2.GetLength(0)) || (mat1.GetLength(1) != mat2.GetLength(1)))
                throw new MatrixConsistentException();
            Matrix temp = mat1.Clone();
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = mat1[i, j] + mat2[i, j];
                }
            }
            return temp;
        }
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            if ((object)mat1 == null || (object)mat2 == null)
                throw new ArgumentNullException();
            if ((mat1.GetLength(0) != mat2.GetLength(0)) || (mat1.GetLength(1) != mat2.GetLength(1)))
                throw new MatrixConsistentException();
            Matrix temp = mat1.Clone();
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = mat1[i, j] - mat2[i, j];
                }
            }
            return temp;
        }
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            if ((object)mat1 == null || (object)mat2 == null)
                throw new ArgumentNullException();
            if (mat1.GetLength(1) != mat2.GetLength(0))
                throw new MatrixConsistentException("The number of columns in the first matrix must equal the number of rows in the second matrix");
            Matrix temp = new Matrix(mat1.GetLength(0), mat2.GetLength(1));

            for (int i = 0; i < mat1.GetLength(0); i++)
            {
                for (int j = 0; j < mat2.GetLength(1); j++)
                {
                    for (int r = 0; r < mat1.GetLength(1); r++)
                    {
                        temp[i, j] += mat1[i, r] * mat2[r, j];
                    }
                }
            }
            return temp;
        }

        public static bool operator ==(Matrix mat1, Matrix mat2)
        {
            if ((object)mat1 == null || (object)mat2 == null)
                throw new ArgumentNullException();
            if (mat1.GetLength(0) == mat2.GetLength(0))
            {
                if (mat1.GetLength(1) == mat2.GetLength(1))
                {
                    for (int i = 0; i < mat1.GetLength(0); i++)
                    {
                        for (int j = 0; j < mat1.GetLength(1); j++)
                        {
                            if (mat1[i,j]!=mat2[i,j])
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        public static bool operator !=(Matrix mat1, Matrix mat2)
        {
            if ((object)mat1 == null || (object)mat2 == null)
                throw new ArgumentNullException();
            return !(mat1==mat2);
        }

        public void Serialize(string path)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Matrix Deserialize(string path)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    return (Matrix)formatter.Deserialize(fs);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Matrix p = obj as Matrix;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (this == p);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public Matrix Clone()
        {
            return new Matrix((double[,])array.Clone());
        }

        public static double[,] ToArray(Matrix matrix)
        {
            if ((object)matrix == null)
                throw new ArgumentNullException();

            return matrix.Clone().array;
        }
        public  double[,] ToArray()
        {
            if ((object)array == null)
                throw new ArgumentNullException();

            return Clone().array;
        }
    }
}
class MatrixConsistentException : Exception
{
    public MatrixConsistentException() : base("Matrix is not consistent") { }
    public MatrixConsistentException(string str) : base(str) { }
}
