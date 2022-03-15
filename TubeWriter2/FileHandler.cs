using NPOI.SS.UserModel;

namespace TubeWriter2;

class FileHandler {

    public FileHandler(IWorkbook workbook)
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string rscPath = System.IO.Path.Combine(currentDirectory, @"..\..\..\rsc");
        Directory.SetCurrentDirectory(rscPath);

        try {

            FileInfo fi = new FileInfo("Tubes.xlsx");
            if (fi.Exists) {
                System.IO.File.Delete("Tubes.xlsx");   
            }
        
            using (FileStream stream = new FileStream("Tubes.xlsx", FileMode.Create, FileAccess.Write)) {
                workbook.Write(stream);
            }
            
            System.Diagnostics.Process.Start("OpenFile.bat");
        }
        catch (System.IO.IOException) {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult message = MessageBox.Show("Please close the \"Tubes\" excel file", "Error", buttons);
        }
    }
}