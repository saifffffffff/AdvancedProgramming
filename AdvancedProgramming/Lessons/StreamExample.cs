
namespace AdvancedProgramming.Lessons;

static class StreamExample {
    
    public static void Copy(Stream streamIn, Stream StreamOut, int bufferSize)
    {

        byte[] buffer = new byte[bufferSize];

        int bytesRead = 0;



        while ((bytesRead = streamIn.Read(buffer, 0, buffer.Length)) > 0)
        {
            StreamOut.Write(buffer, 0, bytesRead);
        }


    }

}
