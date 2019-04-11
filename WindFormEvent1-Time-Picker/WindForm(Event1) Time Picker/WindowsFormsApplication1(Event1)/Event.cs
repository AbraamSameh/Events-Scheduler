using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1_Event1_
{
    class reminder
    {
        public string remind;
        public DateTime reminderTime;

        public reminder()
        {
            remind = "";
        }

        public reminder(string a, DateTime b)
        {
            remind = a;
            reminderTime = b;
        }
    }
    class Event
    {
        public string name;
        public DateTime dateandtimeofstart;
        public DateTime dateandtimeofend;
        public string place;
        public reminder rem = new reminder();
        public bool done;

        public Event()
        {
            name = "";
            place = "";
            rem.remind = "";
            done = false;
        }

        public Event(string Name, DateTime DateAndTimeDfStart, DateTime DateAndTimeDfEnd, string Place, reminder r, bool d)
        {
            name = Name;
            dateandtimeofstart = DateAndTimeDfStart;
            dateandtimeofend = DateAndTimeDfEnd;
            place = Place;
            rem.remind = r.remind;
            rem.reminderTime = r.reminderTime;
            done = d;
        }

    }
    class Functions
    {

        //fill the list
        public void fillList(List<Event> E)
        {
            if (!File.Exists("text.txt"))
            {
                FileStream fs = new FileStream("text.txt", FileMode.Create);

            }
            else
            {
                FileStream fs = new FileStream("text.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                while (sr.Peek() != -1)
                {
                    string rec = sr.ReadLine();
                    string[] feilds = rec.Split('*');

                    reminder z = new reminder(feilds[4], DateTime.Parse(feilds[5]));

                    Event e = new Event(feilds[0], DateTime.Parse(feilds[1]), DateTime.Parse(feilds[2]), feilds[3], z, bool.Parse(feilds[6]));
                    E.Add(e);
                }
                sr.Close();
            }
        }//done


        //fill the del event list
        public void FillDel(List<Event> E)
        {
            if (!File.Exists("del.txt"))
            {

            }
            else
            {
                FileStream fs = new FileStream("del.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                while (sr.Peek() != -1)
                {
                    string rec = sr.ReadLine();
                    string[] feilds = rec.Split('*');

                    reminder z = new reminder(feilds[4], DateTime.Parse(feilds[5]));

                    Event e = new Event(feilds[0], DateTime.Parse(feilds[1]), DateTime.Parse(feilds[2]), feilds[3], z, bool.Parse(feilds[6]));
                    E.Add(e);
                }
                sr.Close();
            }
        }

        // After adding all events , write in file using the list not in use currently
        public void WriteInFileFromList(List<Event> a)
        {
            FileStream fs = new FileStream("text.txt", FileMode.Create);
            StreamWriter sr = new StreamWriter(fs);

            for (int i = 0; i < a.Count(); i++)
            {
                sr.WriteLine(a[i].name + '*' + a[i].dateandtimeofstart + '*' + a[i].dateandtimeofend + '*' + a[i].place + '*' + a[i].rem.remind + '*' + a[i].rem.reminderTime + "*" + a[i].done);
            }
            sr.Close();
        }

        // After adding all events , write in file using the del list not in use currently
        public void WriteInFileFromDelList(List<Event> a)
        {
            FileStream fs = new FileStream("del.txt", FileMode.Create);
            StreamWriter sr = new StreamWriter(fs);

            for (int i = 0; i < a.Count(); i++)
            {
                sr.WriteLine(a[i].name + '*' + a[i].dateandtimeofstart + '*' + a[i].dateandtimeofend + '*' + a[i].place + '*' + a[i].rem.remind + '*' + a[i].rem.reminderTime + "*" + a[i].done);
            }
            sr.Close();
        }
        //check if done or not
        public void Donecheck(List<Event> events, List<Event> afterdel)
        {
            for (int i = 0; i < events.Count(); i++)
            {
                if (events[i].dateandtimeofend < DateTime.Now)
                {
                    events[i].done = true;
                    afterdel.Add(events[i]);
                    events.Remove(events[i]);
                }
            }
        }

        //del event
        public void deleteEvent(List<Event> events, List<Event> del, DateTime find)
        {
            for (int i = 0; i < events.Count(); i++)
            {
                if (events[i].dateandtimeofstart.Year == find.Year && events[i].dateandtimeofstart.Day == find.Day && events[i].dateandtimeofstart.Month == find.Month)
                {
                    del.Add(events[i]);
                    events.Remove(events[i]);
                }
            }
        }

        //reminder viewer
        public string showReminder(List<Event> events)
        {
            string h = "No event tomorrow";
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].rem.reminderTime.Year == DateTime.Now.Year && events[i].rem.reminderTime.Month == DateTime.Now.Month && events[i].rem.reminderTime.Day == DateTime.Now.Day)
                {
                    h =  "You have an Event at : " + events[i].dateandtimeofstart + " Your Reminder Message is : " + events[i].rem.remind;
                }
            }

            return h;
        }



    }
}
