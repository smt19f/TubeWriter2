namespace TubeWriter2;

class TubeWriter : Form {
    private System.ComponentModel.IContainer components = null;
    public int dateCount = 1;
    public int formWidth = 400;
    public TextBox startBox;
    public EntryBox[,] entryBoxes;
    public CheckBox[] checkBoxes;
    private Label[,] labels;
    private Button addDateButton;
    private Button removeDateButton;
    private Button submitButton;
    private string[] labelStrings = { "Date (MM/DD):", "Dogs:", "Horses", "Birds:", "Doubles:" } ;
    private string[,] previousEntries = new string[10, 5];
    private bool[] previousChecks = new bool[10];
    private string previousStart = "";
    private Label startLabel;

    public TubeWriter()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(formWidth, 450);
        this.Text = "TubeWriter2.0";

        startBox = new TextBox();
        startBox.Size = new Size(80, 20);
        startBox.Location = new Point(Globals.STARTX, Globals.STARTY);
        startBox.Text = previousStart;
        this.Controls.Add(startBox);
        startBox.Show();

        startLabel = new Label();
        startLabel.Text = "Start Position:";
        startLabel.Location = new Point(Globals.STARTX, Globals.DATE1Y + (Globals.YBUFFER*3));
        startLabel.Size = new Size(110, 20);
        startLabel.Font = new Font("Calibri", 10);
        this.Controls.Add(startLabel);
        startLabel.Show();

        labels = new Label[dateCount, 5];
        for (int i = 0; i < dateCount; i++) {
            for (int j = 0; j < 5; j++) {
                labels[i, j] = new Label();
                labels[i, j].Text = labelStrings[j];
                labels[i, j].Location = new Point(Globals.DATE1X + (Globals.XBUFFER*i), Globals.DATE1Y + (Globals.YBUFFER*j));
                labels[i, j].Size = new Size(110, 20);
                labels[i, j].Font = new Font("Calibri", 10);
                this.Controls.Add(labels[i, j]);
                labels[i, j].Show();
            }
        }

        entryBoxes = new EntryBox[dateCount, 5];
        for (int i = 0; i < dateCount; i++) {
            for (int j = 0; j < 5; j++) {
                entryBoxes[i, j] = new EntryBox(j);
                entryBoxes[i, j].shouldRead = true;
                entryBoxes[i, j].Location = new Point(Globals.ENTRY1X + (Globals.XBUFFER*i), Globals.ENTRY1Y + (Globals.YBUFFER*j));
                entryBoxes[i, j].Size = new Size(100, 20);
                entryBoxes[i, j].Text = previousEntries[i, j];
                this.Controls.Add(entryBoxes[i, j]);
                entryBoxes[i, j].Show();
            }
        }

        checkBoxes = new CheckBox[dateCount];
        for (int i = 0; i < dateCount; i++) {
            checkBoxes[i] = new CheckBox();
            checkBoxes[i].Location = new Point(Globals.ENTRY1X + (Globals.XBUFFER*i), 340);
            checkBoxes[i].Size = new Size(100, 20);
            checkBoxes[i].Font = new Font("Calibri", 12);
            checkBoxes[i].Text = "Retests";
            checkBoxes[i].Checked = previousChecks[i];
            this.Controls.Add(checkBoxes[i]);
            checkBoxes[i].Show();
        }

        addDateButton = new Button();
        addDateButton.Location = new Point(50, 320);
        addDateButton.Size = new Size(80, 40);
        addDateButton.Text = "Add \nDate";
        addDateButton.Click += new EventHandler(AddDate);
        this.Controls.Add(addDateButton);
        addDateButton.Show();

        removeDateButton = new Button();
        removeDateButton.Location = new Point(50, 365);
        removeDateButton.Size = new Size(80, 40);
        removeDateButton.Text = "Remove\nDate";
        removeDateButton.Click += new EventHandler(RemoveDate);
        this.Controls.Add(removeDateButton);
        removeDateButton.Show();

        submitButton = new Button();
        submitButton.Location = new Point(50, 275);
        submitButton.Size = new Size(80, 40);
        submitButton.Text = "Submit\nTo Excel";
        submitButton.Click += new EventHandler(Extract);
        this.Controls.Add(submitButton);
        submitButton.Show();
    }

    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void AddDate(object sender, EventArgs e)
    {
        //Avoid overflow
        if (dateCount > 9) {
            return;
        }

        //Expand window and redraw
        formWidth += Globals.XBUFFER;
        StoreData();
        dateCount++;
        this.Controls.Clear();
        this.InitializeComponent();
    }

    private void RemoveDate(object sender, EventArgs e)
    {
        //Avoid underflow
        if (dateCount < 2) {
            return;
        }

        //Shrink window and redraw
        formWidth -= Globals.XBUFFER;
        StoreData();
        dateCount--;
        this.Controls.Clear();
        this.InitializeComponent();    
    }

    //Retrieves data from EntryBoxes and puts it in an excel sheet
    private void Extract(object sender, EventArgs e)
    {
        Extractor extractor = new Extractor(this);
    }

    //Retrieves data from EntryBoxes to store for redraw
    private void StoreData()
    {
        for (int i = 0; i < dateCount; i++) {

            for (int j = 0; j < 5; j++) {
                previousEntries[i,j] = entryBoxes[i,j].Text;
            }

            previousChecks[i] = checkBoxes[i].Checked;
            previousStart = startBox.Text;
        }

    }

}
