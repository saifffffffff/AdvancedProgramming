
using AdvancedProgramming.Lessons;
using System.Collections;
using System.Diagnostics;
using System.Formats.Asn1;
using System.IO.Compression;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Markup;
using System.Xml.Schema;
using static Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{

    public static string Hash(string input)
    {
        using SHA256 sha = SHA256.Create();

        byte[] hashedBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));

        string hashedValue = BitConverter.ToString(hashedBytes).Replace("-", ""); // 64 characters each one is a hexadecimal value , each hexadecimal is 4 bits 

        return hashedValue;

    }

    public static string Encrypt1(string plainText, string key)
    {
        
         
        var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(key);

        aes.IV = new byte[aes.BlockSize / 8];

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        var memoryStream = new MemoryStream();

        var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        var streamWriter = new StreamWriter(cryptoStream);

        streamWriter.Write(plainText);
        

        return Convert.ToBase64String(memoryStream.ToArray());

        
    }
    
    static string Encrypt(string plainText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
     
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[aesAlg.BlockSize / 8];


            // Create an encryptor
            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            {
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {

                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                            return Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }

                }



            }

        }

            // Set the key and IV for AES encryption
            // Encrypt the data
    }



    static string EncryptAbo(string plainText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            // Set the key and IV for AES encryption
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16];
            //aesAlg.IV = new byte[aesAlg.BlockSize / 8];


            // Create an encryptor
            ICryptoTransform encryptor = aesAlg.CreateEncryptor();


            // Encrypt the data
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }


                // Return the encrypted data as a Base64-encoded string
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }



    public static void PrintArray<T>(T[] arr)
    {
        foreach (T item in arr)
            Console.Write(item + " ");
        Console.WriteLine();

    }



    public static class StreamExamples
    {
        public static class StreamClass
        {

            public static string Read()
            {
                // int Read(byte[] buffer, int offset, int count);

                // Parameters 
                // buffer → array to store the data
                // offset → start position in the buffer 
                // count → number of bytes to read

                // Returns 
                // Number of bytes actually read.

                using Stream stream = new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open);

                byte[] buffer = new byte[stream.Length];

                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer, 0, bytesRead);



            }
            // They work with **raw typed data** — they write the actual bytes of each primitive type directly, with no text encoding involved:

            public static void Write(string value)
            {
                //2.Write()
                //Writes bytes from a buffer into the stream.
                //void Write(byte[] buffer, int offset, int count);

                using Stream stream = new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open);

                byte[] buffer = Encoding.UTF8.GetBytes(value);

                stream.Write(buffer, 0, buffer.Length);


            }

            public static void ReadBytes()
            {
                using Stream stream = new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open);

                int b = 0;

                while ((b = stream.ReadByte()) != -1)
                {
                    Console.Write((char)b);
                }
            }

            public static void WriteBytes(byte[] bytes)
            {

                using Stream stream = new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open);

                foreach (byte _byte in bytes)
                    stream.WriteByte(_byte);

            }

            public static void WriteBytes(string value) => WriteBytes(Encoding.UTF8.GetBytes(value));

        }


        public static class StreamReaderWriterClasses
        {

            // stream reader / writer are high level wrappers over stream that writes and read on any stream using high level data types ( not bytes ) ( memory , network , file streams )  

            public static string ReadToEnd()
            {
                using StreamReader reader = new StreamReader(new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open));

                return reader.ReadToEnd();
            }
            public static List<string> ReadLines()
            {
                using StreamReader reader = new StreamReader(new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open));
                List<string> lines = new();
                string? line;
                while (((line = reader.ReadLine()) != null))
                {
                    lines.Add(line);
                }



                return lines;

            }

            public static void WriteLine(string line)
            {
                using StreamWriter writer = new StreamWriter(new FileStream(@"..\..\..\Assets\TextFile1.txt", FileMode.Open));

                writer.WriteLine(line);
            }

            public static string? Read(Stream stream)
            {
                using StreamReader reader = new StreamReader(stream, leaveOpen: true);

                return reader.ReadToEnd();

            }

            public static void Write<T>(Stream stream, T value)
            {
                /*
                 if you added using the stream will be unreadable because using calls
                 the dispose method which dispose the underlying stream then
                 the passed stream also will be disposed and the CanRead Property will
                 be false which means that the stream reader can not read it
                 and will throw an excetion if you tried to pass the stream to the constructor
                 , so leave open is used

                 StreamWriter does not write directly to the stream. It has an internal buffer 
                 (a chunk of memory it holds onto for performance). It accumulates your data and
                 only pushes it down to the underlying stream in bulk, to avoid many small writes.
                 
                 Flush() forces the buffer to push everything it's holding down into the underlying 
                 stream immediately.
                */


                using StreamWriter writer = new StreamWriter(stream, leaveOpen: true);
                writer.Write(value); // value will be converted to string first then written

                writer.Flush(); // flush == push values from the internal buffer to actual stream
                                // if you used using block it will close and flush automatically 
                stream.Position = 0;

            }

        }

        public static class BinaryReaderWriterClasses
        {
            public static void Example()
            {
                MemoryStream ms = new MemoryStream();

                StreamWriter writer = new StreamWriter(ms, leaveOpen: true);
                writer.Write(25);        // int
                writer.Write(3.14);      // double
                writer.Write(true);      // bool
                writer.Write("Saif");

                ms.Position = 0;
                BinaryReader reader = new BinaryReader(ms);

                int age = reader.ReadInt32();
                double number = reader.ReadDouble();
                bool flag = reader.ReadBoolean();
                string name = reader.ReadString();

            }
        }

        public static class StreamPositioning
        {
            // Length : total bytes in the stream
            // Position : current position of the read / write head (every read , writer operation moves position forward automatically )
            // Seek(offset , SeekOrigin ) : method that moves the head in more expressive way than than sitting the postion manually , you describe head movement based on the relative point
            // offset + SeekOrigin = head point ( new position) 

            public static void Example()
            {
                // positions in a stream are index-based, just like an array, starting at 0
                char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                MemoryStream memoryStream = new MemoryStream();
                StreamWriter sw = new StreamWriter(memoryStream, leaveOpen: true);
                sw.Write(chars);
                sw.Flush();

                Console.WriteLine($"Length ( total bytes ) : {chars.Length}");
                memoryStream.Position = 0;// writing to the stream moved the position to end we have to set it back to begining

                StreamReader reader = new StreamReader(memoryStream);

                Console.WriteLine(reader.ReadToEnd());

                memoryStream.Position = 0;// writing to the stream moved the position to end we have to set it back to begining

                memoryStream.Position = 5; // [ index : 5 , End ]

                Console.WriteLine(reader.ReadToEnd());

                memoryStream.Seek(0, SeekOrigin.Begin); // [ 0 , end ] 

                Console.WriteLine(reader.ReadToEnd());

                memoryStream.Position = 4;// writing to the stream moved the position to end we have to set it back to begining

                memoryStream.Seek(1, SeekOrigin.Current);// 1 + 4 = [ 5 , end ] 

                Console.WriteLine(reader.ReadToEnd());

                memoryStream.Seek(-3, SeekOrigin.End); // -3 + 10 (end is the last index + 1)  = [6 , end ] 

                Console.WriteLine(reader.ReadToEnd());



            }

        }

        public static class CopyingToStream
        {
            public static void CopyNewStyle (Stream source , Stream destination)
            {
                source.Position = 0;
                source.CopyTo(destination);
                destination.Position = 0;
            }

            public static void CopyOldStyle(Stream source , Stream destination)
            {
                source.Position = 0;
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ( ( bytesRead = source.Read(buffer, 0, buffer.Length) ) != 0 )
                {
                    destination.Write(buffer, 0, bytesRead);
                }
                
                destination.Position = 0;
                
            }
        }
    }
  
    class StreamChaining
    {
        public static MemoryStream CompressString (string value)
        {
            // compressing a stream

            MemoryStream ms = new MemoryStream();
            
            GZipStream compressStream = new GZipStream(ms, CompressionLevel.Fastest);
            
            StreamWriter writer = new StreamWriter(compressStream);
            
            writer.Write(value);
            
            writer.Flush();
            compressStream.Flush();

            return ms;
        }

        public static string Decompress(MemoryStream compressedStream )
        {
            compressedStream.Position = 0;

            GZipStream decompressedStream = new GZipStream(compressedStream , CompressionMode.Decompress);

            StreamReader reader = new StreamReader(decompressedStream);

            return reader.ReadToEnd();
            
        }

        public static string Encrypt(string input  , string _16bitKey )
        {
            
            if (_16bitKey.Length != 16) throw new ArgumentException();

            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_16bitKey);

            using MemoryStream ms = new MemoryStream();
            
            using ICryptoTransform cryptoTrans = aes.CreateEncryptor();
            
            using CryptoStream cs = new CryptoStream(ms, cryptoTrans , CryptoStreamMode.Write); // decorator that wraps another stream and transforms the bytes inside it into a new form 

            using (StreamWriter sw = new(cs))
            {
                sw.Write(input);
                // if you did not used the block here 
                // the Dispose method will be called
                // at the end of the scope and
                // the flush won't be called
                // Note : Dispose the stream writer 
                // will also dispose the underlying
                // wrapped stream which means that the
                // CryptoStream will also be flushed
            }


            

            return Convert.ToBase64String(ms.ToArray());


        }
        public static string Decrypt(string cipherText, string _16bitKey)
        {

            if (_16bitKey.Length != 16) throw new ArgumentException();

            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_16bitKey);

            byte[] bytes = Convert.FromBase64String(cipherText);

            MemoryStream ms = new MemoryStream(bytes);

            ICryptoTransform cryptoTrans = aes.CreateDecryptor();

            CryptoStream cs = new CryptoStream(ms, cryptoTrans, CryptoStreamMode.Read); // decorator that wraps another stream and transforms the bytes inside it into a new form 

            using StreamReader sr = new(cs);
            
            return sr.ReadToEnd();


        }

    }

    public static void EncryptFile(string source_path, string destination_path , string key, byte[] IV)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = IV;
            
            using FileStream fsIn = new FileStream( source_path  , FileMode.Open );

            using FileStream fsOut = new FileStream(destination_path, FileMode.OpenOrCreate);
            
            using CryptoStream cs = new(fsOut, aes.CreateEncryptor(), CryptoStreamMode.Write);

            fsIn.CopyTo(cs);

        }

    }
    public static void DecryptFile(string source_path , string destination_path , string key , byte[] IV)
    {
        using Aes aes = Aes.Create();
        
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = IV;
        var decryptor = aes.CreateDecryptor();

        using FileStream fsIn = new(source_path , FileMode.Open );
        using FileStream fsOut = new(destination_path, FileMode.Create ) ;
        //using CryptoStream cs = new CryptoStream(fsOut , decryptor , CryptoStreamMode.Write);
        using CryptoStream cs = new CryptoStream(fsIn , decryptor , CryptoStreamMode.Read);


        cs.CopyTo(fsOut);

        
        
    }
    public static async Task Main()
    {

        //string a = EncryptAbo("Plain Text", "1234567891012134");
        //Console.WriteLine(a);
        //Console.WriteLine();
        //MemoryStream ms = new MemoryStream(Convert.FromBase64String(a));

        //Aes aes = Aes.Create();
        //aes.IV = new byte[16];
        //aes.Key = Encoding.UTF8.GetBytes("1234567891012134");
        //var dec = aes.CreateDecryptor();





        //MyCryptoStream cs = new (ms , dec , CryptoStreamMode.Read);

        //StreamReader sr = new(cs);

        //string d = sr.ReadToEnd ();

        //Console.WriteLine(d);

        string source = @"C:\Images\zoro.jpg";
        string dest = @"C:\Images\decryptedImage.jpg";
        string key = "1234567890987654";
        byte[] IV = new byte[16];
        EncryptFile(source , dest , key, IV);
        DecryptFile(dest, @"C:\Images\encryptedImage.jpg", key, IV);
         
    }
}







// stream is an abstract class

//string emoji = "✊";
//var a = Encoding.UTF32.GetBytes(emoji);
//PrintArray(a);

//string arabic = "س";
//var b  = Encoding.UTF32.GetBytes(arabic);
//PrintArray(b);

//string chinese = "電";
//Console.WriteLine(chinese.Length);
//var c = Encoding.UTF32.GetBytes(chinese);
//PrintArray(c);

//string family = "👨‍👩‍👧‍👦";
//Console.WriteLine();
//Console.WriteLine(family.Length);
//Console.WriteLine(family.Substring( 0 , 1));
////Console.WriteLine(emoji.Length);
//void PrintArray(byte[] arr) { foreach ( var e in arr) Console.Write (e + " "); Console.WriteLine(); }










public class MyCryptoStream : Stream 
{
    private readonly Stream _wrappedStream;

    private readonly ICryptoTransform _transform;

    private readonly CryptoStreamMode _mode;

    private bool _finalBlockTransformed;

    byte[] _inputBuffer;
    int _inputBufferSize;


    public override bool CanRead => _mode == CryptoStreamMode.Read && _wrappedStream.CanRead;

    public override bool CanWrite => _mode == CryptoStreamMode.Write && _wrappedStream.CanWrite;
    
    public override bool CanSeek => false;

    public override long Length => throw new NotSupportedException();

    public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

    public MyCryptoStream(Stream wrappedStream, ICryptoTransform transform, CryptoStreamMode mode)
    {
        _wrappedStream = wrappedStream;
        _transform = transform;
        _mode = mode;
        
        _inputBuffer = new byte[_transform.InputBlockSize];
        _inputBufferSize = 0;
    }


    public override void Write(byte[] buffer, int offset, int count)
    {

        // buffer -> inputBuffer 
        // write inputBuffer -> encryptor -> outputBuffer
        // write outoputBuffer -> _wrappedStream
        // final block -> flush by FlushFinalBlock in dispose method
        
        if (_mode != CryptoStreamMode.Write)
            throw new InvalidOperationException("Stream is no in write mode");

        int bytesConsumed = 0;

        while (bytesConsumed < count)
        {

            int spaceInBuffer = _transform.InputBlockSize - _inputBufferSize;

            int remainingBytes = count - bytesConsumed;
            
            int bytesToCopy = Math.Min(spaceInBuffer, remainingBytes);

            Array.Copy(buffer, bytesConsumed + offset, _inputBuffer, _inputBufferSize , bytesToCopy);

            _inputBufferSize += bytesToCopy;
            bytesConsumed += bytesToCopy;


            if (_inputBufferSize == _transform.InputBlockSize)
            {
                byte[] outputBuffer = new byte[_transform.OutputBlockSize];
                _transform.TransformBlock(_inputBuffer, 0, _inputBuffer.Length, outputBuffer, 0);
                _wrappedStream.Write(outputBuffer, 0, outputBuffer.Length);
                _inputBufferSize = 0;
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if ( disposing && !_finalBlockTransformed && _mode == CryptoStreamMode.Write )
        {
            FlushFinalBlock();
        }

        base.Dispose(disposing);
    
    }

    public void FlushFinalBlock ()
    {

        if (_finalBlockTransformed)
            throw new InvalidOperationException("FlushFinalBlock already called.");

        byte[] outputBuffer = _transform.TransformFinalBlock(_inputBuffer , 0 , _inputBufferSize);
        
        _wrappedStream.Write(outputBuffer, 0 , outputBuffer.Length);

        _finalBlockTransformed = true;

        _wrappedStream.Flush();

    }

    public override void Flush()
    {
        // nothing
    }

    public override int Read(byte[] buffer, int offset, int count)
    {

        int totalReadBytes = 0;
        int writtenBytesInCallerBuffer = 0;
        int remainingBytesInStream = 0;
        

        while (  writtenBytesInCallerBuffer < count )
        {

            int spaceInBuffer = _inputBuffer.Length - _inputBufferSize;
            
            int bytesReadThisIteration = _wrappedStream.Read(_inputBuffer , _inputBufferSize , spaceInBuffer);
            
            _inputBufferSize += bytesReadThisIteration;
            totalReadBytes += bytesReadThisIteration;
            


            if (_inputBuffer.Length == _inputBufferSize)
            {
                byte[] decryptedBytes = new byte[_transform.OutputBlockSize];
                _transform.TransformBlock(_inputBuffer, 0, _inputBufferSize, decryptedBytes, 0);
                Array.Copy(decryptedBytes, 0, buffer, writtenBytesInCallerBuffer, decryptedBytes.Length);
                writtenBytesInCallerBuffer += decryptedBytes.Length;
                _inputBufferSize = 0;
            }
            
             
        }

        if (remainingBytesInStream > 0 )
        {
            byte[] outputBuffer = _transform.TransformFinalBlock(_inputBuffer, 0, _inputBufferSize);
            Array.Copy(outputBuffer , 0 , buffer , writtenBytesInCallerBuffer , outputBuffer.Length);
        }

        // copy decrypted bytes to the caller's buffer

        return totalReadBytes;


        
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotImplementedException();
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

}