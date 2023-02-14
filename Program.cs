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
        WriteLine(worksheet, line, rowNumber);

        // Avanzar a la siguiente fila
        rowNumber++;
      }

      // Guardar el libro de trabajo como un archivo XLSX
      workbook.SaveAs(xlsxFilePath);
      Console.WriteLine($"File processed: {xlsxFilePath}");
    }
  }
}

void WriteLine(IXLWorksheet worksheet, string line, int rowNumber)
{

  Line lineParsed = new Line(rowNumber, line);

  lineParsed.WriteLine(worksheet);

}



public enum LineType
{
  Type0,
  Type1,
  Type9,
  TypeC,
  Unknown
}

public class Line
{
  public int rowNumber { get; set; }
  public string line { get; set; }
  public LineType type { get; set; }
  public IDictionary<string, string> fields = new Dictionary<string, string>();


  public Line(int rowNumber, string line)
  {
    this.rowNumber = rowNumber;
    this.line = line;
    this.type = GetType();
    this.fields = Parse();
  }

  private IDictionary<string, string> Parse()
  {
    IDictionary<string, string> fields = new Dictionary<string, string>();
    LineStrategy strategy;
    switch (type)
    {
      case LineType.Type0:
        strategy = new LineType0();
        break;
      case LineType.Type1:
        strategy = new LineType1();
        break;
      case LineType.Type9:
        strategy = new LineType9();
        break;
      case LineType.TypeC:
        strategy = new LineTypeC();
        break;

      default:
        strategy = new LineTypeUnknown();
        break;

    }
    return strategy.ProcessLine(line);
  }

  public override string ToString()
  {
    return line;
  }

  public LineType GetType()
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

  public void WriteLine(IXLWorksheet worksheet)
  {
    foreach (var x in fields.Select((entry, index) => new { entry, index }))
    {
      worksheet.Cell(rowNumber, x.index + 1).Value = x.entry.Value;
    }
  }

}

public abstract class LineStrategy
{
  public abstract Dictionary<string, string> ProcessLine(string line);
}



class LineTypeUnknown : LineStrategy
{

  public override Dictionary<string, string> ProcessLine(string line)
  {
    Dictionary<string, string> fields = new Dictionary<string, string>();
    return fields;
  }

}