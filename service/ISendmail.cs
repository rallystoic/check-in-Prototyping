using System;
using System.IO;
using System.Text.Json;
using QC.models;
namespace QC.services
{
    public interface IJsonhandler {
        // return attendee jsonformat
        string HandleAttendee(Attendee attendee);
        // desialize string to object
        Attendee DesializeAttendee(string jsonstring);
    }
    public class Jsonhandler: IJsonhandler {

        public Jsonhandler(){

        }

        public string HandleAttendee(Attendee attendee){
            return JsonSerializer.Serialize<Attendee>(attendee);
        }
        public Attendee DesializeAttendee(string jsonstring){
            return JsonSerializer.Deserialize<Attendee>(jsonstring);
        }



    }
}
