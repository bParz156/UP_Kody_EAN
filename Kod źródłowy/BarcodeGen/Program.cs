using System;
using System.IO;
using SkiaSharp;
using BarcodeStandard;
using System.Drawing.Imaging;

namespace BarcodeGen {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Laboratorium z kursu Urządzenia Peryferyjne");
            String data;
            do {
                Console.WriteLine("Podaj sekwencje: ");
                data = Console.ReadLine();
            } while (!checkSum(data));
            Console.WriteLine("Generuję kod... Proszę czekać");
            BarcodeStandard.Barcode barcode = new Barcode();
            barcode.IncludeLabel = true;
            Console.WriteLine(data.Length);
            SKImage img = barcode.Encode(BarcodeStandard.Type.Ean13, data, 290, 120);
            barcode.SaveImage("BarCodeImage.png", BarcodeStandard.SaveTypes.Png);
            Console.WriteLine("Sciezka do pliku to:");
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine("Nacisnij dowolny przycisk aby zakonczyc");
            data = Console.ReadLine();

        }

        static public bool checkSum(String data) {
            int sum=0;
            char[] bits = data.ToCharArray();
            if(data.Length!=13) {
                Console.WriteLine("Kod ma nieprawdilowa ilosc znakow");
                return false;
            }
            for(int i=0; i<12; i++) {
                if (i % 2 == 0) sum += data[i] - '0';
                else     sum += 3*(data[i] - '0');
            }
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;

            Console.WriteLine(sum);

            return sum == data[12]-'0';
        }


    }
}
