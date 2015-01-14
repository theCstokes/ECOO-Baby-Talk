using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ECOO_2014_4
{
    class BabyTalk
    {

        public static void Main(String[] args)
        {
            StreamReader fin = new StreamReader("../../../DATA41.txt");
            for (int i = 0; i < 10; ++i)
            {
                string data = fin.ReadLine();
                Console.WriteLine(Solve(data));

            }
            Console.ReadKey();
        }

        static int Solve(string data)
        {
            bool[] partOfBabyWord = new bool[data.Length];
            List<BabyWord> words = new List<BabyWord>();
            for (int i = 1; i < data.Length; ++i)
            {
                char leftChar = data[i - 1];
                for (int j = Math.Min(2 * i - 1, data.Length - 1); j >= i; --j)
                {
                    if (leftChar == data[j])
                    {
                        bool isBabyWord = true;
                        int offset = 0;
                        for (int k = j - 1; k >= i; --k)
                        {
                            ++offset;
                            if (data[k] != data[i - offset - 1])
                            {
                                isBabyWord = false;
                                break;
                            }
                        }
                        if (isBabyWord)
                        {
                            words.Add(new BabyWord() { position = i - (j - i + 1), length = 2 * (j - i + 1) });
                            break;
                        }
                    }
                }
            }

            words.Sort((BabyWord bw, BabyWord bz) => (bw.position.CompareTo(bz.position)));
            int longest = 0;
            for (int i = 0; i < words.Count; ++i)
            {
                int length = words[i].length;
                int right = words[i].position + words[i].length;
                for (int j = i + 1; j < words.Count; ++j)
                {
                    if (words[j].position == right)
                    {
                        length += words[j].length;
                        right = words[j].position + words[j].length;
                    }
                    else if (words[j].position > right)
                    {
                        break;
                    }
                }
                if (length > longest)
                {
                    longest = length;
                }
            }
            return longest;
        }

    }

    struct BabyWord
    {
        public int position;
        public int length;
    }
}
