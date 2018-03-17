using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFImgEx
{
    class Program
    {
        static void Main(string[] args)
        {
            String pdfFile = null;
            String outputPath = null;
            String title = null;
            bool overwriteFiles = false;

            if(args.Length >= 1)
            {
                //Set input file name
                pdfFile = args[0];

                //Check if input file exists
                if (!File.Exists(pdfFile))
                {
                    Console.WriteLine(String.Format("ERROR: PDF file {0} does not exist.", pdfFile));
                    Environment.Exit(1);
                }
            }

            if(args.Length >= 2)
            {
                //Determine if options flags are present
                bool foundOutputPath = false;
                int argumentIndex = 1;
                int titleIndex = -1;
                foreach(String arg in args.Skip(1))
                {
                    //Skip the title once found
                    if (argumentIndex != titleIndex)
                    {
                        if (!String.IsNullOrEmpty(arg))
                        {
                            if (arg.StartsWith("-"))
                            {
                                if (arg.Equals("-overwrite", StringComparison.OrdinalIgnoreCase)
                                || arg.Equals("-o", StringComparison.OrdinalIgnoreCase))
                                {
                                    overwriteFiles = true;
                                }
                                else if (arg.Equals("-title", StringComparison.OrdinalIgnoreCase)
                                || arg.Equals("-t", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (argumentIndex < args.Length)
                                    {
                                        title = args[argumentIndex + 1];

                                        //Set flag to ignore next argument
                                        titleIndex = argumentIndex + 1;

                                        if (String.IsNullOrEmpty(title)
                                        || title.Contains(@"\"))
                                        {
                                            //Invalid argument
                                            Console.WriteLine(String.Format("ERROR: Invalid title: {0}", title));
                                            Environment.Exit(4);
                                        }
                                    }
                                    else
                                    {
                                        //Invalid argument
                                        Console.WriteLine(String.Format("ERROR: Invalid title: {0}", arg));
                                        Environment.Exit(4);
                                    }
                                }
                                else
                                {
                                    //Invalid argument
                                    Console.WriteLine(String.Format("ERROR: Invalid argument: {0}", arg));
                                    Environment.Exit(2);
                                }
                            }
                            else if (!foundOutputPath)
                            {
                                foundOutputPath = true;
                                outputPath = arg;
                                if (!Directory.Exists(arg))
                                {
                                    //Invalid argument
                                    Console.WriteLine(String.Format("ERROR: Output path {0} does not exist.", arg));
                                    Environment.Exit(3);
                                }
                            }
                            else
                            {
                                //Invalid argument
                                Console.WriteLine(String.Format("ERROR: Invalid argument: {0}", arg));
                                Environment.Exit(2);
                            }
                        }
                    }

                    argumentIndex++;
                }
            }

            //Check if pdf file has been specified.
            if (pdfFile == null)
            {
                Console.WriteLine("ERROR: No PDF file to parse.");
                Environment.Exit(5);
            }

            //If outputPath is empty, use same directory as file.
            if (outputPath == null)
            {
                outputPath = Path.GetDirectoryName(pdfFile);
            }

            //Parse file and exit
            try
            {
                int imagesExtracted = PDFImageExtract.Core.ImageExtractor.ExtractImagesFromFile(
                    pdfFile, title, outputPath, overwriteFiles);

                Console.WriteLine(String.Format("Extracted {0} images.", imagesExtracted));

                Environment.Exit(0);
            }
            catch (Exception e)
            {
                //General error
                Console.WriteLine(String.Format("ERROR: {0}", e.Message));
                Environment.Exit(6);
            }
        }
    }
}
