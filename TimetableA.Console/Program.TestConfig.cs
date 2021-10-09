using System;

namespace TimetableA.ConsoleImporter
{

    partial class Program
    {
        public class TestConfig : IConfig
        {
            public Uri Source =>
                new(@"webcals://usosapps.umk.pl/services/tt/upcoming_ical?lang=pl&user_id=267050&key=u7LkjKgsdaHK3W5mJaqK");
            //new(@"C:\Users\MikołajMakowski(3081\Desktop\u267050.ics");

            public Uri Dest => new(@"https://timetablea-api.azurewebsites.net");

            public Uri StaticApp => new(@"https://timetableappstatic.z22.web.core.windows.net");

            public int Cycles => 1;
        }
    }
}
