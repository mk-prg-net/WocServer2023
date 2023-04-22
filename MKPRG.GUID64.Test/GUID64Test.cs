using System.Diagnostics;
using System.Threading.Tasks;
namespace MKPRG.GUID64.Test
{
    [TestClass]
    public class GUID64Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = new object() { };
            Parallel.For(0, 10, x =>
            {
                for (int j = 0; j < 3; j++)
                {
                    var b8 = new Byte[8];
                    MKPRG.GUID64.ThreadSafeRandom.NextBytes(b8); // 👈 Use ThreadSafeRandom directly


                    lock (obj)
                    {
                        Debug.Write("[");
                        for (int i = 0; i < 8; i++)
                        {
                            Debug.Write($"{b8[i]}, ");
                        }
                        Debug.WriteLine("]");
                    }
                }

            });

        }
    }
}