using Microsoft.Extensions.Configuration;
var appName = new ConfigurationBuilder().AddJsonFile("settings.json").Build().GetSection("ConnectionStrings")["defaultConnection"];


Console.WriteLine(String.IsNullOrEmpty(appName));
var result = appName ?? "joe";
Console.WriteLine(result);


var test = $@"b\la\blabla";
Console.WriteLine(test);