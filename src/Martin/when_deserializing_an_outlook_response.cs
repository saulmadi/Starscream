using System.Collections.Generic;
using Newtonsoft.Json;

namespace Martin
{
    public class SyncProcessPayloadConverter
    {
        public RootObject ConvertFromJson(string jsonPayload)
        {
            return JsonConvert.DeserializeObject<RootObject>(jsonPayload);
        }
    }

    public class RootObject
    {
        public string Enabled { get; set; }
        public List<Calendar> Calendars { get; set; }
    }

    public class Calendar
    {
        public string Name { get; set; }
        public string FolderId { get; set; }
        public string EntryId { get; set; }
        public string StoreId { get; set; }
        public List<Sync> Sync { get; set; }
        public Queue Queue { get; set; }
        public Prefs Prefs { get; set; }
    }

    public class Prefs
    {
        public string Sync_Interval { get; set; }
        public string Active_Sync { get; set; }
        public string Sync_Delta_Min { get; set; }
        public string Sync_Delta_Max { get; set; }
    }

    public class Sync
    {
        public string St { get; set; }
        public string Et { get; set; }
        public string Ck { get; set; }
    }

    public class Queue
    {
        public List<QueueAction> Create { get; set; }
        public List<QueueAction> Delete { get; set; }
        public List<QueueAction> Update { get; set; }
    }

    public class QueueAction
    {
        public List<string> Recipients { get; set; }
        public int Id { get; set; }
        public string Ac_Id { get; set; }
        public string Start { get; set; }
        public string EndTime { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string StoreId { get; set; }
        public string EntryId { get; set; }
        public string Location { get; set; }
        public Properties Properties { get; set; }

    }

    public class Properties
    {
        public string Start_Buffer { get; set; }
        public string End_Buffer { get; set; }
        public string Appt_Id { get; set; }
    }
}