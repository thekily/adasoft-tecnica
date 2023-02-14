
public class LineType0 : LineStrategy
{

  public override Dictionary<string, string> ProcessLine(string line)
  {
    Dictionary<string, string> fields = new Dictionary<string, string>();

    fields.Add("formatType",                 line.Substring(    0      ,    1       ));
    fields.Add("companyCode",                line.Substring(    1      ,    5       ));
    fields.Add("date",                       line.Substring(    6      ,    8       ));
    fields.Add("registerType",               line.Substring(    14     ,    1       ));
    fields.Add("accountNumber",              line.Substring(    15     ,    12      ));
    fields.Add("description",                line.Substring(    27     ,    30      ));
    fields.Add("amountType",                 line.Substring(    57     ,    1       ));
    fields.Add("documentReference",          line.Substring(    58     ,    10      ));
    fields.Add("accountLineNumber",          line.Substring(    68     ,    1       ));
    fields.Add("accountDescription",         line.Substring(    69     ,    30      ));
    fields.Add("amount",                     line.Substring(    99     ,    14      ));
    fields.Add("emptyReservation1",          line.Substring(    113    ,    137     ));
    fields.Add("accountSalaryIndicator",     line.Substring(    250    ,    1       ));
    fields.Add("hasAnaliticRegistry",        line.Substring(    251    ,    1       ));
    // example file line length is 255
    // fields.Add("emptyReservation2",          line.Substring(    252    ,    256     ));
    // fields.Add("linkCurrency",               line.Substring(    508    ,    1       ));
    // fields.Add("generated",                  line.Substring(    509    ,    1       ));
    // fields.Add("return",                     line.Substring(    510    ,    2       ));

    return fields;
  }

}
class LineType1 : LineStrategy
{

  public override Dictionary<string, string> ProcessLine(string line)
  {
    Dictionary<string, string> fields = new Dictionary<string, string>();
    
    fields.Add("formatType",                       line.Substring(    0      ,    1      ));
    fields.Add("companyCode",                      line.Substring(    1      ,    5      ));
    fields.Add("date",                             line.Substring(    6      ,    8      ));
    fields.Add("registerType",                     line.Substring(    14     ,    1      ));
    fields.Add("accountNumber",                    line.Substring(    15     ,    12     ));
    fields.Add("description",                      line.Substring(    27     ,    30     ));
    fields.Add("amountType",                       line.Substring(    57     ,    1      ));
    fields.Add("documentReference",                line.Substring(    58     ,    10     ));
    fields.Add("accountLineNumber",                line.Substring(    68     ,    1      ));
    fields.Add("accountDescription",               line.Substring(    69     ,    30     ));
    fields.Add("amount",                           line.Substring(    99     ,    14     ));
    fields.Add("emptyReservation1",                line.Substring(    113    ,    62     ));
    fields.Add("clientOrProviderCif",              line.Substring(    175    ,    14     ));
    fields.Add("postalCode",                       line.Substring(    189    ,    40     ));
    fields.Add("emptyReservation2",                line.Substring(    229    ,    5      ));
    fields.Add("operationDate",                    line.Substring(    234    ,    2      ));
    fields.Add("invoceDate",                       line.Substring(    236    ,    8      ));
    fields.Add("invoiceNumberExpandedForSii",      line.Substring(    244    ,    8      ));
    // example file line length is 255
    // fields.Add("emptyReservation3",                line.Substring(    312    ,    60     ));
    // fields.Add("linkCurrency",                     line.Substring(    508    ,   196     ));
    // fields.Add("generated",                        line.Substring(    509    ,    1      ));
    // fields.Add("return",                           line.Substring(    510    ,    2      ));

    return fields;
  }

}
class LineType9 : LineStrategy
{

  public override Dictionary<string, string> ProcessLine(string line)
  {
    Dictionary<string, string> fields = new Dictionary<string, string>();
    
    fields.Add("formatType",                 line.Substring(    0      ,    1       ));
    fields.Add("companyCode",                line.Substring(    1      ,    5       ));
    fields.Add("date",                       line.Substring(    6      ,    8       ));
    fields.Add("registerType",               line.Substring(    14     ,    1       ));
    fields.Add("accountNumber",              line.Substring(    15     ,    12      ));
    fields.Add("description",                line.Substring(    27     ,    30      ));
    fields.Add("amountType",                 line.Substring(    57     ,    1       ));
    fields.Add("documentReference",          line.Substring(    58     ,    10      ));
    fields.Add("accountLineNumber",          line.Substring(    68     ,    1       ));
    fields.Add("accountDescription",         line.Substring(    69     ,    30      ));
    // others

    return fields;
  }

}
class LineTypeC : LineStrategy
{

  public override Dictionary<string, string> ProcessLine(string line)
  {
    Dictionary<string, string> fields = new Dictionary<string, string>();
    
    fields.Add("formatType",                 line.Substring(    0      ,    1       ));
    fields.Add("companyCode",                line.Substring(    1      ,    5       ));
    fields.Add("date",                       line.Substring(    6      ,    8       ));
    fields.Add("registerType",               line.Substring(    14     ,    1       ));
    fields.Add("accountNumber",              line.Substring(    15     ,    12      ));
    fields.Add("description",                line.Substring(    27     ,    30      ));
    // others

    return fields;
  }

}