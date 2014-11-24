﻿using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using VkApiGenerator.Model;

namespace VkApiGenerator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string categoryName = "Likes";

            var methods = new[]
            {
                "likes.getList",
                "likes.add",
                "likes.delete",
                "likes.isLiked"
            };

            var parser = new VkApiParser();
            var generator = new VkApiGenerator();
            var source = InitializeSource(categoryName);
            
            foreach (string methodName in methods)
            {
                System.Console.WriteLine("*** {0} ***", methodName);
                VkMethodInfo methodInfo;
                try
                {
                    methodInfo = parser.Parse(methodName);
                }
                catch (WebException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    continue;
                }
                System.Console.WriteLine("DESCRIPTION: {0}", methodInfo.Description);
                System.Console.WriteLine("RETURN TEXT: {0}", methodInfo.ReturnText);
                System.Console.WriteLine("RETURN TYPE: {0}", methodInfo.ReturnType);

                System.Console.WriteLine("PAPAMETERS:");
                foreach (var p in methodInfo.Params)
                {
                    System.Console.WriteLine("    {0} - {1}", p.Name, p.Description);
                }

                source.Append(generator.GenerateMethod(methodInfo))
                    .AppendLine()
                    .AppendLine();

                System.Console.WriteLine("\n========================================\n");
                //System.Console.ReadLine();
            }

            source.Append("}}");

            System.Console.WriteLine("Saving to disk");

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, @"..\VkNet\Categories\");
            path = string.Format("{0}\\{1}Category_Generated.cs", path, categoryName);

            File.WriteAllText(path, source.ToString());

            System.Console.WriteLine("done.");
        }

        // todo move it to another class and use razor engine
        private static StringBuilder InitializeSource(string categoryName)
        {
            var sb = new StringBuilder().AppendFormat(@"namespace VkNet.Categories
{{
    using System.Collections.ObjectModel;

    using Utils;
    using Model;

    public class {0}Category
    {{
        private readonly VkApi _vk;

        internal {0}Category(VkApi vk)
        {{
            _vk = vk;
        }}
", categoryName);

            return sb;
        }
    }
}
