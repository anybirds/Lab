using System;
using System.Net;
using System.IO;

class HelloException {
    static void Main(string[] args) {
        WebClient client = new WebClient();
        try {
            Console.WriteLine(client.DownloadString("https://wwww.naver.com"));
        } catch(WebException webException) when (webException.Status == WebExceptionStatus.NameResolutionFailure){
            Console.WriteLine(webException.Message);
            Console.WriteLine(webException.StackTrace);
        }

        FileStream fs = null;
        try {
            fs = new FileStream("a.txt", FileMode.CreateNew);
            // do something
        } finally {
            fs?.Dispose();
        }

        // equivalent
        /*
        using (FileStream fs = new FileStream("a.txt", FileMode.CreateNew)) {
            // do something
        }
        */
    }
}