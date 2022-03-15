namespace TubeWriter2;

static class Globals {

    //Buffers to seperate winform components
    public const int XBUFFER = 120;
    public const int YBUFFER = 60;


    //Starting position for dates
    public const int DATE1X = 160;
    public const int DATE1Y = 40;
    

    //Starting position for entryBoxes
    public const int ENTRY1X = DATE1X;
    public const int ENTRY1Y = DATE1Y + 20;

    //Starting position for startPoistion textbox
    public const int STARTX = 50;
    public const int STARTY = ENTRY1Y + (YBUFFER * 3);
}