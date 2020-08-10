using System;
using System.Text;
using RabbitMQ.Client;

namespace TCT_App
{
    public class MocRabbitMQ
    {
        private const string UserName           = "guest";
        private const string Password           = "dmtDmt@2020";
        private const string HostName           = "evthai.info";

        private const string EventQueueName     = "qm-events-rto";
        private const string EventExchanges     = "em-events";

        private const string TransQueueName     = "qm-trans-hq";
        private const string TransExchanges     = "em-trans";

        ConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = HostName,
            UserName = UserName,
            Password = Password,
        };

        public IConnection connection = null;
        private IModel channel = null;
        //---------------------------------------------------------------
        public MocRabbitMQ()
        {

        }

        public bool ConnectMocRabbitMQ()
        {
            if (!TCT_Lib.bEnableDcRabbitMQ)
                return false;

            if (!TCT_Lib.laneController.TestNetworkConnection(TCT_Lib.szDC_RabbitMQ_ServerIPAddress, TCT_Lib.iDC_RabbitMQ_ServerPort))
            {
                Console.WriteLine($"- Fail TestNetworkConnection to RabbitMQ => ConnectMOC Rabbit MQ : IP = {TCT_Lib.szDC_RabbitMQ_ServerIPAddress}, PORT = {TCT_Lib.iDC_RabbitMQ_ServerPort}");
                LogInfo.WriteLog($"- Fail TestNetworkConnection to RabbitMQ => ConnectMOC Rabbit MQ : IP = {TCT_Lib.szDC_RabbitMQ_ServerIPAddress}, PORT = {TCT_Lib.iDC_RabbitMQ_ServerPort}");
                return false;
            }

            try
            {
                connection  = connectionFactory.CreateConnection();
                channel     = connection.CreateModel();

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Fail Connect RabbitMQ : {err.Message}");
                LogInfo.WriteLog($"Fail Connect RabbitMQ : {err.Message}");
                connection  = null;
                channel     = null;

                return false;
            }
        }

        public void CloseMocRabbitMQ()
        {
            channel.Close();
            connection.Close();
        }

        public bool SendLaneEventRabbitMQ(string keyName, string msg)
        {
            if (!TCT_Lib.bEnableDcRabbitMQ)
                return false;

            if (!TCT_Lib.laneController.TestNetworkConnection(TCT_Lib.szDC_RabbitMQ_ServerIPAddress, TCT_Lib.iDC_RabbitMQ_ServerPort))
            {
                Console.WriteLine($"- Fail TestNetworkConnection to RabbitMQ => SendLaneEventRabbitMQ : IP = {TCT_Lib.szDC_RabbitMQ_ServerIPAddress}, PORT = {TCT_Lib.iDC_RabbitMQ_ServerPort}");
                LogInfo.WriteLog($"- Fail TestNetworkConnection to RabbitMQ => SendLaneEventRabbitMQ : IP = {TCT_Lib.szDC_RabbitMQ_ServerIPAddress}, PORT = {TCT_Lib.iDC_RabbitMQ_ServerPort}");
                return false;
            }

            try
            {
                if (connection == null)
                {
                    if (!ConnectMocRabbitMQ())
                        return false;
                }
                var properties          = channel.CreateBasicProperties();
                properties.Persistent   = false;

                // Bind Queue to Exchange
                channel.QueueBind(EventQueueName, EventExchanges, keyName);

                byte[] messagebuffer    = Encoding.Default.GetBytes(msg);
                channel.BasicPublish(exchange: EventExchanges,
                                        routingKey: keyName,
                                        basicProperties: null,
                                        body: messagebuffer);

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine($"- Error SendLaneEventRabbitMQ : {err.Message}");
                LogInfo.WriteLog($"- Error SendLaneEventRabbitMQ : {err.Message}");
                return false;
            }
        }

        public bool SendLaneTransactionRabbitMQ(string keyName, string msg)
        {
            if (!TCT_Lib.bEnableDcRabbitMQ)
                return false;

            if (!TCT_Lib.laneController.TestNetworkConnection(TCT_Lib.szDC_RabbitMQ_ServerIPAddress, TCT_Lib.iDC_RabbitMQ_ServerPort))
            {
                Console.WriteLine($"- Fail TestNetworkConnection to RabbitMQ => SendLaneTransactionRabbitMQ : IP = {TCT_Lib.szDC_RabbitMQ_ServerIPAddress}, PORT = {TCT_Lib.iDC_RabbitMQ_ServerPort}");
                LogInfo.WriteLog($"- Fail TestNetworkConnection to RabbitMQ => SendLaneTransactionRabbitMQ : IP = {TCT_Lib.szDC_RabbitMQ_ServerIPAddress}, PORT = {TCT_Lib.iDC_RabbitMQ_ServerPort}");
                return false;
            }

            try
            {
                if (connection == null)
                {
                    if (!ConnectMocRabbitMQ())
                        return false;
                }
                var properties          = channel.CreateBasicProperties();
                properties.Persistent   = false;

                // Bind Queue to Exchange
                channel.QueueBind(TransQueueName, TransExchanges, keyName);

                byte[] messagebuffer    = Encoding.Default.GetBytes(msg);
                channel.BasicPublish(exchange: TransExchanges,
                                        routingKey: keyName,
                                        basicProperties: null,
                                        body: messagebuffer);

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine($"- Error SendLaneEventRabbitMQ : {err.Message}");
                LogInfo.WriteLog($"- Error SendLaneEventRabbitMQ : {err.Message}");
                return false;
            }
        }
    }
}
