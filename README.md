Sources 
# Metadata extraction
## Exiftool - https://exiftool.org/ - basic metadata

Steps to install perl and exiftool:  
1. You must have Perl installed - https://www.activestate.com/products/perl/
2. Download the Image-ExifTool distribution from the ExifTool home page - https://exiftool.org/index.html
    (The file you download should be named "Image-ExifTool-11.79.tar.gz".)
3. Extract the ExifTool files from the archive.
    (The archive is a gzipped tar file, and can be opened with various Windows utilities, including WinZip.)
4. Rename "exiftool" to "exiftool.pl" in the exiftool distribution.
    Move "exiftool.pl" and the "lib" directory from the exiftool distribution to "C:\WINDOWS" (or any other directory in your PATH).

Now, if you have made the proper Windows associations for the ".pl" extension (an option in the ActivePerl installation), you can run exiftool by typing "exiftool.pl" at the "cmd.exe" prompt. Otherwise you should type "perl c:\windows\exiftool.pl"
  
## Mediainfo - https://mediaarea.net/en/MediaInfo - metadata encoded in a variety of standards
 
## Mpeg7fex - https://github.com/mubastan/mpeg7fex - low-level video features: Face recognition, Color Structure Descriptor, Dominant Color Descriptor, Region Shape Descriptor 

