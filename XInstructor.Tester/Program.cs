

using XInstructor.Common.Services;

NavigraphService navigraphService = new NavigraphService();

//string? airac = await navigraphService.GetAirac();
//if(airac is not null)
//{
//    Console.WriteLine($"Current Airac : {airac}");
//}

await navigraphService.DownloadNavigationDatabase();