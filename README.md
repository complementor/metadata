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
  
## Mediainfo - https://mediaarea.net/en/MediaInfo - 
metadata encoded in a variety of standards including 

* Container: MPEG-4, QuickTime, Matroska, AVI, MPEG-PS (including unprotected DVD), MPEG-TS (including unprotected Blu-ray), MXF, GXF, LXF, WMV, FLV, Real...
* Tags: Id3v1, Id3v2, Vorbis comments, APE tags...
* Video: MPEG-1/2 Video, H.263, MPEG-4 Visual (including DivX, XviD), H.264/AVC, H.265/HEVC, FFV1...
* Audio: MPEG Audio (including MP3), AC3, DTS, AAC, Dolby E, AES3, FLAC...
* Subtitles: CEA-608, CEA-708, DTVCC, SCTE-20, SCTE-128, ATSC/53, CDP, DVB Subtitle, Teletext, SRT, SSA, ASS, SAMI.
 
## Mpeg7fex - https://github.com/mubastan/mpeg7fex - 
low-level video features: Face recognition, Color Structure Descriptor, Dominant Color Descriptor, Region Shape Descriptor 

# Install document database 
Mongodb - 

Run service -> cmd: mongod
Run db -> cmd: mongo

Queries: 
* use metadata
* show collections
* db.basic.find({ TracksMarkersName: : "theguys"}).pretty()

# Create Ontology 
## Protege - https://protege.stanford.edu/ 
MPEG ontology 
Video ontology
 
Xmp core - https://github.com/drewnoakes/xmp-core-dotnet 


