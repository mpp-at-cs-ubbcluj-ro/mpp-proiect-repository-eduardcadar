using Apache.NMS;
using Model;
using Model.notification;
using Services.ams;
using System;

namespace Server.server
{
    public class NotificationServiceImpl : INotificationService
    {
        private readonly Uri _connectUri = new("activemq:tcp://127.0.0.1:61616");
        private readonly IConnectionFactory _connectionFactory;

        public NotificationServiceImpl()
        {
            _connectionFactory = new NMSConnectionFactory(_connectUri);
        }

        public void NewReservation(Reservation reservation)
        {
            using IConnection connection = _connectionFactory.CreateConnection();
            using ISession session = connection.CreateSession();
            
            IDestination destination = session.GetTopic("res.topic");
            using IMessageProducer producer = session.CreateProducer(destination);
            connection.Start();
            producer.DeliveryMode = MsgDeliveryMode.Persistent;
            Notification notif = new()
            {
                Type = NotificationType.NEW_RESERVATION,
                Reservation = reservation
            };
            IObjectMessage message = session.CreateObjectMessage(notif);
            producer.Send(message);
        }
    }
}
