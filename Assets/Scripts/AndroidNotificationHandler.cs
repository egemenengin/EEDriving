using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
    int LastNotification;
#if UNITY_ANDROID
    private const string ChannelId = "notification_channel";
   public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationCenter.CancelAllNotifications();
        //AndroidNotificationCenter.CancelScheduledNotification(LastNotification);
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged",
            Text = "Your energy has recharged. Come back to play again!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };
        LastNotification = AndroidNotificationCenter.SendNotification(notification, ChannelId);
        
    }
#endif
}
