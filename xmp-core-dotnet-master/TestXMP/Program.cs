using System;
using System.IO;

using XmpCore;

namespace TestXMP
{
    class Program
    {
        static void Main(string[] args)
        {
            IXmpMeta xmp;

            using (var stream = File.OpenRead("41fa4954-dbcb-49d0-94d4-881a02cfc063.xmp"))
                xmp = XmpMetaFactory.Parse(stream);

            foreach (var property in xmp.Properties)
                Console.WriteLine($"Path={property.Path} Namespace={property.Namespace} Value={property.Value}");

            Console.ReadLine();
        }
    }
}
