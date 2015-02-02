using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;

namespace GetLinks
{
    public partial class Form1 : Form
    {
        private uint maxDepth = 0;
        Thread anThread;
        Boolean running = false;
        delegate void AddItem(SiteItem item);
        delegate void Act();
        delegate void TextWriter(String str);
        Act stop;
        TextWriter setState;
        Stack<SiteItem> uriBuf = new Stack<SiteItem>();
        List<SiteItem> uriRes = new List<SiteItem>();       //result ^.^
        AddItem addItem;
        WebClientTimeouted web;
        String cache;
        const String badURI = "<Bad URL!>";
        
        public Form1()
        {
            InitializeComponent();
            ResizeForm(this, null);
            addItem = new AddItem(AddItemToList);
            stop = new Act(Stop);
            web = new WebClientTimeouted();
            setState = new TextWriter(SetStatus);
            //web.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            web.Timeout = 5;        //5 seconds
            web.Encoding = Encoding.UTF8;
        }

        private void ResizeForm(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(this.lblMaxDepth.Width + this.tbMaxDepth.Width + this.btnSaveXML.Width * 3 + 50,
                this.btnSaveXML.Height * 5 + 50);
            int left = 12;
            this.lblRootSite.Left = left;
            this.lbSites.Left = left;
            this.lblMaxDepth.Left = left;
            this.lblState.Left = left;

            this.tbRootSite.Left = this.lblRootSite.Width + left;
            this.lblStatus.Left = this.lblMaxDepth.Width + left;
            this.tbMaxDepth.Left = this.lblMaxDepth.Width + left;

            this.tbRootSite.Width = this.Width - this.tbRootSite.Left - 20;
            this.lbSites.Width = this.Width - this.lbSites.Left - 20;
            this.lblStatus.Width = this.Width - this.lblStatus.Left - 20;

            this.lbSites.Height = this.ClientRectangle.Height - this.lbSites.Top - this.btnSaveXML.Height -this.lblStatus.Height - 20;
            this.lblStatus.Top = this.lbSites.Bottom + 5;
            this.lblState.Top = this.lbSites.Bottom + 5;
            this.lblMaxDepth.Top = this.lblStatus.Bottom + 5;
            this.tbMaxDepth.Top = this.lblStatus.Bottom + 5;
            this.btnSaveXML.Top = this.lblStatus.Bottom + 5;
            this.btnAnalyze.Top = this.lblStatus.Bottom + 5;
            this.btnExit.Top = this.lblStatus.Bottom + 5;

            this.btnExit.Left = this.lbSites.Right - this.btnExit.Width;
            this.btnAnalyze.Left = this.btnExit.Left - this.btnAnalyze.Width - 5;
            this.btnSaveXML.Left = this.btnAnalyze.Left - this.btnSaveXML.Width - 5;
        }

        private void SetStatus(String str)
        {
            this.lblStatus.Text = str;
        }

        private void Stop()
        {
            this.btnAnalyze.Text = "Analyze";
            running = false;
            uriBuf.Clear();     //cleanup
            //uriRes.Clear();
            lblStatus.Invoke(setState, "Ready");
            //web.CancelRequest = true;
        }

        private void AddItemToList(SiteItem item)       // add Item!
        {
            String resStr = new String('-', item.Depth * 2);
            resStr += " " + item.Title;
            resStr += " (" + item.URL + ")";
            this.lbSites.Items.Add(resStr);
            this.lbSites.SelectedIndex = this.lbSites.Items.Count - 1;      // PseudoAutoScroll...
            this.lbSites.SelectedIndex = -1;
        }

        private void tbMaxDepth_Leave(object sender, EventArgs e)   //check for correct data
        {
            if (!UInt32.TryParse(this.tbMaxDepth.Text, out maxDepth))
            {
                MessageBox.Show("Bad \"Max Depth\" value: integer expected.");
                this.tbMaxDepth.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)      //exit
        {
            if (anThread != null && anThread.ThreadState == ThreadState.Running)
                anThread.Abort();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)      //save as XML
        {
            if (this.lbSites.Items.Count == 0)
            {
                MessageBox.Show("No items in list. Nothing will be saved.");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML files|*.xml|All files|*.*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlWorker.Save(this.uriRes, sfd.FileName);
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)       // Analyze start/stop
        {
            if (!running)       //run it!
            {
                if (!(this.tbRootSite.Text.Contains("http://") || this.tbRootSite.Text.Contains("https://")))
                    this.tbRootSite.Text="http://"+this.tbRootSite.Text;
                if (!CorrectURL(this.tbRootSite.Text))
                    return;
                running = true;
                this.lbSites.Items.Clear();
                this.uriRes.Clear();
                this.uriBuf.Clear();

                uriBuf.Push(new SiteItem() { Depth = 0, Title = StringWorker.GetTitle(this.tbRootSite.Text), URL = this.tbRootSite.Text });
                this.maxDepth = UInt32.Parse(this.tbMaxDepth.Text);

                anThread = new Thread(new ThreadStart(Run));
                anThread.Start();
                this.btnAnalyze.Text = "Stop";
            }
            else
            {
                this.btnAnalyze.Text = "Analyze";
                running = false;
            }
        }

        private void Run()      //main loop
        {
            
            //this.lbSites.Invoke(addItem, new SiteItem() { Depth = n, Title = "SomeTitle" + n.ToString(), URL = "http://url" + n.ToString() + ".com" });
            SiteItem curItem = new SiteItem();
            while (uriBuf.Count > 0)
            {
                if (!running)
                    break;
                curItem = uriBuf.Pop();
                //if (curItem.Depth > maxDepth)
                //    continue;
                try
                {
                    lblStatus.Invoke(setState, "Getting data from " + curItem.URL);
                    cache = web.DownloadString(curItem.URL);
                    /* enc = StringWorker.GetEncoding(cache);
                     if (enc != web.Encoding)
                     {
                         web.Encoding = enc;
                         cache = cache = web.DownloadString(curItem.URL);        //re-download?!
                     }*/
                }
                catch (WebException e)
                {
                    if ((from u in uriRes where u.URL.Substring(u.URL.LastIndexOf('/')).Equals(curItem.URL.Substring(curItem.URL.LastIndexOf('/'))) select u).Count() < 1)
                    {
                        curItem.Title = "[" + e.Message + "]";
                        this.uriRes.Add(curItem);               //add to list
                        this.lbSites.Invoke(addItem, curItem);  //..and to listbox control
                    }
                    continue;
                }
                catch (Exception e)
                {
                    continue;
                }

                lblStatus.Invoke(setState, "Processing " + curItem.URL);
                
                curItem.Title = StringWorker.GetTitle(cache);
                FSWorker.SaveCache(cache, curItem.URL.Substring(curItem.URL.IndexOf("//") + 1));       ////remove!!!

                if ((from u in uriRes where u.URL.Substring(u.URL.LastIndexOf('/')).Equals(curItem.URL.Substring(curItem.URL.LastIndexOf('/'))) select u).Count() < 1)
                {
                    this.uriRes.Add(curItem);               //add to list
                    this.lbSites.Invoke(addItem, curItem);  //..and to listbox control
                }
                else continue;

                if (curItem.Depth >= maxDepth)      // better is ==, but don't exists excess checkings
                    continue;

                foreach (var curUri in StringWorker.GetLinks(cache))        // get more links (level up)
                {
                    if (((from u in uriRes where u.URL.Substring(u.URL.LastIndexOf('/')).Equals(curUri.Substring(curUri.LastIndexOf('/'))) select u).Count() < 1))// &&     //this element was not...+ ho http\https
                        this.uriBuf.Push(new SiteItem() { Depth = curItem.Depth + 1, URL = curUri, Parent = curItem });
                }
                if (!running) break;
            }
            MessageBox.Show("Site found: " + uriRes.Count, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.btnAnalyze.Invoke(stop);
        }

        private bool CorrectURL(String url){
            String res = String.Empty;
            try
            {
                web.OpenRead(url).Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot connect to "+url+". Check URL (or connection) and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.btnAnalyze.Invoke(stop);
                return false;
            }
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FSWorker.ClearCache();      // cleanup
        }
    }
}