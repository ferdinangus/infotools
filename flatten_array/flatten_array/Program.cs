using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flatten_array
{
    class Program
    {
        public static List<object> list = new List<object>();

        static void Main(string[] args)
        {
            int[][] twoDimensional = new int[][] { 
                                      new int[] {1, 2},
                                      new int[] {3, 4},
                                      new int[] {5, 6}
                                     };

            int[][][] threeDimensional = new int[][][] { 
                                      new int[][] 
                                      {
                                          new int[] {1, 2},
                                          new int[] {3, 4}
                                      },
                                      new int[][] 
                                      {
                                          new int[] {5, 6},
                                          new int[] {7, 8}
                                      },
                                      new int[][] 
                                      {
                                          new int[] {9, 10},
                                          new int[] {11, 12}
                                      },
                                     };

            var result = flatten(threeDimensional);

            foreach (var item in result)
            {
                Console.Write(item + " ");
            }

            Console.ReadLine();
        }

        public static object[] flatten(object[] array)
        {
            foreach (var item in array)
            {
                Type type = item.GetType();
                if (type.IsArray && type != typeof(Int32[]))
                {
                    array = flatten((object[])item);
                }
                else if (type.IsArray && type == typeof(Int32[]))
                {
                    foreach (var item2 in (Int32[])item)
                    {
                        list.Add(item2);
                    }
                }
                else
                {
                    list.Add(item);
                }
            }

            return list.ToArray();
        }
    }
}
