using Emotiv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FG
{
    class AVGPower
    {
        static int userID = -1;
        TextWriter file;
        EmoEngine engine;
        void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            //Console.WriteLine("User Added Event has occured");
            userID = (int)e.userId;

            engine.DataAcquisitionEnable((uint)userID, true);
            engine.DataSetBufferSizeInSec(1);
            //EmoEngine.Instance.IEE_FFTSetWindowingType((uint)userID, EdkDll.IEE_WindowingTypes.IEE_HAMMING);
        }

        public AVGPower()
        {
            engine = EmoEngine.Instance;
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);
            engine.Connect();
        }
        
        public AVGPower(TextWriter infile)
        {
            engine = EmoEngine.Instance;
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);
            engine.Connect();
            file = infile;
        }

        public void getData()
        {
            Boolean j = true;
            // create the engine

            string header = "Theta, Alpha, Low_beta, High_beta, Gamma"; ;
            file.WriteLine(header);
            file.WriteLine("");

            EdkDll.IEE_DataChannel_t[] channelList = new EdkDll.IEE_DataChannel_t[4] { EdkDll.IEE_DataChannel_t.IED_P7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O1,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O2 };
            while (j)
            {
                engine.ProcessEvents(10);

                if (userID < 0) continue;

                double[] alpha = new double[1];
                double[] low_beta = new double[1];
                double[] high_beta = new double[1];
                double[] gamma = new double[1];
                double[] theta = new double[1];
                Thread.Sleep(3000);
                for (int i = 0; i < 4; i++)
                {
                    Int32 result = engine.IEE_GetAverageBandPowers((uint)userID, channelList[i], theta, alpha, low_beta, high_beta, gamma);
                    
                    if (result == EdkDll.EDK_OK)
                    {
                        //file.Write(theta[0] + ",");
                        file.Write(alpha[0] + ",");
                        //file.Write(low_beta[0] + ",");
                        //file.Write(high_beta[0] + ",");
                        //file.WriteLine(gamma[0] + ",");

                        Console.WriteLine("Theta: " + theta[0]);
                    }
                }
                file.WriteLine();
                j = false;
            }

            engine.Disconnect();
        }
        public bool getData(String dir)
        {
            int j = 4;
            // create the engine

            //string header = "Theta, Alpha, Low_beta, High_beta, Gamma"; ;
            //file.WriteLine(header);
            //file.WriteLine("");

            EdkDll.IEE_DataChannel_t[] channelList = new EdkDll.IEE_DataChannel_t[14] {
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC5,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC6,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O1,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O2 };
            double[,] temp = new double[14, j+1];
            while (j >= 0)
            {
                engine.ProcessEvents(10);

                if (userID < 0) continue;

                double[] alpha = new double[1];
                double[] low_beta = new double[1];
                double[] high_beta = new double[1];
                double[] gamma = new double[1];
                double[] theta = new double[1];
                for (int i = 0; i < 14; i++)
                {
                    Int32 result = engine.IEE_GetAverageBandPowers((uint)userID, channelList[i], theta, alpha, low_beta, high_beta, gamma);
                    
                    if (result == EdkDll.EDK_OK)
                    {
                        //if (alpha[0] > 20)
                        //    return false;
                        //else
                            temp[i, j] = alpha[0];
                    }
                }
                j--;
                Thread.Sleep(500);
            }
            for(int i = 3;i>=0;i--)
            {
                for(int k = 0;k<14;k++)
                {
                    file.Write(temp[k,i] + ",");
                }
                file.WriteLine(dir);
            }
            
            return true;

        }
        public double[,] getOnlineData(bool mode)
        {
            int j = 4;
            // create the engine
            double[,] onlineData = new double[j,14];

            EdkDll.IEE_DataChannel_t[] channelList = new EdkDll.IEE_DataChannel_t[14] {
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC5,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC6,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O1,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O2 };

            Thread.Sleep(3000);
            while (j > 0)
            {
                engine.ProcessEvents(10);

                if (userID < 0) continue;

                double[] alpha = new double[1];
                double[] low_beta = new double[1];
                double[] high_beta = new double[1];
                double[] gamma = new double[1];
                double[] theta = new double[1];
                for (int i = 0; i < 14; i++)
                {
                    Int32 result = engine.IEE_GetAverageBandPowers((uint)userID, channelList[i], theta, alpha, low_beta, high_beta, gamma);

                    if (result == EdkDll.EDK_OK)
                    {
                        if (mode)
                        {
                            if (alpha[0] > 20)
                                return null;
                            onlineData[j - 1, i] = alpha[0];
                        }
                        else
                        {
                            if (low_beta[0] > 20)
                                return null;
                            onlineData[j - 1, i] = low_beta[0];
                        }
                    }
                }
                j--;
                Thread.Sleep(500);

            }
            
            return onlineData;
        }

        public bool getDataRaw(int level)
        {
            int j = 0; 

            EdkDll.IEE_DataChannel_t[] channelList = new EdkDll.IEE_DataChannel_t[14] {
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC5,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC6,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O1,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O2 };
            double[,] dataToWrite = new double[14, 128];
            while (j < 128)
            {
                engine.ProcessEvents();

                if (userID < 0) continue;
                
                Dictionary<EdkDll.IEE_DataChannel_t, double[]> data = engine.GetData((uint)userID);

                if (data == null)
                    continue;
                for (int i = 0; i < data[EdkDll.IEE_DataChannel_t.IED_TIMESTAMP].Length; i++)
                {
                    for (int k = 0; k < 14; k++)
                    {
                        //if (data[channelList[k]][i] > 4300 || data[channelList[k]][i] < 4100)
                        //    return false;
                        dataToWrite[k, j] = data[channelList[k]][i];
                    }
                    j++;
                    if (j > 127)
                        break;
                }
            }
            for (int i = 0; i < 128; i++)
            {
                for (int k = 0; k < 14; k++)
                {
                    file.Write(dataToWrite[k,i] + ",");
                }
                file.WriteLine(level);
            }

            return true;
        }
        public double[,] getOnlineDataRaw()
        {
            int j = 0;
            double[,] onlineData = new double[256,14];

            EdkDll.IEE_DataChannel_t[] channelList = new EdkDll.IEE_DataChannel_t[14] {
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_AF4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F3,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F4,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_F8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC5,
                                                                                       EdkDll.IEE_DataChannel_t.IED_FC6,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_T8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P7,
                                                                                       EdkDll.IEE_DataChannel_t.IED_P8,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O1,
                                                                                       EdkDll.IEE_DataChannel_t.IED_O2 };
            Thread.Sleep(2000);
            while (j < 256)
            {
                engine.ProcessEvents();

                if (userID < 0) continue;

                Dictionary<EdkDll.IEE_DataChannel_t, double[]> data = engine.GetData((uint)userID);

                if (data == null)
                    continue;

                for (int i = 0; i < data[EdkDll.IEE_DataChannel_t.IED_TIMESTAMP].Length; i++)
                {
                    for (int k = 0; k < 14; k++)
                    {
                        onlineData[j, k] = data[channelList[k]][i];
                    }
                    j++;
                    if (j > 255)
                        break;
                }
            }
            return onlineData;
        }


        public void Disconnect()
        {
            engine.Disconnect();
        }
    }
}
