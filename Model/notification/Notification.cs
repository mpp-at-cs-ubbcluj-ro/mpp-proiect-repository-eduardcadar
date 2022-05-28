using System;

namespace Model.notification
{
    [Serializable]
    public class Notification
    {
        public NotificationType Type { get; set; }
        public Reservation Reservation { get; set; }

        public Notification() { }

        public override string ToString()
        {
            return "type: " + Type.ToString() + ", reservation: " + Reservation.ToString();
        }
    }
}
