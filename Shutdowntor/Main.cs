using Microsoft.Win32;
using Shutdowntor.Command;
using Shutdowntor.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Shutdowntor
{
    enum ActionFlag
    {
        Shutdown,
        Reboot
    }

    public partial class Main : Form
    {
        ICommand command;

        bool running = true;
        TimeSpan remainTime;
        DateTime targetDateTime;
        bool start = false;
        bool visited = true;
        bool actionFired;
        int tempInt = 0;

        bool autoStart, autoSetDateTime;

        private string action;
        public Main(bool autoStart, string action, DateTime targetDateTime, bool autoSetDateTime)
        {
            InitializeComponent();
            this.autoStart = autoStart;
            this.action = GetAction(action);
            this.targetDateTime = targetDateTime;
            this.autoSetDateTime = autoSetDateTime;
            Init();
        }

        private void Init()
        {
            string[] ActionFlagArray = Enum.GetValues(typeof(ActionFlag)).Cast<ActionFlag>().Select(i => i.ToString()).ToArray();
            this.actionOptiontoolStripComboBox.Items.AddRange(ActionFlagArray);
            actionOptiontoolStripComboBox.SelectedItem = this.action;
            actionOptiontoolStripComboBox.SelectedIndexChanged += ActionOptiontoolStripComboBox_SelectedIndexChanged;
            timer.Tick += Timer_Tick;
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
            dateTimePicker_date.MinDate = DateTime.Now;
            InitUI();
        }

        private void ActionOptiontoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.action = actionOptiontoolStripComboBox.SelectedItem.ToString();
        }

        public string GetAction(string s)
        {
            return ActionParser.ToActionName(s);
        }

        private void InitUI()
        {
            dateTimePicker_date.Left = (panelMain.Width - dateTimePicker_date.Width) / 2;
            dateTimePicker_time.Left = (panelMain.Width - dateTimePicker_time.Width) / 2;
            labelRemainTime.Left = (panelMain.Width - labelRemainTime.Width) / 2;
            this.Text = Global.ApplicationNameNVer;
            notifyIcon1.Text = this.Text;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (ArgsReader.Instance.GetArg<bool>(ArgsReader.ArgOption.hide))
            {
                visited = false;
                this.ShowInTaskbar = false;
                this.Hide();
            }

            WriteLog($"Main_Load {Global.APP_VER}");

            if (autoSetDateTime)
            {
                if (targetDateTime > dateTimePicker_date.MinDate)
                {
                    dateTimePicker_date.Value = targetDateTime;
                    dateTimePicker_time.Value = targetDateTime;
                }
            }

            if (autoStart)
            {
                if (!autoSetDateTime)
                {
                    dateTimePicker_date.Value = DateTime.Now.AddDays(1);
                }
                button_start_Click(sender, e);
            }

            WriteLog($"Main_Load END\tAuto:{autoStart}\tAutoDateTime:{autoSetDateTime}");
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            WriteLog("button_start_Click");
            targetDateTime = new DateTime(dateTimePicker_date.Value.Year, dateTimePicker_date.Value.Month, dateTimePicker_date.Value.Day, dateTimePicker_time.Value.Hour, dateTimePicker_time.Value.Minute, dateTimePicker_time.Value.Second);

            remainTime = ScheduleClock.Remaining(DateTime.Now, targetDateTime);

            if (remainTime.TotalSeconds <= 0)
            {
                MessageBox.Show("Invalid time", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WriteLog("button_start_Click : Invalid time");
            }
            else
            {
                if (!start)
                {
                    actionFired = false;
                    running = true;
                    timer.Start();
                    this.BackColor = Color.FromArgb(0, 0, 0);
                    this.ForeColor = Color.FromArgb(255, 255, 255);
                    button_start.ForeColor = Color.FromArgb(255, 255, 255);
                    button_start.BackColor = Color.FromArgb(119, 246, 0);
                    actionOptiontoolStripComboBox.Enabled = false;
                    button_start.Text = "STOP";
                    WriteLog("timer.Start(\n\ttargetDateTime = " + targetDateTime.ToString("yyyy/MM/dd HH:mm:ss") + $"\n\tremainTime.TotalSeconds = {remainTime.TotalSeconds})");
                }
                else
                {
                    timer.Stop();
                    tempInt = 0;
                    notifyIcon1.Text = Global.ApplicationNameNVer;
                    this.BackColor = Color.FromArgb(47, 161, 231);
                    this.ForeColor = Color.FromArgb(0, 0, 0);
                    button_start.ForeColor = Color.FromArgb(0, 0, 0);
                    button_start.BackColor = Color.FromArgb(255, 0, 0);
                    actionOptiontoolStripComboBox.Enabled = true;
                    button_start.Text = "START";
                    labelRemainTime.Text = "--.--:--:--";
                    labelRemainTime.Left = (panelMain.Width - labelRemainTime.Width) / 2;
                    GC.Collect();
                    WriteLog("timer.Stop()");
                }
                dateTimePicker_date.Enabled = start;
                dateTimePicker_time.Enabled = start;
                start = !start;
                timer.Enabled = start;
            }
        }

        private void DoAction()
        {
            ActionFlag actionFlag = (ActionFlag)Enum.Parse(typeof(ActionFlag), action);
            WriteLog($"DoAction (flags={actionFlag})");
            command = (ICommand)Global.GetInstanceByClassName($"Shutdowntor.Command.{actionFlag}Command");
            command.Execute();
            WriteLog($"DoAction (flags={actionFlag}) END");
        }

        private void FinalTry()
        {
            WriteLog("Run bat");
            command = new BatShutdownCommand();
            command.Execute();
            WriteLog("Run bat END");
        }

        private void TimerStopEvent()
        {
            if (actionFired)
            {
                return;
            }
            actionFired = true;
            running = false;
            timer.Enabled = false;
            timer.Stop();
            try
            {
                DoAction();
                WriteLog($" ---------{action}--------- END");
            }
            catch
            {
                FinalTry();
                WriteLog($" ---------{action}--------- ERROR");
            }
        }

        private void EvaluateDeadline(string reason)
        {
            if (!start || actionFired)
            {
                return;
            }

            var now = DateTime.Now;
            remainTime = ScheduleClock.Remaining(now, targetDateTime);
            WriteLog($"EvaluateDeadline ({reason}) now={now:O} target={targetDateTime:O} remaining={remainTime.TotalSeconds}");

            if (ScheduleClock.HasReached(now, targetDateTime))
            {
                TimerStopEvent();
                return;
            }

            UpdateRemainingLabel();
        }

        private void UpdateRemainingLabel()
        {
            if (labelRemainTime.IsDisposed)
            {
                return;
            }

            void apply()
            {
                string remainTimeStr = remainTime.ToString(@"dd\.hh\:mm\:ss");
                labelRemainTime.Text = remainTimeStr;
                notifyIcon1.Text = remainTimeStr;
                if (tempInt == 1)
                    labelRemainTime.Left = (panelMain.Width - labelRemainTime.Width) / 2;
            }

            if (labelRemainTime.InvokeRequired)
            {
                labelRemainTime.Invoke((Action)apply);
            }
            else
            {
                apply();
            }
        }

        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            WriteLog($"PowerModeChanged {e.Mode}");
            if (e.Mode == PowerModes.Resume)
            {
                if (IsHandleCreated)
                {
                    BeginInvoke(new Action(() => EvaluateDeadline("resume")));
                }
                else
                {
                    EvaluateDeadline("resume");
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (tempInt <= 0)
                tempInt++;
            if (!running)
            {
                timer.Enabled = false;
                WriteLog("timer.Enabled = false");
                return;
            }

            EvaluateDeadline("tick");
        }

        private void ToggleForm()
        {
            if (visited)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.TopMost = true;
                this.TopMost = false;
            }
            GC.Collect();
            visited = !visited;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            ToggleForm();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemEvents.PowerModeChanged -= OnPowerModeChanged;
            if (!running)
            {
                WriteLog("From Closed by non-running");
                return;
            }
            if (start)
            {
                if (MessageBox.Show("Did you confirm stop shutdown counter and quit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    WriteLog("Timer STOPED. From Closed by click button and MessageBox");
                }
                else
                {
                    e.Cancel = true;
                    SystemEvents.PowerModeChanged += OnPowerModeChanged;
                }
            }
            WriteLog("From Closed by click button");
        }

        private void WriteLog(string s)
        {
            Debuger.DebugLoger.Instance.WriteLog(s);
        }
    }
}
