using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace JobConsumer
{
    class Program
    {
        private static int no;

        static async Task Main(string[] args)
        {
            try
            {

                QueueManager manager = new QueueManager();

                Job job;
                QueueMessageModel message;

                //while (true)
                //{
                job = new Job();
                message = new QueueMessageModel();

                Console.WriteLine("Lütfen Veri Gönderilme Frekansını Giriniz:");
                string frekans = Console.ReadLine();

                Console.WriteLine("frekans: " + frekans);
                message.Frekans = frekans;

                Console.WriteLine("Kod Giriniz: ");
                string kod = Console.ReadLine();

                Console.WriteLine("kod: " + kod);

                job.Kod = Convert.ToInt32(kod);

                Console.WriteLine("Okuyucu Giriniz: ");
                string okuyucu = Console.ReadLine();

                Console.WriteLine("okuyucu : " + okuyucu);

                job.Okuyucu = Convert.ToInt32(okuyucu);

                


                while (true)
                {
                    job.Tarih = DateTime.Now;
                    job.Uzaklik = randomGenerator();
                    job.VeriNo = dataCountNo();


                    message.JobModel = job;

                    string serializeData1 = JsonSerializer.Serialize(message); //Komplex Değer
                    byte[] data = Encoding.UTF32.GetBytes(serializeData1);

                    Thread.Sleep(Convert.ToInt32(message.Frekans));

                    manager.CreateQueue(data);

                    Console.WriteLine(message.JobModel.Tarih + " " + message.JobModel.VeriNo + " " + message.JobModel.Uzaklik);
                }

                


                //Console.WriteLine("Yeni veri girebilmek için herhangi bir tuşa basınız.");
                //Console.ReadLine();
                //Console.Clear();

                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static int randomGenerator()
        {
            Random random = new Random();
            int sayi = random.Next(0, 30000);
            return sayi;

        }
        private static int dataCountNo()
        {
            if (no < 256)
                return no++;

            no = 0;
            return no++;
        }
    }
}


