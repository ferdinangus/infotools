using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html_checker
{
    class Program
    {
        public static List<string> tags = new List<string>();
        public static List<string> queue = new List<string>();

        static void Main(string[] args)
        {
            while(true)
            {
                bool message_displayed = false;
                Console.WriteLine("Press A for enter an HTML");
                Console.WriteLine("Press E for exit");

                char c = Convert.ToChar(Console.ReadLine());                

                switch (c) {
                    case 'A':
                    case 'a':
                        Console.WriteLine("Please enter an HTML text to check if it's written propertly");
                        string input = Console.ReadLine();
                        string val = get_html_tags(input);

                        for (int i = 0; i < tags.Count; i++)
                        {
                            if (tags[i].Contains("/>"))
                            {
                                tags.Remove(tags[i]);
                                i--;
                            }
                            else if (tags[i].Contains("<<d>")) 
                            {
                                tags.Remove(tags[i]);
                                i--;
                            }
                            else if (!tags[i].Contains("</"))
                            {
                                queue.Add(tags[i]);
                                tags.Remove(tags[i]);
                                i--;
                            }
                            else if (queue.Count > 0 && queue.Last().Split('<', '>')[1].Contains(tags[i].Split('/', '>')[1]))
                            {
                                queue.Remove(queue.Last());
                                tags.Remove(tags[i]);
                                i--;
                            }
                            else if (queue.Count > 0 && !queue.Last().Split('<', '>')[1].Contains(tags[i].Split('/', '>')[1]))
                            {
                                Console.WriteLine("Missing " + queue.Last() + "c lose tag instead of " + tags[i]);
                                message_displayed = true;
                            }
                        }

                        if (tags.Count == 0 && queue.Count == 0 && !message_displayed)
                        {
                            Console.WriteLine("Correctly tagged paragraph");
                        }
                        else if (queue.Count > 0 && !message_displayed)
                        {
                            Console.Write("Missing");

                            foreach (var item in queue)
                            {
                                Console.Write(" " + item + " ");
                            }

                            Console.WriteLine("close tag");
                        }
                        else if (tags.Count > 0 && !message_displayed)
                        {
                            Console.Write("Missing");

                            foreach (var item in tags)
                            {
                                Console.Write(" " + item + " ");
                            }

                            Console.WriteLine("open tag");
                        }

                        break;
                    case 'E':
                    case 'e':
                        return;
                }                
            }
        }

        private static string get_html_tags(string input)
        {
            string regex = @"<"
                    + @"(?<endTag>/)?"    //Captures the / if this is an end tag.
                    + @"(?<tagname>\w+)"    //Captures TagName
                    + @"("                //Groups tag contents
                    + @"(\s+"            //Groups attributes
                    + @"(?<attName>\w+)"  //Attribute name
                    + @"("                //groups =value portion.
                    + @"\s*=\s*"            // = 
                    + @"(?:"        //Groups attribute "value" portion.
                    + @"""(?<attVal>[^""]*)"""    // attVal='double quoted'
                    + @"|'(?<attVal>[^']*)'"        // attVal='single quoted'
                    + @"|(?<attVal>[^'"">\s]+)"    // attVal=urlnospaces
                    + @")"
                    + @")?"        //end optional att value portion.
                    + @")+\s*"        //One or more attribute pairs
                    + @"|\s*"            //Some white space.
                    + @")"
                    + @"(?<completeTag>/)?>";

            string text = String.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Equals('<'))
                {
                    text = input.Substring(i, Math.Max(input.IndexOf('>') + 1, 0) - i);
                    
                    int lastLocation = input.IndexOf( ">" );
                    if(lastLocation >= 0)
                    {
                        input =  input.Substring( lastLocation + 1 );

                        if (System.Text.RegularExpressions.Regex.IsMatch(text, regex))
                        {
                            tags.Add(text);                            
                        }

                        input = get_html_tags(input);
                    }
                }
            }

            return input;
        }
    }
}
