namespace MVC.G02.PL.Helper
{
    public static class DocumentSetting
    {
        //1.Upload
        public static string UploadFile(IFormFile file,string folderName) 
        {
            //1.Get Location Folder path
            //string folderPath = $"C:\\Users\\METRO\\Desktop\\C#basic Route\\MVC.G02.PL Solution\\MVC.G02.PL\\wwwroot\files\\{folderName}";
            //string folderPath= Directory.GetCurrentDirectory()+@"wwwroot\files"+folderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            //2. GetFileName Make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3.GEt file path ->FolderPath +FileName
            string filepath=Path.Combine(folderPath, fileName);
            //4. Save file as a Stream : Data per Time
           using var fileStream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;


        }
        //2.Delete
        public static void DeleteFile(string fileName, string folderName) 
        { 
            string filepath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files", folderName, fileName);
            if (File.Exists(filepath)) 
            {
                File.Delete(filepath);
            }
             
        }
    }
}
