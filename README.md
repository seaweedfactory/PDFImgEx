# PDFImgEx
A command line tool for extracting image files from a PDF file.

<h2>Usage</h2>

<i>PDFImgEx PDF_File_Name [Output_Path] [-o(verwrite)] [-t(itle) Title_Prefix]</i>

<b>PDF_File_Name:</b> Name of the PDF file to parse.

<b>Output_Path:</b> Write images to this path. Optional, if not present then write to same directory as source file.

<b>-overwrite:</b> Overwrite files in the output directory if they have the same name. Optional, by default this is disabled and the image is skipped.

<b>-title:</b> Add an optional prefix to the output image files. Images files are normally named _Page_TotalImageCount.Format where <b>Page</b> is the page in the document, <b>TotalImageCount</b> is a squential count of images in the document and <b>Format</b> is the file format extension.

<h2>File Conversion</h2>
Images are written according to the format used in the pdf. 
The resulting files often require conversion to be used in other programs.
Use another tool, like imagemagick, to do this: https://www.imagemagick.org/script/index.php


For example, if .jp2 (JPEG 2000) files around created, use the following command to convert all files in the output directory to PNG files.

<i>magick mogrify -format png *.jp2</i>
