using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1_Event1_
{
    public partial class Form1 : Form
    {
        List<Event> events = new List<Event>();
        List<Event> afterdel = new List<Event>();
        Functions f = new Functions();
        ListViewItem v;
        public Form1()
        {
            InitializeComponent();

            eventBtn.Visible = true;
            addBtn.Visible = false;
            deleteBtn.Visible = false;
            updataBtn.Visible = false;
            reminderBtn.Visible = false;
            displayBtn.Visible = false;
            closeBtn.Visible = false;

            PanelAdd.Visible = false;
            PanelDelete.Visible = false;
            PanelReminder.Visible = false;
            PanelFind.Visible = false;
            PanelUpdata.Visible = false;
            panel1.Visible = false;
            PanelDisplay.Visible = false;
            /*
                        

                       
                       
                        panel3.Visible = false;
                        panel4.Visible = false;

                       */


            f.fillList(events);
            f.FillDel(afterdel);
            f.Donecheck(events, afterdel);
        }
        int ii;
        bool r;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you want to Save?", "Closing", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

                case DialogResult.Yes:
                    f.WriteInFileFromDelList(afterdel);
                    f.WriteInFileFromList(events);
                    break;

                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PanelAdd.Visible = true;

            eventBtn.Visible = false;
            addBtn.Visible = false;
            deleteBtn.Visible = false;
            updataBtn.Visible = false;
            reminderBtn.Visible = false;
            displayBtn.Visible = false;
            closeBtn.Visible = false;

        }



        private void button4_Click(object sender, EventArgs e)
        {
            eventBtn.Visible = true;

            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (n.Text == "" || p.Text == "" || remfrouser.Text == "")
            {
                MessageBox.Show("Please fill  Empty fields !!!");
            }
            else
            {
                //adds the Name of the event.

                string name = n.Text;

                //adds the date of start.



                DateTime DateAndTimeOfStart = this.dateTimePicker1.Value.Date;

                DateTime DateAndTimeOfEnd = this.dateTimePicker2.Value.Date;
                DateTime reminderTime = this.dateTimePicker3.Value.Date;
                if (DateAndTimeOfStart < DateTime.Now && DateAndTimeOfEnd < DateTime.Now)
                {

                    //  Console.Write("Can not add an Event before today");
                    MessageBox.Show("Can not add an Event before today");

                }
                else
                {


                    if (DateAndTimeOfStart > DateAndTimeOfEnd)
                    {
                        MessageBox.Show("Can not add an Event with wrong time!!!");

                    }
                    else
                    {
                        if (reminderTime > DateTime.Now && reminderTime <= DateAndTimeOfStart)//reminder should beetween  (time of today and the time of end to the event)

                        {

                            //enters the palce of the event.

                            string place = p.Text;

                            //enters the reminder of the event.

                            string rems = remfrouser.Text;

                            reminder r = new reminder(rems, reminderTime);

                            bool done = false;

                            //checks if there is an event on the day of start.
                            bool same_exist = true;
                            for (int i = 0; i < events.Count; i++)
                            {
                                if ((DateAndTimeOfStart < events[i].dateandtimeofstart && DateAndTimeOfEnd < events[i].dateandtimeofstart) || (DateAndTimeOfStart > events[i].dateandtimeofend && DateAndTimeOfEnd > events[i].dateandtimeofend))
                                { }
                                else
                                {
                                    same_exist = false;
                                }
                            }

                            if (same_exist == true)
                            {
                                // try using WriteInFileFromList not write in file.
                                Event a = new Event(name, DateAndTimeOfStart, DateAndTimeOfEnd, place, r, done);
                                events.Add(a);
                                PanelAdd.Visible = false;
                                //  panel2.Visible = false;
                                eventBtn.Visible = true;
                                addBtn.Visible = true;
                                deleteBtn.Visible = true;
                                updataBtn.Visible = true;
                                reminderBtn.Visible = true;
                                displayBtn.Visible = true;
                                closeBtn.Visible = true;
                            }
                            else
                            {
                                // Console.WriteLine("You already have an event on that day, add another event with another day.");
                                MessageBox.Show("You already have An Event on that day !");
                            }
                        }else
                        {
                            MessageBox.Show(" Reminder time should be between Today and Event start time !");
                        }
                    }

                }
            }
        }



        private void button12_Click(object sender, EventArgs e)
        {
            PanelAdd.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;

        }

        private void closeBtn_Click(object sender, EventArgs e)
        { this.Close(); }


        private void deleteBtn_Click(object sender, EventArgs e)
        {
            PanelDelete.Visible = true;

            eventBtn.Visible = false;
            addBtn.Visible = false;
            deleteBtn.Visible = false;
            updataBtn.Visible = false;
            reminderBtn.Visible = false;
            displayBtn.Visible = false;
            closeBtn.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
                PanelDelete.Visible = false;
                addBtn.Visible = true;
                deleteBtn.Visible = true;
                updataBtn.Visible = true;


            DateTime find =this. dateTimePicker4.Value.Date;
                for (int i = 0; i < events.Count(); i++)
                {
                    if (events[i].dateandtimeofstart.Year == find.Year && events[i].dateandtimeofstart.Day == find.Day && events[i].dateandtimeofstart.Month == find.Month)
                    {
                        afterdel.Add(events[i]);
                        events.Remove(events[i]);
                    break;
                }
                    else if (i == events.Count() - 1)
                {
                    MessageBox.Show("There is no Event with this name .");
                }
                }
            PanelDelete.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;
        
            //panel1.Visible = false;
        }

           
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            PanelDelete.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;

            
        }

        private void reminderBtn_Click(object sender, EventArgs e)
        {
            PanelReminder.Visible = true;
            txtReminder.Text = f.showReminder(events);

            eventBtn.Visible = false;
            addBtn.Visible = false;
            deleteBtn.Visible = false;
            updataBtn.Visible = false;
            reminderBtn.Visible = false;
            displayBtn.Visible = false;
            closeBtn.Visible = false;


        }

        private void backReminder_Click(object sender, EventArgs e)
        {

            PanelReminder.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;
            txtReminder.Text = "";
        }

        private void updataBtn_Click(object sender, EventArgs e)
        {
            PanelFind.Visible = true;

            eventBtn.Visible = false;
            addBtn.Visible = false;
            deleteBtn.Visible = false;
            updataBtn.Visible = false;
            reminderBtn.Visible = false;
            displayBtn.Visible = false;
            closeBtn.Visible = false;

        }

        private void button16_Click(object sender, EventArgs e)
        {
           
                r = false;

               
                //adds the time of start.


                DateTime change = this.dateTimePicker8.Value.Date;
                for (int i = 0; i < events.Count; i++)
                {
                    if (events[i].dateandtimeofstart.Year == change.Year && events[i].dateandtimeofstart.Month == change.Month && events[i].dateandtimeofstart.Day == change.Day)
                    {
                        ii = i;
                        r = true;
                    }
                }


                if (r == true)
                {
                    PanelFind.Visible = false;
                    PanelUpdata.Visible = true;

                txtname.Text = events[ii].name;
                txtplace.Text = events[ii].place;
                txtrem.Text = events[ii].rem.remind;
                dateTimePicker5.Value = events[ii].dateandtimeofstart;
                dateTimePicker6.Value = events[ii].dateandtimeofend;
                dateTimePicker7.Value = events[ii].rem.reminderTime;
            }
                else
                {

                    MessageBox.Show("There is not an event with this time");
                   
                }
                /*  PanelFind.Visible = false;

                  eventBtn.Visible = true;
                  addBtn.Visible = true;
                  deleteBtn.Visible = true;
                  updataBtn.Visible = true;
                  reminderBtn.Visible = true;
                  displayBtn.Visible = true;
                  closeBtn.Visible = true;*/

            }
        

        private void PanelFind_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            PanelFind.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "" || txtplace.Text == "" || txtrem.Text == "")
            { MessageBox.Show("Please fill the empty field!!!"); }
            else
            {
                 //adds the Name of the event.

                    string name = txtname.Text;

                //adds the date of start.



                DateTime NEWdateofstart = this.dateTimePicker5.Value.Date;
              
                DateTime NEWdateofend = this.dateTimePicker6.Value.Date;
                DateTime reminderTime = this.dateTimePicker7.Value.Date;
                if (NEWdateofstart < DateTime.Now && NEWdateofend < DateTime.Now)
                    {

                        //  Console.Write("Can not add an Event before today");
                        MessageBox.Show("Can not add an Event before today");

                    }
                    else
                    {


                        if (NEWdateofstart > NEWdateofend)
                        {
                            MessageBox.Show("Can not add an event with wrong time!!!");

                        }
                        else
                        {
                            if (reminderTime > DateTime.Now && reminderTime <= NEWdateofstart)//reminder should beetween  (time of today and the time of end to the event)

                            {

                                //enters the palce of the event.

                                string place = txtplace.Text;

                                //enters the reminder of the event.

                                string rems = txtrem.Text;

                                reminder r = new reminder(rems, reminderTime);

                                bool done = false;

                                //checks if there is an event on the day of start.
                                bool same_exist = true;
                                for (int i = 0; i < events.Count; i++)
                            {
                                if (i != ii)
                                {
                                    if ((NEWdateofstart < events[i].dateandtimeofstart && NEWdateofend < events[i].dateandtimeofstart) || (NEWdateofstart > events[i].dateandtimeofend && NEWdateofend > events[i].dateandtimeofend))
                                    { }
                                    else
                                    {
                                        same_exist = false;
                                    }
                                }
                                }

                                if (same_exist == true)
                                {
                                    // try using WriteInFileFromList not write in file.
                                   
                                Event eee = new Event();
                                eee.name = txtname.Text;
                                eee.dateandtimeofstart = NEWdateofstart;
                               
                                eee.dateandtimeofend = NEWdateofend;    //wrong/////////////////////////////////////////////


                                eee.place = txtplace.Text;

                                eee.done = done;
                                eee.rem.remind = txtrem.Text;
                                eee.rem.reminderTime = this.dateTimePicker7.Value.Date;
                                events.Remove(events[ii]);
                                events.Add(eee);
                                PanelUpdata.Visible = false;

                                eventBtn.Visible = true;
                                addBtn.Visible = true;
                                deleteBtn.Visible = true;
                                updataBtn.Visible = true;
                                reminderBtn.Visible = true;
                                displayBtn.Visible = true;
                                closeBtn.Visible = true;
                            }
                                else
                                {
                                    // Console.WriteLine("You already have an event on that day, add another event with another day.");
                                    MessageBox.Show("You already have an event on that day !");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Reminder time should be between Today and Event start time !");
                            }
                        }

                    }
                





                /////////////////////////////
                


            }
           

        }

        private void button14_Click(object sender, EventArgs e)
        {

            PanelUpdata.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;
        }

        private void displayBtn_Click(object sender, EventArgs e)
        {
            PanelDisplay.Visible = true;

            eventBtn.Visible = false;
            addBtn.Visible = false;
            deleteBtn.Visible = false;
            updataBtn.Visible = false;
            reminderBtn.Visible = false;
            displayBtn.Visible = false;
            closeBtn.Visible = false;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            panel1.Visible = false;
            displayBtn_Click(sender, e);
            listView1.Items.Clear();

        }

        private void back_display_Click(object sender, EventArgs e)
        {
            PanelDisplay.Visible = false;

            eventBtn.Visible = true;
            addBtn.Visible = true;
            deleteBtn.Visible = true;
            updataBtn.Visible = true;
            reminderBtn.Visible = true;
            displayBtn.Visible = true;
            closeBtn.Visible = true;
        }

        private void display_del_event_Click(object sender, EventArgs e)
        {
            PanelDisplay.Visible = false;
            if (afterdel.Count == 0)
            {
                MessageBox.Show("There is no Data!!");
                eventBtn.Visible = true;
                addBtn.Visible = true;
                deleteBtn.Visible = true;
                updataBtn.Visible = true;
                reminderBtn.Visible = true;
                displayBtn.Visible = true;
                closeBtn.Visible = true;

            }
            else
            {
                
                panel1.Visible = true;
                Event swap = new Event();
                for (int i = 0; i < afterdel.Count; i++)
                {
                    for (int j = 0; j < (afterdel.Count - 1); j++)
                    {
                        if (afterdel[j].dateandtimeofstart > afterdel[j + 1].dateandtimeofstart)
                        {
                            swap = afterdel[j];
                            afterdel[j] = afterdel[j + 1];
                            afterdel[j + 1] = swap;
                        }
                    }
                }

                for (int i = 0; i < afterdel.Count; i++)
                {
                    v = new ListViewItem(afterdel[i].name);

                    v.SubItems.Add(afterdel[i].dateandtimeofstart.ToString());
                    v.SubItems.Add(afterdel[i].place);
                    v.SubItems.Add(afterdel[i].dateandtimeofend.ToString());
                    v.SubItems.Add(afterdel[i].rem.remind);
                    v.SubItems.Add(afterdel[i].rem.reminderTime.ToString());
                    string don;
                    if (afterdel[i].done == true)
                    {
                        don = "Yes";
                    }
                    else
                    {
                        don = "No";
                    }
                    v.SubItems.Add(don);
                    listView1.Items.Add(v);
                }
            }
        }
        private void display_event_Click(object sender, EventArgs e)
        {
            PanelDisplay.Visible = false;
            if (events.Count == 0)
            {
                MessageBox.Show("There is no data!!");
                eventBtn.Visible = true;
                addBtn.Visible = true;
                deleteBtn.Visible = true;
                updataBtn.Visible = true;
                reminderBtn.Visible = true;
                displayBtn.Visible = true;
                closeBtn.Visible = true;

            }
            else
            {

                panel1.Visible = true;
                Event swap = new Event();
                for (int i = 0; i < events.Count; i++)
                {
                    for (int j = 0; j < (events.Count - 1); j++)
                    {
                        if (events[j].dateandtimeofstart > events[j + 1].dateandtimeofstart)
                        {
                            swap = events[j];
                            events[j] = events[j + 1];
                            events[j + 1] = swap;
                        }
                    }
                }

                for (int i = 0; i < events.Count; i++)
                {
                    v = new ListViewItem(events[i].name);

                    v.SubItems.Add(events[i].dateandtimeofstart.ToString());
                    v.SubItems.Add(events[i].place);
                    v.SubItems.Add(events[i].dateandtimeofend.ToString());
                    v.SubItems.Add(events[i].rem.remind);
                    v.SubItems.Add(events[i].rem.reminderTime.ToString());
                    string don;
                    if (events[i].done == true)
                    {
                        don = "Yes";
                    }
                    else
                    {
                        don = "No";
                    }
                    v.SubItems.Add(don);
                    listView1.Items.Add(v);
                }
            }

        }
    }
}
