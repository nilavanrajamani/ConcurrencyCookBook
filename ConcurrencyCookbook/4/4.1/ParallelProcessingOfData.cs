using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ParallelProcessingOfData
    {
        static async Task Main(string[] args)
        {
            Matrix matrix1 = new Matrix();
            matrix1.Name = "A";
            matrix1.IsInvertible = true;
            Matrix matrix2 = new Matrix();
            matrix2.Name = "B";
            matrix2.IsInvertible = true;
            Matrix matrix3 = new Matrix();
            matrix3.Name = "C";
            matrix3.IsInvertible = false;
            Matrix matrix4 = new Matrix();
            matrix4.Name = "D";
            matrix4.IsInvertible = false;

            ParallelProcessingOfData parallelProcessingOfData = new ParallelProcessingOfData();
            parallelProcessingOfData.InvertMetrics(new Matrix[] { matrix1, matrix2, matrix3, matrix4 });
        }

        class Matrix
        {
            public bool IsInvertible { get; set; }
            public string Name { get; set; }

            public void Rotate(float degrees)
            {

            }

            public void Invert()
            {

            }
        }

        void RotateMetrics(IEnumerable<Matrix> matrices, float degrees)
        {
            Parallel.ForEach(matrices, (mat) => { mat.Rotate(degrees); });
        }

        void InvertMetrics(IEnumerable<Matrix> matrices)
        {
            Parallel.ForEach(matrices, (matrix, state) =>
            {
                if (!matrix.IsInvertible)
                {
                    state.Stop();
                }
                else
                {
                    matrix.Invert();
                    Console.WriteLine(matrix.Name);
                }
            });

        }
    }
}
