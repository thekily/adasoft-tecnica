using System.IO;
using System.Reflection;
using ClosedXML.Excel;

// See https://aka.ms/new-console-template for more information

string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
string directoryPathProcessed = Path.Combine(Directory.GetCurrentDirectory(), "data-xlsx");

// Crear el observador del sistema de archivos
FileSystemWatcher watcher = new FileSystemWatcher(directoryPath);

// Configurar el observador para que detecte la creación de nuevos archivos
watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
watcher.Filter = "*.*";
watcher.Created += OnFileCreated;

// Iniciar la observación
watcher.EnableRaisingEvents = true;

// Esperar a que se presione una tecla para salir
Console.WriteLine($"Observing the folder {directoryPath}, press any key to exit");

Console.Read();

void OnFileCreated(object sender, FileSystemEventArgs e)
{
  // Procesar el archivo recién creado
  string filePath = e.FullPath;
  Console.WriteLine($"New file added to folder: {filePath}");
  // Aquí se podría llamar a una función para procesar el archivo
  ProcessFile(filePath);

}

void ProcessFile(string filePath)
{
  if (File.Exists(filePath))
  {
    string filename = filePath.Split('/').Last();
    string newFilePath = Path.Combine(directoryPathProcessed, $"{filename}.xlsx");


    ConvertTextToXlsx(filePath, newFilePath);
  }
}

void ConvertTextToXlsx(string textFilePath, string xlsxFilePath)
{
  using (StreamReader reader = new StreamReader(textFilePath))
  {
    using (XLWorkbook workbook = new XLWorkbook())
    {
      var worksheet = workbook.Worksheets.Add("Data");

      string line;
      int rowNumber = 1;

      while ((line = reader.ReadLine()) != null)
      {
        ProcessLine(worksheet, line, rowNumber);

        // Avanzar a la siguiente fila
        rowNumber++;
      }

      // Guardar el libro de trabajo como un archivo XLSX
      workbook.SaveAs(xlsxFilePath);
      Console.WriteLine($"File processed: {xlsxFilePath}");
    }
  }
}

void ProcessLine(IXLWorksheet worksheet, string line, int rowNumber)
{
  // Identificar tipo de linea
  LineType lineType = IdentifyLineType(line);

  switch (lineType)
  {
    case LineType.Type0:
      ProcessType0(worksheet, line, rowNumber);
      break;
    case LineType.Type1:
      ProcessType1(worksheet, line, rowNumber);
      break;
    case LineType.Type9:
      ProcessType9(worksheet, line, rowNumber);
      break;
    case LineType.TypeC:
      ProcessTypeC(worksheet, line, rowNumber);
      break;
    case LineType.Unknown:
      ProcessUnknown(worksheet, line, rowNumber);
      break;
  }

}

void ProcessUnknown(IXLWorksheet worksheet, string line, int rowNumber)
{
  worksheet.Cell(rowNumber, 1).Value = "UNKNOWN";
  worksheet.Cell(rowNumber, 2).Value = line;
}

void ProcessTypeC(IXLWorksheet worksheet, string line, int rowNumber)
{
  worksheet.Cell(rowNumber, 1).Value = "type C";
  worksheet.Cell(rowNumber, 2).Value = line;
}

void ProcessType9(IXLWorksheet worksheet, string line, int rowNumber)
{
  worksheet.Cell(rowNumber, 1).Value = "type 9";
  worksheet.Cell(rowNumber, 2).Value = line;
}

void ProcessType1(IXLWorksheet worksheet, string line, int rowNumber)
{
  worksheet.Cell(rowNumber, 1).Value = "type 1";
  worksheet.Cell(rowNumber, 2).Value = line;
}

void ProcessType0(IXLWorksheet worksheet, string line, int rowNumber)
{
  worksheet.Cell(rowNumber, 1).Value = "type A";
  worksheet.Cell(rowNumber, 2).Value = line;
}

LineType IdentifyLineType(string line)
{
  string idChar = line[14].ToString();

  switch (idChar)
  {
    case "0":
      return LineType.Type0;
    case "1":
      return LineType.Type1;
    case "9":
      return LineType.Type9;
    case "C":
      return LineType.TypeC;
    default:
      return LineType.Unknown;
      // throw exception if we want to cancel de file processing and catch it on error
      // throw new Exception("Invalid line type");
  }
}


enum LineType
{
  Type0,
  Type1,
  Type9,
  TypeC,
  Unknown
}