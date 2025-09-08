var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KargoTakip_Server_WebAPI>("kargo-takip-webapi");

builder.Build().Run();
